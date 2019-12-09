using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using Flurl;
using Flurl.Http;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IApiSettings _settings;

        public ClientService(
            IApiSettings settings)
        {
            _settings = settings;
        }

        public async Task<TResult> PostAsync<TContent, TResult>(string url, TContent content)
        {
            return await _settings.BaseUrl
                .AppendPathSegment(url)
                .PostJsonAsync(content)
                .ReceiveJson<TResult>();
        }
    }
}