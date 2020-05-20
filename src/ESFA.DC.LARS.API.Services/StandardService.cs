using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class StandardService : IStandardService
    {
        private readonly IAzureStandardService _azureStandardService;
        private readonly ISearchCleaningService _searchCleaningService;

        public StandardService(IAzureStandardService azureStandardService, ISearchCleaningService searchCleaningService)
        {
            _azureStandardService = azureStandardService;
            _searchCleaningService = searchCleaningService;
        }

        public async Task<Models.StandardModel> GetStandard(string standardCode)
        {
            return await _azureStandardService.GetStandard(standardCode);
        }

        public async Task<IEnumerable<StandardModel>> GetStandards(StandardSearchModel searchModel)
        {
            searchModel.SearchTerm = _searchCleaningService.EscapeSearchSpecialCharacters(searchModel.SearchTerm);
            return await _azureStandardService.GetStandards(searchModel);
        }
    }
}