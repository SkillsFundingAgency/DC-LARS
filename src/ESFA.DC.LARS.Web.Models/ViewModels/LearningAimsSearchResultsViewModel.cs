using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimsSearchResultsViewModel
    {
        public LearningAimsSearchResultsViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; set; }

        public LearningAimsSearchModel SearchModel { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public IEnumerable<LearningAimModel> LearningAimModels { get; set; }

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