using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureDownloadDataMapper : IMapper<DownloadDetailsModel, Models.DownloadDetailsModel>
    {
        public Models.DownloadDetailsModel Map(DownloadDetailsModel input)
        {
            return new Models.DownloadDetailsModel
            {
                Id = input.Id,
                Version = input.Version,
                Type = input.Type,
                DateUploaded = input.DateUploaded,
                ApplicableFrom = input.ApplicableFrom,
                DownloadLink = input.DownloadLink
            };
        }
    }
}
