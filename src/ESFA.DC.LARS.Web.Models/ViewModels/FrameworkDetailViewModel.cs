using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class FrameworkDetailViewModel
    {
        public FrameworkModel Framework { get; set; }

        public List<BreadcrumbsModel> Breadcrumbs => new List<BreadcrumbsModel>
        {
            new BreadcrumbsModel { Id = "searchResultsLink", Text = "Search Results" },
            new BreadcrumbsModel { Id = "learningAimDetailLink" }, // Text property is set client side.
            new BreadcrumbsModel { Id = "frameworksLink", Text = "Frameworks" },
            new BreadcrumbsModel { Text = "Pathways" },
        };
    }
}