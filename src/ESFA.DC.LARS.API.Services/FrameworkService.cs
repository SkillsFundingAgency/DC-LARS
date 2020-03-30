using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class FrameworkService : IFrameworkService
    {
        private readonly IAzureFrameworkService _azureFrameworkService;

        public FrameworkService(IAzureFrameworkService azureFrameworkService)
        {
            _azureFrameworkService = azureFrameworkService;
        }

        public async Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            return await _azureFrameworkService.GetFramework(frameworkCode, programType, pathwayCode);
        }

        public async Task<IEnumerable<FrameworkModel>> GetFrameworks(FrameworkSearchModel searchModel)
        {
            return await _azureFrameworkService.GetFrameworks(searchModel);
        }
    }
}