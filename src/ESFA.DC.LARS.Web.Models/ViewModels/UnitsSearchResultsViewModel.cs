using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class UnitsSearchResultsViewModel : LearningAimsResultsViewModel
    {
        public UnitsSearchResultsViewModel()
            : base()
        {
            LearningAimBreadcrumbs = new BreadcrumbsModel()
            {
                Id = "unitsBreadcrumbs",
                Breadcrumbs = new Dictionary<string, string>()
                {
                    { "homeLink", "Home" },
                    { "searchResultsLink", "Search Results" }
                },
            };
        }
    }
}
