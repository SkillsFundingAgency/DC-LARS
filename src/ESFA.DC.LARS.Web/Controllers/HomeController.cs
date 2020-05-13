using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITelemetry _telemetryClient;
        private readonly ILookupApiService _lookupApiService;
        private readonly IClientValidationService _clientValidationService;

        public HomeController(
            ITelemetry telemetryClient,
            ILookupApiService lookupApiService,
            IClientValidationService clientValidationService)
        {
            _telemetryClient = telemetryClient;
            _lookupApiService = lookupApiService;
            _clientValidationService = clientValidationService;
        }

        public async Task<IActionResult> Index(HomeViewModel model = null)
        {
            model ??= new HomeViewModel();

            var lookups = await _lookupApiService.GetLookups();
            model.Lookups = lookups;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index([FromForm]BasicSearchModel searchModel, LearningType searchType)
        {
            var model = new HomeViewModel();
            ValidateSearch(searchModel, model);

            if (model.ValidationErrors.Any())
            {
                return RedirectToAction("Index", model);
            }

            switch (searchType)
            {
                // redirect to action based on the search Type.
                case LearningType.Qualifications:
                    return RedirectToAction("Index", "LearningAimSearchResult", searchModel);
                case LearningType.Frameworks:
                    return RedirectToAction("Index", "FrameworkSearchResult", searchModel);
                case LearningType.Units:
                    return RedirectToAction("Index", "UnitSearchResult", searchModel);
                case LearningType.Standards:
                    return RedirectToAction("Index", "StandardsSearchResult", searchModel);
                default:
                    return RedirectToAction("Index", model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void ValidateSearch(BasicSearchModel searchModel, HomeViewModel viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }

            var filterError = _clientValidationService.FilterLengthValid(searchModel.AwardingBody);
            if (!string.IsNullOrEmpty(filterError))
            {
                viewModel.ValidationErrors.Add(filterError);
            }
        }
    }
}
