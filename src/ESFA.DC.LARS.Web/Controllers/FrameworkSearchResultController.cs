using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("FrameworkSearchResult")]
    public class FrameworkSearchResultController : Controller
    {
        private readonly IFrameworkApiService _frameworkApiService;
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly ILookupApiService _lookupApiService;
        private readonly IClientValidationService _clientValidationService;

        public FrameworkSearchResultController(
            IFrameworkApiService frameworkApiService,
            ISearchModelFactory searchModelFactory,
            ILookupApiService lookupApiService,
            IClientValidationService clientValidationService)
        {
            _frameworkApiService = frameworkApiService;
            _searchModelFactory = searchModelFactory;
            _lookupApiService = lookupApiService;
            _clientValidationService = clientValidationService;
        }

        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
            var model = await PopulateViewModel(basicSearchModel);

            return View(model);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromForm]FrameworkSearchModel searchModel)
        {
            var model = new FrameworkSearchResultsViewModel();

            ValidateSearch(searchModel, model);
            if (model.ValidationErrors.Any())
            {
                model.SearchModel = searchModel;
                model.LookUpModel = await _lookupApiService.GetLookups();
                return View("Index", model);
            }

            model = await PopulateViewModel(null, searchModel);

            return View("Index", model);
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(int frameworkCode, int programType, int pathwayCode)
        {
            return RedirectToAction("Index", "FrameworkDetail", new { frameworkCode, programType, pathwayCode });
        }

        [HttpGet("ClearFilters")]
        public async Task<IActionResult> ClearFilters(string searchTerm)
        {
            var model = await PopulateViewModel(null, new FrameworkSearchModel { SearchTerm = searchTerm });
            return View("Index", model);
        }

        private void ValidateSearch(FrameworkSearchModel searchModel, FrameworkSearchResultsViewModel viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }
        }

        private async Task<FrameworkSearchResultsViewModel> PopulateViewModel(
            BasicSearchModel basicSearchModel = null,
            FrameworkSearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = _searchModelFactory.GetFrameworkSearchModel(basicSearchModel);
            }

            var frameworksTask = _frameworkApiService.GetFrameworks(searchModel);
            var lookupsTask = _lookupApiService.GetLookups();

            await Task.WhenAll(frameworksTask, lookupsTask);

            var frameworks = frameworksTask.Result;
            var lookups = lookupsTask.Result;

            return new FrameworkSearchResultsViewModel
            {
                SearchModel = searchModel,
                FrameworkModels = frameworks.ToList(),
                LookUpModel = lookups
            };
        }
    }
}