namespace ESFA.DC.LARS.API.Interfaces
{
    public interface IAzureSettings
    {
        string SearchServiceName { get; set; }

        string SearchServiceAdminApiKey { get; set; }

        string SearchIndexName { get; set; }
    }
}