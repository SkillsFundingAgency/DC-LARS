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
using FrameworkModel = ESFA.DC.LARS.Azure.Models.FrameworkModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureFrameworkService : AzureBaseService, IAzureFrameworkService
    {
        private readonly ITelemetry _telemetry;
        private readonly IFrameworkIndexService _frameworkIndexService;
        private readonly IMapper<FrameworkModel, Models.FrameworkModel> _mapper;
        private readonly IAzureService _azureService;
        private readonly IODataQueryService _oDataQueryService;

        public AzureFrameworkService(
            ITelemetry telemetry,
            IFrameworkIndexService frameworkIndexService,
            IMapper<FrameworkModel, Models.FrameworkModel> mapper,
            IAzureService azureService,
            IODataQueryService oDataQueryService)
        {
            _telemetry = telemetry;
            _frameworkIndexService = frameworkIndexService;
            _mapper = mapper;
            _azureService = azureService;
            _oDataQueryService = oDataQueryService;
        }

        public async Task<IEnumerable<Models.FrameworkModel>> GetFrameworks(FrameworkSearchModel searchModel)
        {
            var parameters = GetDefaultParameters();

            SetFilters(searchModel, parameters);

            var searchTerm = string.Empty;
            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                searchTerm = $"{searchModel.SearchTerm}";
            }

            IEnumerable<Models.FrameworkModel> frameworks;
            try
            {
                frameworks = await SearchIndex(searchTerm, parameters);
            }
            catch (Exception ex)
            {
                _telemetry.TrackEvent(ex.Message);
                throw;
            }

            return frameworks;
        }

        public async Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            Models.FrameworkModel framework;

            var key = $"{frameworkCode}-{programType}-{pathwayCode}";

            try
            {
                var result = await _azureService.GetAsync<FrameworkModel>(_frameworkIndexService, key);

                framework = _mapper.Map(result);
            }
            catch (Exception ex)
            {
                _telemetry.TrackEvent(ex.Message);
                throw;
            }

            return framework;
        }

        private async Task<IEnumerable<Models.FrameworkModel>> SearchIndex(string searchTerm, SearchParameters parameters)
        {
            IEnumerable<Models.FrameworkModel> frameworks;
            try
            {
                var result = await _azureService.SearchIndexAsync<FrameworkModel>(_frameworkIndexService, searchTerm, parameters);

                frameworks = result.Results.Select(r => r.Document).Select(d => _mapper.Map(d));
            }
            catch (Exception ex)
            {
                _telemetry.TrackEvent(ex.Message);
                throw;
            }

            return frameworks;
        }

        private void SetFilters(FrameworkSearchModel searchModel, SearchParameters parameters)
        {
            _oDataQueryService.SetFrameworkFilters(searchModel, parameters);
        }
    }
}