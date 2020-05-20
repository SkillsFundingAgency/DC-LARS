using ESFA.DC.LARS.API.AzureSearch.Constants;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch.Clients
{
    public class StandardClient : SearchIndexClient, IStandardIndexService
    {
        public StandardClient(IAzureSettings settings)
            : base(settings.SearchServiceName, AzureIndexes.StandardIndex, new SearchCredentials(settings.SearchServiceAdminApiKey))
        {
        }
    }
}