using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("LearningAimDetails")]
    public class LearningAimDetailController : Controller
    {
        private readonly ILearningAimsApiService _learningAimsApiService;

        public LearningAimDetailController(ILearningAimsApiService learningAimsApiService)
        {
            _learningAimsApiService = learningAimsApiService;
        }

        [Route("{learnAimRef}")]
        public async Task<IActionResult> Index(string learnAimRef)
        {
            var learningAim = await _learningAimsApiService.GetLearningAim(learnAimRef);

            var model = new LearningAimDetailsViewModel
            {
                LearningAimModel = learningAim
            };

            return View(model);
        }
    }
}