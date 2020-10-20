using ESFA.DC.LARS.API.AzureSearch.Constants;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch.Clients
{
    public class DownloadsClient : SearchIndexClient, IDownloadsIndexService
    {
        public DownloadsClient(IAzureSettings settings)
            : base(settings.SearchServiceName, AzureIndexes.DownloadsIndex, new SearchCredentials(settings.SearchServiceAdminApiKey))
        {
        }
    }
}