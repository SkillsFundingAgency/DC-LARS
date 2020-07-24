using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESFA.DC.LARS.API.Interfaces.AzureSearch
{
    public interface IAzureDownloadsService
    {
        Task<IEnumerable<Models.DownloadDetailsModel>> GetDownloadDetails(string key);
    }
}