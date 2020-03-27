using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimsCategoryViewModel
    {
        public LearningAimModel LearningAimModel { get; set; }

        public List<BreadcrumbsModel> Breadcrumbs => new List<BreadcrumbsModel>
        {
            new BreadcrumbsModel { Id = "searchResultsLink", Text = "Search Results" },
            new BreadcrumbsModel { Id = "learningAimDetailLink", Text = LearningAimModel.LearningAimTitle },
            new BreadcrumbsModel { Text = "Category" },
        };
    }
}