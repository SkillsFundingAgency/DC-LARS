using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimDetailsViewModel
    {
        public string AcademicYear { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public LearningAimModel LearningAimModel { get; set; }

        public BreadcrumbsModel LearningAimBreadcrumbs => new BreadcrumbsModel()
        {
            Id = "learningAimBreadcrumbs",
            Breadcrumbs = new Dictionary<string, string>()
            {
                { "homeLink", "Home" },
                { "searchResultsLink", "Search Results" },
                { "learningAimDetailLink", LearningAimModel.LearningAimTitle }
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
                { "learningAimDetailLink", LearningAimModel.LearningAimTitle }
            },
        };
    }
}