using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;

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
    }
}