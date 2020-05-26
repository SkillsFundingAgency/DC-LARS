using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using StandardModel = ESFA.DC.LARS.Web.Models.StandardModel;
using StandardSearchModel = ESFA.DC.LARS.Web.Models.StandardSearchModel;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("StandardsSearchResult")]
    public class StandardsSearchResultController : AbstractResultsController<StandardSearchModel, StandardModel>
    {
        private const string ResultsTemplate = "_SearchResults";

        private readonly IStandardApiService _standardApiService;
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly IClientValidationService _clientValidationService;

        public StandardsSearchResultController(
            IStandardApiService standardApiService,
            ISearchModelFactory searchModelFactory,
            ILookupApiService lookupApiService,
            IClientValidationService clientValidationService,
            IEnumerable<ISearchResultsRouteStrategy> resultRouteStrategies)
            : base(resultRouteStrategies, lookupApiService, ResultsTemplate, LearningType.Standards)
        {
            _standardApiService = standardApiService;
            _searchModelFactory = searchModelFactory;
            _clientValidationService = clientValidationService;
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(string standardCode)
        {
            return RedirectToAction("Index", "StandardDetail", new { standardCode });
        }

        [HttpGet("ClearFilters")]
        public async Task<IActionResult> ClearFilters(string searchTerm)
        {
            var model = await PopulateViewModel(null, new StandardSearchModel { SearchTerm = searchTerm, SearchType = _searchType });
            return View("Index", model);
        }

        protected override Task<IEnumerable<StandardModel>> GetSearchResults(StandardSearchModel searchModel)
        {
            return _standardApiService.GetStandards(searchModel);
        }

        protected override StandardSearchModel GetSearchModel(BasicSearchModel basicSearchModel)
        {
            return _searchModelFactory.GetStandardSearchModel(basicSearchModel);
        }

        protected override void ValidateSearch(StandardSearchModel searchModel, SearchResultsViewModel<StandardSearchModel, StandardModel> viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }
        }
    }
}