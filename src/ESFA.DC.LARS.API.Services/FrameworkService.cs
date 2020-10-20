using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class FrameworkService : IFrameworkService
    {
        private readonly IAzureFrameworkService _azureFrameworkService;
        private readonly ISearchCleaningService _searchCleaningService;

        public FrameworkService(IAzureFrameworkService azureFrameworkService, ISearchCleaningService searchCleaningService)
        {
            _azureFrameworkService = azureFrameworkService;
            _searchCleaningService = searchCleaningService;
        }

        public async Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            return await _azureFrameworkService.GetFramework(frameworkCode, programType, pathwayCode);
        }

        public async Task<IEnumerable<FrameworkModel>> GetFrameworks(FrameworkSearchModel searchModel)
        {
            searchModel.SearchTerm = _searchCleaningService.EscapeSearchSpecialCharacters(searchModel.SearchTerm);
            return await _azureFrameworkService.GetFrameworks(searchModel);
        }
    }
}