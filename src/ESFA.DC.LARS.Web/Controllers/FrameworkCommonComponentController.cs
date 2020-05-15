using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("FrameworkCommonComponents")]
    public class FrameworkCommonComponentController : Controller
    {
        private readonly IFrameworkApiService _frameworkApiService;

        public FrameworkCommonComponentController(IFrameworkApiService frameworkApiService)
        {
            _frameworkApiService = frameworkApiService;
        }

        [Route("{frameworkCode}/{programType}/{pathwayCode}")]
        public async Task<IActionResult> Index(int frameworkCode, int programType, int pathwayCode)
        {
            var framework = await _frameworkApiService.GetFramework(frameworkCode, programType, pathwayCode);

            var model = new FrameworkCommonComponentsViewModel
            {
                Framework = framework
            };

            return View(model);
        }
    }
}