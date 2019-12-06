using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using Newtonsoft.Json;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IApiSettings _settings;
        private readonly HttpClient _client;

        public ClientService(
            IApiSettings settings,
            HttpClient client)
        {
            _settings = settings;
            _client = client;
        }

        public async Task<TResult> PostAsync<TContent, TResult>(string url, TContent content)
        {
            var json = JsonConvert.SerializeObject(content);
            var contentString = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_settings.BaseUrl}{url}", contentString);

            response.EnsureSuccessStatusCode();
            var aimsString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(aimsString);
        }
    }
}