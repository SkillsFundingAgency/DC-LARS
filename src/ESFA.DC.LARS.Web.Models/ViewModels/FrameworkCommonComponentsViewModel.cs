using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class FrameworkCommonComponentsViewModel
    {
        public FrameworkModel Framework { get; set; }

        public List<BreadcrumbsModel> Breadcrumbs => new List<BreadcrumbsModel>()
        {
            new BreadcrumbsModel
            {
                Id = "frameworksBreadcrumbs",
                Breadcrumbs = new Dictionary<string, string>()
                {
                    { "homeLink", "Home" },
                    { "frameworksSearchResultsLink", "Search Results" },
                    { "pathwaysLink", "Pathways" },
                    { "commonComponentLink", "Common components" }
                }
            }
        };
    }
}
