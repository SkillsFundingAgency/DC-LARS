using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("StandardsSearchResult")]
    public class StandardsSearchResultController : Controller
    {
        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
           return View();
        }
    }
}