﻿using System;
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

        public LarsJsonService(
            IFileService fileService,
            IJsonSerializationService jsonSerializationService,
            IMapper<LARSLearningDelivery, LearningAimModel> mapper)
        {
            _fileService = fileService;
            _jsonSerializationService = jsonSerializationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LearningAimModel>> GetLarsLearningDeliveriesFromJsonFile()
        {
            List<LearningAimModel> larsLearningDeliveries;

            try
            {
                using (var stream = await _fileService.OpenReadStreamAsync(ReferenceDataConstants.LarsReferenceDataZipFile, null, CancellationToken.None))
                {
                    using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
                    {
                        larsLearningDeliveries = RetrieveModel<List<LARSLearningDelivery>>(zip, ReferenceDataConstants.LarsLearningDeliveriesFile)
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