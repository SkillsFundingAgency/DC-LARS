using ESFA.DC.LARS.API.AzureSearch.Constants;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch.Clients
{
    public class FrameworkClient : SearchIndexClient, IFrameworkIndexService
    {
        public FrameworkClient(IAzureSettings settings)
            : base(settings.SearchServiceName, AzureIndexes.FrameworkIndex, new SearchCredentials(settings.SearchServiceAdminApiKey))
        {
        }
    }
}