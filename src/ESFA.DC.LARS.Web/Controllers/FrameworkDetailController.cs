using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("FrameworkDetails")]
    public class FrameworkDetailController : AbstractFrameworkDetailController
    {
        private const string CommonComponentController = "FrameworkCommonComponent";

        public FrameworkDetailController(IFrameworkApiService frameworkApiService)
            : base(frameworkApiService, CommonComponentController)
        {
        }
    }
}