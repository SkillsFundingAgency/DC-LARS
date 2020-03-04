using System;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureFrameworkService : IAzureFrameworkService
    {
        private readonly string _frameworkQueryString = "FrameworkCode eq {0} and ProgramType eq {1} and PathwayCode eq {2}";

        private readonly ITelemetry _telemetry;
        private readonly IFrameworkIndexService _frameworkIndexService;
        private readonly IMapper<FrameworkModel, Models.FrameworkModel> _mapper;
        private readonly IAzureService _azureService;
        private readonly ISearchCleaningService _searchCleaningService;

        public AzureFrameworkService(
            ITelemetry telemetry,
            IFrameworkIndexService frameworkIndexService,
            IMapper<FrameworkModel, Models.FrameworkModel> mapper,
            IAzureService azureService,
            ISearchCleaningService searchCleaningService)
        {
            _telemetry = telemetry;
            _frameworkIndexService = frameworkIndexService;
            _mapper = mapper;
            _azureService = azureService;
            _searchCleaningService = searchCleaningService;
        }

        public async Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            Models.FrameworkModel framework;

            var parameters = new SearchParameters
            {
                Filter = GetFilter(frameworkCode, programType, pathwayCode)
            };

            try
            {
                var result = await _azureService.SearchIndexAsync<FrameworkModel>(_frameworkIndexService, string.Empty, parameters);

                framework = result.Results.Select(r => _mapper.Map(r.Document)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _telemetry.TrackEvent(ex.Message);
                throw;
            }

            return framework;
        }

        private string GetFilter(int frameworkCode, int programType, int pathwayCode)
        {
            return _searchCleaningService.EscapeFilterSpecialCharacters(
                string.Format(_frameworkQueryString, frameworkCode, programType, pathwayCode));
        }
    }
}