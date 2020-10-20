using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("FrameworkCommonComponents")]
    public class FrameworkCommonComponentController : AbstractFrameworkComponentController
    {
        public FrameworkCommonComponentController(IFrameworkApiService frameworkApiService)
            : base(frameworkApiService)
        {
        }
    }
}