using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public class AbstractFrameworkDetailController : Controller
    {
        private readonly IFrameworkApiService _frameworkApiService;
        private readonly string _commonComponentController;

        public AbstractFrameworkDetailController(IFrameworkApiService frameworkApiService, string commonComponentController)
        {
            _frameworkApiService = frameworkApiService;
            _commonComponentController = commonComponentController;
        }

        [Route("{frameworkCode}/{programType}/{pathwayCode}")]
        public async Task<IActionResult> Index(int frameworkCode, int programType, int pathwayCode)
        {
            var framework = await _frameworkApiService.GetFramework(frameworkCode, programType, pathwayCode);

            var model = new FrameworkDetailViewModel
            {
                Framework = framework,
                CommonComponentController = _commonComponentController
            };

            return View(model);
        }
    }
}
