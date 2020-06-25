using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("TLevelSearchResult")]
    public class TLevelSearchResultController : AbstractResultsController<FrameworkSearchModel, FrameworkModel>
    {
        private const string ResultsTemplate = "_FrameworkSearchResults";
        private readonly ITLevelApiService _tlevelsApiService;
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly IClientValidationService _clientValidationService;

        public TLevelSearchResultController(
            ISearchModelFactory searchModelFactory,
            ILookupApiService lookupApiService,
            ITLevelApiService itLevelsApiService,
            IEnumerable<ISearchResultsRouteStrategy> resultRouteStrategies,
            IClientValidationService clientValidationService)
            : base(resultRouteStrategies, lookupApiService, ResultsTemplate, LearningType.TLevels)
        {
            _tlevelsApiService = itLevelsApiService;
            _searchModelFactory = searchModelFactory;
            _clientValidationService = clientValidationService;
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(int frameworkCode, int programType, int pathwayCode)
        {
            return RedirectToAction("Index", "TLevelDetail", new { frameworkCode, programType, pathwayCode });
        }

        protected override FrameworkSearchModel GetSearchModel(BasicSearchModel basicSearchModel)
        {
            return _searchModelFactory.GetFrameworkSearchModel(basicSearchModel);
        }

        protected override Task<IEnumerable<FrameworkModel>> GetSearchResults(FrameworkSearchModel searchModel)
        {
            return _tlevelsApiService.GetFrameworks(searchModel);
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