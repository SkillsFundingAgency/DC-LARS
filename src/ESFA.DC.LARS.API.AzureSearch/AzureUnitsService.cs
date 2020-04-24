using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.Telemetry.Interfaces;
using LearningAimModel = ESFA.DC.LARS.Azure.Models.LearningAimModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureUnitsService : AzureLearningAimsService, IAzureUnitsService
    {
        public AzureUnitsService(
            ITelemetry telemetryClient,
            IMapper<LearningAimModel, Models.LearningAimModel> mapper,
            IUnitIndexService unitIndex,
            IODataQueryService oDataQueryService,
            IAzureService azureService)
            : base(telemetryClient, mapper, unitIndex, oDataQueryService, azureService)
        {
        }
    }
}