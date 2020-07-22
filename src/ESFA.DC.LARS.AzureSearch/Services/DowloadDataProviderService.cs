using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class DowloadDataProviderService : IDownloadDataProviderService
    {
        public async Task<IEnumerable<DownloadDetailsModel>> GetDownloadDetails()
        {
            return new List<DownloadDetailsModel>
            {
                new DownloadDetailsModel
                {
                    Id = "1",
                    Version = "7",
                    Type = "CSV",
                    ApplicableFrom = new DateTime(2020, 8, 1),
                    DateUploaded = new DateTime(2020, 8, 1),
                    DownloadLink = "larsdownloads/published/007/LearningDelivery_V007_CSV.Zip"
                }
            };
        }
    }
}