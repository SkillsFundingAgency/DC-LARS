using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("Frameworks")]
    public class FrameworkController : Controller
    {
        private readonly ILearningAimsApiService _learningAimsApiService;

        public FrameworkController(ILearningAimsApiService learningAimsApiService)
        {
            _learningAimsApiService = learningAimsApiService;
        }

        [Route("{learnAimRef}")]
        public async Task<IActionResult> Index(string learnAimRef)
        {
            var model = await GetData(learnAimRef);

            return View(model);
        }

        private async Task<FrameworkViewModel> GetData(string learnAimRef)
        {
            var learningAim = await _learningAimsApiService.GetLearningAim(learnAimRef);

            return new FrameworkViewModel
            {
                LearningAim = learningAim
            };
        }
    }
}
