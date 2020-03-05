using System;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.Telemetry.Interfaces;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureFrameworkService : IAzureFrameworkService
    {
        private readonly ITelemetry _telemetry;
        private readonly IFrameworkIndexService _frameworkIndexService;
        private readonly IMapper<FrameworkModel, Models.FrameworkModel> _mapper;
        private readonly IAzureService _azureService;

        public AzureFrameworkService(
            ITelemetry telemetry,
            IFrameworkIndexService frameworkIndexService,
            IMapper<FrameworkModel, Models.FrameworkModel> mapper,
            IAzureService azureService)
        {
            _telemetry = telemetry;
            _frameworkIndexService = frameworkIndexService;
            _mapper = mapper;
            _azureService = azureService;
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
    }
}