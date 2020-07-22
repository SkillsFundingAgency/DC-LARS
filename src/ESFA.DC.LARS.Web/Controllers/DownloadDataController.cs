using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("DownloadData")]
    public class DownloadDataController : Controller
    {
        private readonly IDownloadDataApiService _downloadDataApiService;

        public DownloadDataController(IDownloadDataApiService downloadDataApiService)
        {
            _downloadDataApiService = downloadDataApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = _downloadDataApiService.GetDownloadData();

            return View(model);
        }
    }
}
