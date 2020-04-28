using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("LearningAimDetails")]
    public class LearningAimDetailController : AbstractAimDetailController
    {
        public LearningAimDetailController(
            ILearningAimsApiService learningAimsApiService,
            ILookupApiService lookupApiService)
        : base(learningAimsApiService, lookupApiService)
        {
            FormController = "LearningAimDetail";
            CategoryController = "LearningAimCategory";
        }
    }
}