using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using Newtonsoft.Json;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class ClientService : IClientService
    {
        private const string BaseUrl = "http://localhost:4257/api/";
        private readonly HttpClient _client;

        public ClientService(
            HttpClient client)
        {
            _client = client;
        }

        public async Task<TResult> PostAsync<TContent, TResult>(string url, TContent content)
        {
            var json = JsonConvert.SerializeObject(content);
            var contentString = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{BaseUrl}{url}", contentString);

            response.EnsureSuccessStatusCode();
            var aimsString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(aimsString);
        }
    }
}