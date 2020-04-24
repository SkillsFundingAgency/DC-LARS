using ESFA.DC.LARS.API.AzureSearch.Constants;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch.Clients
{
    public class UnitsClient : SearchIndexClient, IUnitIndexService
    {
        public UnitsClient(IAzureSettings settings)
            : base(settings.SearchServiceName, AzureIndexes.UnitIndex, new SearchCredentials(settings.SearchServiceAdminApiKey))
        {
        }
    }
}