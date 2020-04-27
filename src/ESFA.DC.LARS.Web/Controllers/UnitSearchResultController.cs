using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public class UnitSearchResultController : Controller
    {
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly IUnitsApiService _unitsApiService;
        private readonly ILookupApiService _lookupApiService;

        public UnitSearchResultController(
            ISearchModelFactory searchModelFactory,
            IUnitsApiService unitsApiService,
            ILookupApiService lookupApiService)
        {
            _searchModelFactory = searchModelFactory;
            _unitsApiService = unitsApiService;
            _lookupApiService = lookupApiService;
        }

        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
            var model = await PopulateViewModel(basicSearchModel);
            return View(model);
        }

        private async Task<UnitsSearchResultsViewModel> PopulateViewModel(
                             BasicSearchModel basicSearchModel = null,
                             LearningAimsSearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = _searchModelFactory.GetLearningAimsSearchModel(basicSearchModel);
            }

            var learningAimsTask = _unitsApiService.GetLearningAims(searchModel);
            var lookupsTask = _lookupApiService.GetLookups();

            await Task.WhenAll(learningAimsTask, lookupsTask);

            var learningAims = learningAimsTask.Result;
            var lookups = lookupsTask.Result;

            return new UnitsSearchResultsViewModel
            {
                SearchModel = searchModel,
                LearningAimModels = learningAims,
                LookUpModel = lookups
            };
        }
    }
}