using ESFA.DC.LARS.API.AzureSearch.Constants;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch.Clients
{
    public class LookupClient : SearchIndexClient, ILookupIndexService
    {
        public LookupClient(IAzureSettings settings)
            : base(settings.SearchServiceName, AzureIndexes.LookupIndex, new SearchCredentials(settings.SearchServiceAdminApiKey))
        {
        }
    }
}