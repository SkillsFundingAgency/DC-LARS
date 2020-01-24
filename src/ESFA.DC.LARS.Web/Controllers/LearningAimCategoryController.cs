using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("LearningAimCategories")]
    public class LearningAimCategoryController : Controller
    {
        private readonly ILearningAimsApiService _learningAimsApiService;

        public LearningAimCategoryController(
            ILearningAimsApiService learningAimsApiService)
        {
            _learningAimsApiService = learningAimsApiService;
        }

        [Route("{learnAimRef}")]
        public async Task<IActionResult> Index(string learnAimRef)
        {
            var viewModel = new LearningAimsCategoryViewModel
            {
                LearningAimModel = await _learningAimsApiService.GetLearningAim(learnAimRef)
            };

            return View(viewModel);
        }
    }
}