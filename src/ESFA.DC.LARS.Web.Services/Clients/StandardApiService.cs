using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class StandardApiService : IStandardApiService
    {
        private const string Url = "Standard";
        private const string StandardCodeParameterName = "standardCode";
        private readonly IClientService _clientService;

        public StandardApiService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<StandardModel> GetStandard(string standardCode)
        {
            var parameters = new Dictionary<string, object>
            {
                { StandardCodeParameterName, standardCode }
            };

            var response = await _clientService.GetAsync<StandardModel>(Url, parameters);

            return response;
        }

        public async Task<IEnumerable<StandardModel>> GetStandards(StandardSearchModel content)
        {
            var response = await _clientService.PostAsync<StandardSearchModel, IEnumerable<StandardModel>>(Url, content);

            return response;
        }
    }
}