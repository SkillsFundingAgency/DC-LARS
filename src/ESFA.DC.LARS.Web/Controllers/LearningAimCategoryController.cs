using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("LearningAimCategories")]
    public class LearningAimCategoryController : AbstractAimCategoryController
    {
        public LearningAimCategoryController(ILearningAimsApiService learningAimsApiService)
            : base(learningAimsApiService)
        {
        }
    }
}