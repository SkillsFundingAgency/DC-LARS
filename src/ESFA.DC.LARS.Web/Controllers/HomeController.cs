using System.Diagnostics;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int SearchFieldMaxLength = 100;

        private readonly ITelemetry _telemetryClient;
        private readonly ILookupApiService _lookupApiService;

        public HomeController(
            ITelemetry telemetryClient,
            ILookupApiService lookupApiService)
        {
            _telemetryClient = telemetryClient;
            _lookupApiService = lookupApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new LookupViewModel();

            var lookups = await _lookupApiService.GetLookups();
            model.lookups = lookups;

            _telemetryClient.TrackEvent("ESFA.DC.LARS.Web - In home controller");
            return View(model);
        }

        [HttpPost("Search")]
        public IActionResult Search([FromForm]BasicSearchModel searchModel)
        {
            return RedirectToAction("Index", "SearchResult", searchModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
