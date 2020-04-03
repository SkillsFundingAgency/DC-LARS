using System;
using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class FrameworkDetailViewModel
    {
        public FrameworkModel Framework { get; set; }

        public BreadcrumbsModel LearningAimBreadcrumbs => new BreadcrumbsModel()
        {
            Id = "learningAimBreadcrumbs",
            Breadcrumbs = new Dictionary<string, string>()
            {
                { "homeLink", "Home" },
                { "searchResultsLink", "Search Results" },
                { "learningAimDetailLink", string.Empty },
                { "frameworksLink", "Frameworks" },
                { "pathwaysLink", "Pathways" },
            },
        };

        public BreadcrumbsModel FrameworksBreadcrumbs => new BreadcrumbsModel()
        {
            Id = "frameworksBreadcrumbs",
            Breadcrumbs = new Dictionary<string, string>()
            {
                { "homeLink", "Home" },
                { "frameworksSearchResultsLink", "Search Results" },
                { "pathwaysLink", "Pathways" },
            },
        };
    }
}