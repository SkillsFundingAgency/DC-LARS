using System.Diagnostics;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITelemetry _telemetryClient;
        private readonly ILearningAimsApiService _learningAimsApiService;

        public HomeController(
            ITelemetry telemetryClient,
            ILearningAimsApiService learningAimsApiService)
        {
            _telemetryClient = telemetryClient;
            _learningAimsApiService = learningAimsApiService;
        }

        public IActionResult Index()
        {
            var learningAims = _learningAimsApiService.GetLearningAims(new SearchModel()).Result;
            _telemetryClient.TrackEvent("In home controller");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
