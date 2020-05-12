using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("FrameworkSearchResult")]
    public class FrameworkSearchResultController : BaseResultsController<FrameworkSearchModel, FrameworkModel>
    {
        private const string ResultsTemplate = "_SearchResults";

        private readonly IFrameworkApiService _frameworkApiService;
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly IClientValidationService _clientValidationService;

        public FrameworkSearchResultController(
            IFrameworkApiService frameworkApiService,
            ISearchModelFactory searchModelFactory,
            ILookupApiService lookupApiService,
            IClientValidationService clientValidationService)
            : base(lookupApiService, ResultsTemplate, LearningType.Frameworks)
        {
            _frameworkApiService = frameworkApiService;
            _searchModelFactory = searchModelFactory;
            _clientValidationService = clientValidationService;
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

        protected override Task<IEnumerable<FrameworkModel>> GetSearchResults(FrameworkSearchModel searchModel)
        {
            return _frameworkApiService.GetFrameworks(searchModel);
        }

        protected override FrameworkSearchModel GetSearchModel(BasicSearchModel basicSearchModel)
        {
            return _searchModelFactory.GetFrameworkSearchModel(basicSearchModel);
        }

        protected override void ValidateSearch(FrameworkSearchModel searchModel, SearchResultsViewModel<FrameworkSearchModel, FrameworkModel> viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }
        }
    }
}