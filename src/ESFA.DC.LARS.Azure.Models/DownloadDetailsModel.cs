using System;

namespace ESFA.DC.LARS.Azure.Models
{
    public class DownloadDetailsModel
    {
        public string Version { get; set; }

        public string Type { get; set; }

        public DateTime ApplicableFrom { get; set; }

        public DateTime DateUploaded { get; set; }

        public string DownloadLink { get; set; }
    }
}