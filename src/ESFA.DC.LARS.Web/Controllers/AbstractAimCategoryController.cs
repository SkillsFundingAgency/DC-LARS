using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public abstract class AbstractAimCategoryController : Controller
    {
        private readonly ILearningAimsApiService _learningAimsApiService;

        public AbstractAimCategoryController(ILearningAimsApiService learningAimsApiService)
        {
            _learningAimsApiService = learningAimsApiService;
        }

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
