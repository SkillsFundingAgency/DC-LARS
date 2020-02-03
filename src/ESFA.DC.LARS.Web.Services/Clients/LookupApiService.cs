using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class LookupApiService : ILookupApiService
    {
        private const string Url = "Lookups";

        private readonly IClientService _clientService;

        public LookupApiService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<LookUpModel> GetLookups()
        {
            var parameters = new Dictionary<string, object>();
            var response = await _clientService.GetAsync<LookUpModel>(Url, parameters);

            return response;
        }
    }
}