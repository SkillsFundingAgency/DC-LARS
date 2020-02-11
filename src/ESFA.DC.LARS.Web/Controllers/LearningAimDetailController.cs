using System.Linq;
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
        private readonly ILookupApiService _lookupApiService;

        public LearningAimDetailController(
            ILearningAimsApiService learningAimsApiService,
            ILookupApiService lookupApiService)
        {
            _learningAimsApiService = learningAimsApiService;
            _lookupApiService = lookupApiService;
        }

        [Route("{learnAimRef}")]
        public async Task<IActionResult> Index(string learnAimRef)
        {
            var lookupsTask = _lookupApiService.GetLookups();
            var learningAimTask = _learningAimsApiService.GetLearningAim(learnAimRef);

            await Task.WhenAll(lookupsTask, learningAimTask);

            var lookups = lookupsTask.Result;
            var learningAim = learningAimTask.Result;

            var model = new LearningAimDetailsViewModel
            {
                AcademicYear = lookups.AcademicYearLookups.FirstOrDefault(l => l.IsCurrentAcademicYear)?.AcademicYear,
                LookUpModel = lookups,
                LearningAimModel = learningAim
            };

            return View(model);
        }
    }
}