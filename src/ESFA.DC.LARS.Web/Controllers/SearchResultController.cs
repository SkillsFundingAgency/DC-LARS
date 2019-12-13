using System.Collections.Generic;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("SearchResult")]
    public class SearchResultController : Controller
    {
        public IActionResult Index(IEnumerable<LearningAimModel> learningAims = null)
        {
            var model = new SearchResultsViewModel
            {
                LearningAimModels = learningAims
            };

            return View(model);
        }
    }
}