using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using DownloadDetailsModel = ESFA.DC.LARS.Azure.Models.DownloadDetailsModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureDownloadsService : AzureBaseService, IAzureDownloadsService
    {
        private readonly IAzureService _azureService;
        private readonly IMapper<DownloadDetailsModel, Models.DownloadDetailsModel> _mapper;
        private readonly IDownloadsIndexService _downloadsIndexService;

        public AzureDownloadsService(
            IAzureService azureService,
            IMapper<DownloadDetailsModel, Models.DownloadDetailsModel> mapper,
            IDownloadsIndexService downloadsIndexService)
        {
            _azureService = azureService;
            _mapper = mapper;
            _downloadsIndexService = downloadsIndexService;
        }

        public async Task<IEnumerable<Models.DownloadDetailsModel>> GetDownloadDetails(string key)
        {
            var result = new List<Models.DownloadDetailsModel>();

            var allDownloads = (await _azureService.GetAsync<IEnumerable<DownloadDetailsModel>>(_downloadsIndexService, key)).ToList();
            var downloadsForLastTwoVersions = allDownloads
                .Select(d => _mapper.Map(d))
                .GroupBy(d => d.Version, d => d)
                .OrderByDescending(d => d.Key)
                .Take(2);

            foreach (var versionDownloads in downloadsForLastTwoVersions)
            {
                var downloads = versionDownloads.ToList();
                var downloadsPerType = downloads.GroupBy(d => d.Type);

                foreach (var typeDownloads in downloadsPerType)
                {
                    var latestDownloadForType = typeDownloads.FirstOrDefault(d => d.DateUploaded == typeDownloads.Max(dd => dd.DateUploaded));
                    if (latestDownloadForType != null)
                    {
                        result.Add(latestDownloadForType);
                    }
                }
            }

            return result
                .OrderByDescending(d => d.Version)
                .ThenBy(d => d.Type);
        }
    }
}