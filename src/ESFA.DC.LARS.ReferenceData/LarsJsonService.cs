using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.ReferenceDataService.Model.LARS;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.LARS.API.ReferenceData
{
    public class LarsJsonService : ILarsJsonService
    {
        private readonly IFileService _fileService;
        private readonly IJsonSerializationService _jsonSerializationService;
        private readonly IMapper<LARSLearningDelivery, LearningAimModel> _mapper;
        private readonly IPathProvider _pathProvider;

        public LarsJsonService(
            IFileService fileService,
            IJsonSerializationService jsonSerializationService,
            IMapper<LARSLearningDelivery, LearningAimModel> mapper,
            IPathProvider pathProvider)
        {
            _fileService = fileService;
            _jsonSerializationService = jsonSerializationService;
            _mapper = mapper;
            _pathProvider = pathProvider;
        }

        public async Task<IEnumerable<LearningAimModel>> GetLarsLearningDeliveriesFromJsonFile(SearchModel searchModel)
        {
            IEnumerable<LearningAimModel> larsLearningDeliveries;

            try
            {
                using (var stream = await _fileService.OpenReadStreamAsync(_pathProvider.GetFileLocation(ReferenceDataConstants.LarsReferenceDataZipFile), null, CancellationToken.None))
                {
                    using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
                    {
                        var query = RetrieveModel<List<LARSLearningDelivery>>(zip,  ReferenceDataConstants.LarsLearningDeliveriesFile).AsQueryable();

                        query = FilterDeliveryMatchesSearch(query, searchModel);

                        larsLearningDeliveries = query
                            .Select(ld => _mapper.Map(ld))
                            .ToList();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return larsLearningDeliveries;
        }

        private IQueryable<LARSLearningDelivery> FilterDeliveryMatchesSearch(IQueryable<LARSLearningDelivery> query, SearchModel searchModel)
        {
            if (searchModel == null)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                query = query.Where(ld =>
                    ld.LearnAimRefTitle.Contains(searchModel.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    ld.LearnAimRef.Equals(searchModel.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchModel.AwardingBody))
            {
                query = query.Where(ld => ld.AwardOrgCode.Contains(searchModel.AwardingBody));
            }

            //if (!string.IsNullOrEmpty(searchModel.Level))
            //{
            //    query = query.Where(ld => ld.NotionalNVQLevelv2.Contains(searchModel.Level));
            //}

            if (searchModel.SearchFilters != null)
            {
                if (searchModel.SearchFilters?.AwardingBodies.Any() ?? false)
                {
                    query = query.Where(ld => searchModel.SearchFilters.AwardingBodies.Any(ab => ab.Contains(ld.AwardOrgCode, StringComparison.OrdinalIgnoreCase)));
                }

                if (searchModel.SearchFilters?.Levels.Any() ?? false)
                {
                    query = query.Where(ld => searchModel.SearchFilters.Levels.Any(ab => ab.Contains(ld.NotionalNVQLevelv2, StringComparison.OrdinalIgnoreCase)));
                }
            }

            return query;
        }

        private T RetrieveModel<T>(ZipArchive zipArchive, string fileName)
        {
            T model;
            using (var stream = zipArchive.GetEntry(fileName)?.Open())
            {
                model = _jsonSerializationService.Deserialize<T>(stream);
            }

            return model;
        }
    }
}