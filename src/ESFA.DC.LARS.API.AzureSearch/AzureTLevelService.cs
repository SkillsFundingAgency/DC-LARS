using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.Telemetry.Interfaces;
using FrameworkModel = ESFA.DC.LARS.Azure.Models.FrameworkModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureTLevelService : AzureFrameworkService, IAzureTLevelService
    {
        public AzureTLevelService(
            ITelemetry telemetryClient,
            IMapper<FrameworkModel, Models.FrameworkModel> mapper,
            ITLevelIndexService tlevelIndex,
            IODataQueryService oDataQueryService,
            IAzureService azureService)
            : base(telemetryClient, tlevelIndex, mapper, azureService, oDataQueryService)
        {
        }
    }
}