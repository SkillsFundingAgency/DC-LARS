using System;
using System.ComponentModel.DataAnnotations;

namespace ESFA.DC.LARS.Azure.Models
{
    public class DownloadDetailsModel
    {
        [Key]
        public string Id { get; set; }

        public string Version { get; set; }

        public string Type { get; set; }

        public DateTime ApplicableFrom { get; set; }

        public DateTime DateUploaded { get; set; }

        public string DownloadLink { get; set; }
    }
}