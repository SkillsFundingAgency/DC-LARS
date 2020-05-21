using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Extensions;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public abstract class AbstractResultsController<TSearchModel, TResults> : Controller
        where TSearchModel : AbstractSearchModel
        where TResults : class
    {
        protected readonly LearningType _searchType;
        private readonly ILookupApiService _lookupApiService;
        private readonly string _resultsTemplate;
        private readonly IEnumerable<ISearchResultsRouteStrategy> _resultRouteStrategies;

        public AbstractResultsController(
            IEnumerable<ISearchResultsRouteStrategy> resultRouteStrategies,
            ILookupApiService lookupApiService,
            string resultsTemplate,
            LearningType searchType)
        {
            _lookupApiService = lookupApiService;
            _resultsTemplate = resultsTemplate;
            _searchType = searchType;
            _resultRouteStrategies = resultRouteStrategies;
        }

        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
            var model = await PopulateViewModel(basicSearchModel, null, ShouldIncludeSearchResults(basicSearchModel));
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

        protected async Task<SearchResultsViewModel<TSearchModel, TResults>> PopulateViewModel(BasicSearchModel basicSearchModel = null, TSearchModel searchModel = null, bool includeSearchResults = true)
        {
            if (searchModel == null)
            {
                searchModel = GetSearchModel(basicSearchModel);
            }

            searchModel.SearchType = _searchType;

            LookUpModel lookups;
            var results = new List<TResults>();

            if (includeSearchResults)
            {
                var learningAimsTask = GetSearchResults(searchModel);
                var lookupsTask = _lookupApiService.GetLookups();

                await Task.WhenAll(learningAimsTask, lookupsTask);

                results = learningAimsTask.Result.ToList();
                lookups = lookupsTask.Result;
            }
            else
            {
                lookups = await _lookupApiService.GetLookups();
            }

            return new SearchResultsViewModel<TSearchModel, TResults>
            {
                SearchModel = searchModel,
                Results = results,
                LookUpModel = lookups
            };
        }

        protected abstract Task<IEnumerable<TResults>> GetSearchResults(TSearchModel searchModel);

        protected abstract TSearchModel GetSearchModel(BasicSearchModel basicSearchModel);

        protected abstract void ValidateSearch(TSearchModel searchModel, SearchResultsViewModel<TSearchModel, TResults> viewModel);

        private IActionResult RedirectToNewSearch(TSearchModel searchModel)
        {
            var basicSearchModel = new BasicSearchModel
            {
                SearchTerm = searchModel.SearchTerm,
                TeachingYear = searchModel.TeachingYears?.FirstOrDefault()
            };

            var routeStrategy = _resultRouteStrategies.SingleOrDefault(r => r.SearchType == searchModel.SearchType);

            if (routeStrategy != null)
            {
                return RedirectToAction(routeStrategy.Action, routeStrategy.Controller, basicSearchModel);
            }

            return RedirectToAction("Index", basicSearchModel);
        }

        private bool ShouldIncludeSearchResults(BasicSearchModel basicSearchModel)
        {
            // Filter state is only stored clientside and to avoid HTTP Posts (which neagtively affect the back button)
            // we will not display results initially where we know filters have been applied.  The client code will know
            // to get the results once the  page has been loaded.
            if (basicSearchModel?.HasFilters == true)
            {
                return false;
            }

            return true;
        }
    }
}