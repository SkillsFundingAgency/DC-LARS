using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureService : IAzureService
    {
        public async Task<DocumentSearchResult<T>> SearchIndexAsync<T>(ISearchIndexClient indexClient, string searchTerm, SearchParameters parameters)
        {
            return await indexClient.Documents.SearchAsync<T>(searchTerm, parameters);
        }

        public async Task<T> GetAsync<T>(ISearchIndexClient indexClient, string searchTerm)
        {
            return await indexClient.Documents.GetAsync<T>(searchTerm);
        }
    }
}