using ESFA.DC.LARS.API.AzureSearch.Constants;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch.Clients
{
    public class TLevelClient : SearchIndexClient, ITLevelIndexService
    {
        public TLevelClient(IAzureSettings settings)
            : base(settings.SearchServiceName, AzureIndexes.TLevelIndex, new SearchCredentials(settings.SearchServiceAdminApiKey))
        {
        }
    }
}