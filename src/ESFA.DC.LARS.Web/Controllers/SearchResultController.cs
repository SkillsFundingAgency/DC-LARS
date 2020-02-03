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

        public SearchResultController(
            ISearchModelFactory searchModelFactory,
            ILearningAimsApiService learningAimsApiService)
        {
            _searchModelFactory = searchModelFactory;
            _learningAimsApiService = learningAimsApiService;
        }

        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
            var searchModel = _searchModelFactory.GetSearchModel(basicSearchModel);

            var learningAims = await _learningAimsApiService.GetLearningAims(searchModel);

            var model = new SearchResultsViewModel
            {
                SearchModel = searchModel,
                LearningAimModels = learningAims
            };

            return View(model);
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(string learnAimRef)
        {
            return RedirectToAction("Index", "LearningAimDetail", new { learnAimRef });
        }
    }
}