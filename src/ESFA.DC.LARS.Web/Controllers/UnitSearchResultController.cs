using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;

namespace ESFA.DC.LARS.Web.Controllers
{
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
            : base(lookupApiService, ResultsTemplate)
        {
            _searchModelFactory = searchModelFactory;
            _unitsApiService = unitsApiService;
            _clientValidationService = clientValidationService;
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