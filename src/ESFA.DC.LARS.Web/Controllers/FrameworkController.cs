using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
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

            //return new FrameworkViewModel()
            //{
            //    LearningAim = new LearningAimModel()
            //    {
            //        LearningAimTitle = "Learning Aim Title",
            //        LearnAimRef = learnAimRef
            //    },
            //    Frameworks = new List<LearningAimFrameworkModel>()
            //    {
            //        new LearningAimFrameworkModel()
            //        {
            //            FrameworkTitle = "Framework Title",
            //            IssuingAuthority = "100",
            //            FrameworkCode = 1,
            //            ProgramType = 2,
            //            ProgramTypeDesc = "Prog type desc",
            //            PathwayCode = 3,
            //            PathwayName = "Pathway Name",
            //            EffectiveFrom = new DateTime(2000, 01, 01),
            //            EffectiveTo = new DateTime(2001, 02, 01),
            //            ComponentType = 4,
            //            ComponentTypeDesc = "Component type desc",
            //            IssuingAuthorityDesc = "Issuing authority desc"
            //        },
            //        new LearningAimFrameworkModel()
            //        {
            //            FrameworkTitle = "Framework Title 2",
            //            IssuingAuthority = "100",
            //            FrameworkCode = 1,
            //            ProgramType = 2,
            //            ProgramTypeDesc = "Prog type desc",
            //            PathwayCode = 3,
            //            PathwayName = "Pathway Name",
            //            EffectiveFrom = new DateTime(2000, 01, 01),
            //            EffectiveTo = new DateTime(2001, 02, 01),
            //            ComponentType = 4,
            //            ComponentTypeDesc = "Component type desc",
            //            IssuingAuthorityDesc = "Issuing authority desc"
            //        }
            //    }
            //};
        }
    }
}
