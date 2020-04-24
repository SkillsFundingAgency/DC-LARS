using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;

namespace ESFA.DC.LARS.API.Services
{
    public class UnitService : LearningAimAzureService, IUnitService
    {
        public UnitService(
            IAzureUnitsService azureSearchService,
            ISearchCleaningService searchCleaningService)
        : base(azureSearchService, searchCleaningService)
        {
        }
    }
}