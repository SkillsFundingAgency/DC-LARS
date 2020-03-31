using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimDetailsViewModel
    {
        public string AcademicYear { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public LearningAimModel LearningAimModel { get; set; }

        public List<BreadcrumbsModel> Breadcrumbs => new List<BreadcrumbsModel>
        {
            new BreadcrumbsModel { Id = "homeLink", Text = "Home" },
            new BreadcrumbsModel { Id = "searchResultsLink", Text = "Search Results" },
            new BreadcrumbsModel { Text = LearningAimModel.LearningAimTitle },
        };
    }
}