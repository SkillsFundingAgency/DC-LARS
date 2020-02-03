using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;

namespace ESFA.DC.LARS.API.Services
{
    public class LookupService : ILookupService
    {
        private readonly IAzureLookupService _azureLookupService;

        public LookupService(
            IAzureLookupService azureLookupService)
        {
            _azureLookupService = azureLookupService;
        }

        public async Task<Models.LookUpModel> GetLookups()
        {
            return await _azureLookupService.GetLookups();
        }
    }
}