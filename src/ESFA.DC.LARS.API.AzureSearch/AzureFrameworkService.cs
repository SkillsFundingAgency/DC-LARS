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
            IODataQueryService oDataQueryService,
            ISearchTermFormattingService searchTermFormattingService)
            : base(searchTermFormattingService)
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

            var searchTerm = FormatSearchTerm(searchModel.SearchTerm);

            IEnumerable<Models.FrameworkModel> frameworks;
            frameworks = await SearchIndex(searchTerm, parameters);

            return frameworks;
        }

        public async Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            Models.FrameworkModel framework;

            var key = $"{frameworkCode}-{programType}-{pathwayCode}";
            var result = await _azureService.GetAsync<FrameworkModel>(_frameworkIndexService, key);

            framework = _mapper.Map(result);

            return framework;
        }

        private async Task<IEnumerable<Models.FrameworkModel>> SearchIndex(string searchTerm, SearchParameters parameters)
        {
            IEnumerable<Models.FrameworkModel> frameworks;

            var result = await _azureService.SearchIndexAsync<FrameworkModel>(_frameworkIndexService, searchTerm, parameters);
            frameworks = result.Results
                .Select(r => r.Document)
                .Select(d => _mapper.Map(d))
                .OrderBy(f => f.FrameworkTitle)
                .ThenBy(f => f.ProgramType)
                .ThenByDescending(f => f.PathwayCode);

            return frameworks;
        }

        private void SetFilters(FrameworkSearchModel searchModel, SearchParameters parameters)
        {
            _oDataQueryService.SetFrameworkFilters(searchModel, parameters);
        }
    }
}