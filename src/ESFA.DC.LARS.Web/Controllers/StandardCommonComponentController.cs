using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("StandardCommonComponents")]
    public class StandardCommonComponentController : Controller
    {
        private readonly IStandardApiService _standardApiService;

        public StandardCommonComponentController(IStandardApiService standardApiService)
        {
            _standardApiService = standardApiService;
        }

        [Route("{standardCode}")]
        public async Task<IActionResult> Index(string standardCode)
        {
            var standard = await _standardApiService.GetStandard(standardCode);

            var model = new StandardCommonComponentsViewModel
            {
                Standard = standard
            };

            return View(model);
        }
    }
}