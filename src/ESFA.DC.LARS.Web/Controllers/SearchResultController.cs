using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("SearchResult")]
    public class SearchResultController : Controller
    {
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly ILearningAimsApiService _learningAimsApiService;
        private readonly ILookupApiService _lookupApiService;

        public SearchResultController(
            ISearchModelFactory searchModelFactory,
            ILearningAimsApiService learningAimsApiService,
            ILookupApiService lookupApiService)
        {
            _searchModelFactory = searchModelFactory;
            _learningAimsApiService = learningAimsApiService;
            _lookupApiService = lookupApiService;
        }

        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
            var model = await PopulateViewModel(basicSearchModel);

            return View(model);
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(string learnAimRef)
        {
            return RedirectToAction("Index", "LearningAimDetail", new { learnAimRef });
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromForm]SearchModel searchModel)
        {
            var model = await PopulateViewModel(null, searchModel);

            return View("Index", model);
        }

        private async Task<SearchResultsViewModel> PopulateViewModel(
            BasicSearchModel basicSearchModel = null,
            SearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = _searchModelFactory.GetSearchModel(basicSearchModel);
            }

            var learningAims = await _learningAimsApiService.GetLearningAims(searchModel);

            var lookups = await _lookupApiService.GetLookups();

            return new SearchResultsViewModel
            {
                SearchModel = searchModel,
                LearningAimModels = learningAims,
                LookUpModel = lookups
            };
        }
    }
}