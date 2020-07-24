using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;

namespace ESFA.DC.LARS.API.Services
{
    public class DownloadDataService : IDownloadDataService
    {
        private readonly IAzureDownloadsService _downloadsService;

        public DownloadDataService(IAzureDownloadsService downloadsService)
        {
            _downloadsService = downloadsService;
        }

        public async Task<IEnumerable<Models.DownloadDetailsModel>> GetDownloadDetails(string key)
        {
            return await _downloadsService.GetDownloadDetails(key);
        }
    }
}