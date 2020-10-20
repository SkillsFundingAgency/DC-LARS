using ESFA.DC.LARS.Web.Interfaces;

namespace ESFA.DC.LARS.Web.Configuration
{
    public class ApiSettings : IApiSettings
    {
        public string BaseUrl { get; set; }

        public string BlobStorageConnectionString { get; set; }

        public string DownloadDataBlobStorageFolder { get; set; }
    }
}