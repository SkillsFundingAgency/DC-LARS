using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class TLevelService : ITLevelService
    {
        private readonly IAzureTLevelService _azureTLevelService;
        private readonly ISearchCleaningService _searchCleaningService;

        public TLevelService(IAzureTLevelService azureTLevelService, ISearchCleaningService searchCleaningService)
        {
            _azureTLevelService = azureTLevelService;
            _searchCleaningService = searchCleaningService;
        }

        public async Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            return await _azureTLevelService.GetFramework(frameworkCode, programType, pathwayCode);
        }

        public async Task<IEnumerable<FrameworkModel>> GetFrameworks(FrameworkSearchModel searchModel)
        {
            searchModel.SearchTerm = _searchCleaningService.EscapeSearchSpecialCharacters(searchModel.SearchTerm);
            return await _azureTLevelService.GetFrameworks(searchModel);
        }
    }
}