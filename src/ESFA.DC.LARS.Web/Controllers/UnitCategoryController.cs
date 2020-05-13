using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public class UnitCategoryController : Controller
    {
        private readonly IUnitsApiService _unitsApiService;

        public UnitCategoryController(
            IUnitsApiService unitsApiService)
        {
            _unitsApiService = unitsApiService;
        }

        [Route("{learnAimRef}")]
        public async Task<IActionResult> Index(string learnAimRef)
        {
            var viewModel = new LearningAimsCategoryViewModel
            {
                LearningAimModel = await _unitsApiService.GetLearningAim(learnAimRef)
            };

            return View(viewModel);
        }
    }
}