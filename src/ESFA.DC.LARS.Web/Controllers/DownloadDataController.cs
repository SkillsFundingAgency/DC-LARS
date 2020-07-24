using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("DownloadData")]
    public class DownloadDataController : Controller
    {
        private readonly IApiSettings _apiSettings;
        private readonly IStorageService _storageService;
        private readonly IDownloadDataApiService _downloadDataApiService;

        public DownloadDataController(
            IApiSettings apiSettings,
            IStorageService storageService,
            IDownloadDataApiService downloadDataApiService)
        {
            _apiSettings = apiSettings;
            _storageService = storageService;
            _downloadDataApiService = downloadDataApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _downloadDataApiService.GetDownloadData();

            return View(model);
        }

        [HttpGet("GetDownloadFileAsync")]
        public async Task<FileResult> GetDownloadFileAsync(string fileName, CancellationToken cancellationToken)
        {
            var blobStream = await _storageService.GetFile(_apiSettings.DownloadDataBlobStorageFolder, fileName, cancellationToken);

            return new FileStreamResult(blobStream, _storageService.GetMimeTypeFromFileName(fileName))
            {
                FileDownloadName = fileName
            };
        }
    }
}
