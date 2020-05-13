using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("UnitSearchResult")]
    public class UnitSearchResultController : BaseResultsController<LearningAimsSearchModel, LearningAimModel>
    {
        private const string ResultsTemplate = "_LearningAimsResults";

        private readonly ISearchModelFactory _searchModelFactory;
        private readonly IUnitsApiService _unitsApiService;
        private readonly IClientValidationService _clientValidationService;

        public UnitSearchResultController(
            ISearchModelFactory searchModelFactory,
            IUnitsApiService unitsApiService,
            ILookupApiService lookupApiService,
            IClientValidationService clientValidationService)
            : base(lookupApiService, ResultsTemplate, LearningType.Units)
        {
            _searchModelFactory = searchModelFactory;
            _unitsApiService = unitsApiService;
            _clientValidationService = clientValidationService;
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(string learnAimRef, string academicYear)
        {
            return RedirectToAction(nameof(UnitDetailController.Index), "UnitDetail", new { learnAimRef, academicYear });
        }

        [HttpGet("ClearFilters")]
        public async Task<IActionResult> ClearFilters(string searchTerm, string academicYear)
        {
            var model = await PopulateViewModel(null, new LearningAimsSearchModel { SearchTerm = searchTerm, TeachingYears = new List<string> { academicYear }, SearchType = _searchType });
            return View("Index", model);
        }

        protected override LearningAimsSearchModel GetSearchModel(BasicSearchModel basicSearchModel)
        {
            return _searchModelFactory.GetLearningAimsSearchModel(basicSearchModel);
        }

        protected override Task<IEnumerable<LearningAimModel>> GetSearchResults(LearningAimsSearchModel searchModel)
        {
            return _unitsApiService.GetLearningAims(searchModel);
        }

        protected override void ValidateSearch(LearningAimsSearchModel searchModel, SearchResultsViewModel<LearningAimsSearchModel, LearningAimModel> viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }
        }
    }
}