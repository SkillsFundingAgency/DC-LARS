using System;

namespace ESFA.DC.LARS.API.Models
{
    public class DownloadDetailsModel
    {
        public string Id { get; set; }

        public string Version { get; set; }

        public string Type { get; set; }

        public DateTime ApplicableFrom { get; set; }

        public DateTime DateUploaded { get; set; }

        public string DownloadLink { get; set; }
    }
}