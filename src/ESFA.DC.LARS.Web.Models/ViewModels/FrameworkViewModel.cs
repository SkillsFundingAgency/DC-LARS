using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class FrameworkViewModel
    {
        public LearningAimModel LearningAim { get; set; }

        public BreadcrumbsModel LearningAimBreadcrumbs => new BreadcrumbsModel()
        {
            Id = "learningAimBreadcrumbs",
            Breadcrumbs = new Dictionary<string, string>()
            {
                { "homeLink", "Home" },
                { "searchResultsLink", "Search Results" },
                { "learningAimDetailLink", LearningAim.LearningAimTitle },
                { "frameworksLink", "Frameworks" }
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
                { "learningAimDetailLink", LearningAim.LearningAimTitle },
                { "frameworksLink", "Frameworks" }
            },
        };
    }
}
