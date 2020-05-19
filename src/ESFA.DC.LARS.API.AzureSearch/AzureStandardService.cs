using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.Azure.Search.Models;
using StandardModel = ESFA.DC.LARS.Azure.Models.StandardModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureStandardService : AzureBaseService, IAzureStandardService
    {
        private readonly ITelemetry _telemetry;
        private readonly IStandardIndexService _standardIndexService;
        private readonly IMapper<StandardModel, Models.StandardModel> _mapper;
        private readonly IAzureService _azureService;
        private readonly IODataQueryService _oDataQueryService;

        public AzureStandardService(
            ITelemetry telemetry,
            IStandardIndexService standardIndexService,
            IMapper<StandardModel, Models.StandardModel> mapper,
            IAzureService azureService,
            IODataQueryService oDataQueryService)
        {
            _telemetry = telemetry;
            _standardIndexService = standardIndexService;
            _mapper = mapper;
            _azureService = azureService;
            _oDataQueryService = oDataQueryService;
        }

        public async Task<IEnumerable<Models.StandardModel>> GetStandards(StandardSearchModel searchModel)
        {
            var parameters = GetDefaultParameters();

            var searchTerm = string.Empty;
            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                searchTerm = $"{searchModel.SearchTerm}";
            }

            IEnumerable<Models.StandardModel> standards;
            try
            {
                standards = await SearchIndex(searchTerm, parameters);
            }
            catch (Exception ex)
            {
                _telemetry.TrackEvent(ex.Message);
                throw;
            }

            return standards;
        }

        public async Task<Models.StandardModel> GetStandard(string standardCode)
        {
            Models.StandardModel standard;

            try
            {
                var result = await _azureService.GetAsync<StandardModel>(_standardIndexService, standardCode);

                standard = _mapper.Map(result);
            }
            catch (Exception ex)
            {
                _telemetry.TrackEvent(ex.Message);
                throw;
            }

            return standard;
        }

        private async Task<IEnumerable<Models.StandardModel>> SearchIndex(string searchTerm, SearchParameters parameters)
        {
            IEnumerable<Models.StandardModel> standards;

            var result = await _azureService.SearchIndexAsync<StandardModel>(_standardIndexService, searchTerm, parameters);

            standards = result.Results
                .Select(r => r.Document)
                .Select(d => _mapper.Map(d))
                .OrderBy(f => f.StandardName);

            return standards;
        }
    }
}