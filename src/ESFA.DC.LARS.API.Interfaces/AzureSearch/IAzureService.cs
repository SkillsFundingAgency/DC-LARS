using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.Interfaces.AzureSearch
{
    public interface IAzureService
    {
        Task<DocumentSearchResult<T>> SearchIndexAsync<T>(ISearchIndexClient indexClient, string searchTerm, SearchParameters parameters);

        Task<T> GetAsync<T>(ISearchIndexClient indexClient, string searchTerm);
    }
}