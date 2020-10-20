namespace ESFA.DC.LARS.Web.Interfaces
{
    public interface IApiSettings
    {
        string BaseUrl { get; set; }

        string BlobStorageConnectionString { get; set; }

        string DownloadDataBlobStorageFolder { get; set; }
    }
}