using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class FrameworkSearchResultsViewModel
    {
        public FrameworkSearchResultsViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; set; }

        public FrameworkSearchModel SearchModel { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public List<FrameworkModel> FrameworkModels { get; set; }

        public BreadcrumbsModel LearningAimBreadcrumbs => new BreadcrumbsModel()
        {
            Id = "learningAimBreadcrumbs",
            Breadcrumbs = new Dictionary<string, string>()
            {
                { "homeLink", "Home" },
                { "searchResultsLink", "Search Results" }
            },
        };
    }
}