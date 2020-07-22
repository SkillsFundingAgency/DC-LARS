using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class DownloadDataApiService : IDownloadDataApiService
    {
        protected string Url;

        private const string KeyParameterName = "key";
        private const string WildcardValue = "*";

        private readonly IClientService _clientService;

        public DownloadDataApiService(IClientService clientService)
        {
            _clientService = clientService;
            Url = "DownloadData";
        }

        public async Task<IEnumerable<DownloadDetailsModel>> GetDownloadData()
        {
            var parameters = new Dictionary<string, object>
            {
                { KeyParameterName, WildcardValue }
            };
            var response = await _clientService.GetAsync<IEnumerable<DownloadDetailsModel>>(Url, parameters);

            return response;
        }
    }
}