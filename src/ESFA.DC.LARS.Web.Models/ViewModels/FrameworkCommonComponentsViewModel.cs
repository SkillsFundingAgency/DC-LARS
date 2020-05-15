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
                Id = "learningAimBreadcrumbs",
                Breadcrumbs = new Dictionary<string, string>()
                {
                    { "homeLink", "Home" },
                    { "searchResultsLink", "Search Results" },
                    { "frameworkTitleLink", Framework.FrameworkTitle },
                    { "commonComponentLink", "Common components" }
                }
            }
        };
    }
}
