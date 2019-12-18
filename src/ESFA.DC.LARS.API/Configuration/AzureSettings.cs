using ESFA.DC.LARS.API.Interfaces;

namespace ESFA.DC.LARS.API.Configuration
{
    public class AzureSettings : IAzureSettings
    {
        public string SearchServiceName { get; set; }

        public string SearchServiceAdminApiKey { get; set; }

        public string SearchIndexName { get; set; }
    }
}
