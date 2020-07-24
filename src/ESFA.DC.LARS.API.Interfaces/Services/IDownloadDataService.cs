using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface IDownloadDataService
    {
        Task<IEnumerable<Models.DownloadDetailsModel>> GetDownloadDetails(string key);
    }
}