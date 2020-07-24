using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface IStorageService
    {
        Task<Stream> GetFile(string containerName, string fileName, CancellationToken cancellationToken);

        string GetMimeTypeFromFileName(string fileName);
    }
}