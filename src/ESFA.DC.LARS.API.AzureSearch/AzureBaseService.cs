using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public abstract class AzureBaseService
    {
        protected SearchParameters GetDefaultParameters()
        {
            return new SearchParameters
            {
                QueryType = QueryType.Full,
                SearchMode = SearchMode.Any,
                IncludeTotalResultCount = true,
                Top = 10000
            };
        }
    }
}