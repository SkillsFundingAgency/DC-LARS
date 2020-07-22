using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IDownloadDataProviderService
    {
        Task<IEnumerable<DownloadDetailsModel>> GetDownloadDetails();
    }
}