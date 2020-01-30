using System.Diagnostics;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITelemetry _telemetryClient;

        public HomeController(
            ITelemetry telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public IActionResult Index()
        {
            _telemetryClient.TrackEvent("In home controller");
            return View();
        }

        [HttpPost("Search")]
        public IActionResult Search([FromForm]SearchModel searchModel)
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
