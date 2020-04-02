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

        public List<BreadcrumbsModel> Breadcrumbs => new List<BreadcrumbsModel>
        {
            new BreadcrumbsModel { Id = "homeLink", Text = "Home" },
            new BreadcrumbsModel { Id = "searchResultsLink", Text = "Search Results" },
        };
    }
}