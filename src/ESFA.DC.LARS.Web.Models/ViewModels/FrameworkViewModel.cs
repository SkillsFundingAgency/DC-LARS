using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class FrameworkViewModel
    {
        public LearningAimModel LearningAim { get; set; }

        public List<BreadcrumbsModel> Breadcrumbs => new List<BreadcrumbsModel>
        {
            new BreadcrumbsModel { Id = "searchResultsLink", Text = "Search Results" },
            new BreadcrumbsModel { Id = "learningAimDetailLink", Text = LearningAim.LearningAimTitle },
            new BreadcrumbsModel { Text = "Frameworks" },
        };
    }
}
