using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Extensions;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public abstract class BaseResultsController<TSearchModel, TResults> : Controller
        where TSearchModel : BaseSearchModel
        where TResults : class
    {
        private readonly LearningType _searchType;
        private readonly ILookupApiService _lookupApiService;
        private readonly string _resultsTemplate;

        public BaseResultsController(
            ILookupApiService lookupApiService, string resultsTemplate, LearningType searchType)
        {
            _lookupApiService = lookupApiService;
            _resultsTemplate = resultsTemplate;
            _searchType = searchType;
        }

        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
            var model = await PopulateViewModel(basicSearchModel, null);
            return View(model);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromForm]TSearchModel searchModel)
        {
            if (searchModel.SearchType.HasValue
                && _searchType != searchModel.SearchType)
            {
                return RedirectToNewSearch(searchModel);
            }

            var model = new SearchResultsViewModel<TSearchModel, TResults>();

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

        [HttpGet("Results")]
        public async Task<IActionResult> Results([FromQuery]TSearchModel searchModel)
        {
            var resultsModel = new SearchResultsViewModel<TSearchModel, TResults>
            {
                SearchModel = searchModel,
                Results = new List<TResults>()
            };

            ValidateSearch(searchModel, resultsModel);

            if (!resultsModel.ValidationErrors.Any())
            {
                var results = await GetSearchResults(searchModel);
                resultsModel.Results = results.ToList();
            }

            var partialViewHtml = await this.RenderViewAsync(_resultsTemplate, resultsModel, true);

            return Json(new
            {
                data = partialViewHtml,
                count = resultsModel.Results.Count(),
                validationErrors = resultsModel.ValidationErrors
            });
        }

        protected async Task<SearchResultsViewModel<TSearchModel, TResults>> PopulateViewModel(BasicSearchModel basicSearchModel = null, TSearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = GetSearchModel(basicSearchModel);
            }

            var learningAimsTask = GetSearchResults(searchModel);
            var lookupsTask = _lookupApiService.GetLookups();

            await Task.WhenAll(learningAimsTask, lookupsTask);

            var learningAims = learningAimsTask.Result.ToList();
            var lookups = lookupsTask.Result;

            return new SearchResultsViewModel<TSearchModel, TResults>
            {
                SearchModel = searchModel,
                Results = learningAims,
                LookUpModel = lookups,
                Breadcrumbs = new BreadcrumbsModel()
                {
                    Id = "ResultsBreadcrumb",
                    Breadcrumbs = new Dictionary<string, string>()
                    {
                        { "homeLink", "Home" },
                        { "searchResultsLink", "Search Results" }
                    }
                }
            };
        }

        protected abstract Task<IEnumerable<TResults>> GetSearchResults(TSearchModel searchModel);

        protected abstract TSearchModel GetSearchModel(BasicSearchModel basicSearchModel);

        protected abstract void ValidateSearch(TSearchModel searchModel, SearchResultsViewModel<TSearchModel, TResults> viewModel);

        private IActionResult RedirectToNewSearch(TSearchModel searchModel)
        {
            var basicSearchModel = new BasicSearchModel
            {
                SearchTerm = searchModel.SearchTerm
            };

            switch (searchModel.SearchType)
            {
                // redirect to action based on the search Type.
                case LearningType.Qualifications:
                    return RedirectToAction("Index", "LearningAimSearchResult", basicSearchModel);
                case LearningType.Frameworks:
                    return RedirectToAction("Index", "FrameworkSearchResult", basicSearchModel);
                case LearningType.Units:
                    return RedirectToAction("Index", "UnitSearchResult", basicSearchModel);
                default:
                    return RedirectToAction("Index", "LearningAimSearchResult", basicSearchModel);
            }
        }
    }
}