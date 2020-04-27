using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimsResultsViewModel
    {
        public LearningAimsResultsViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; set; }

        public LearningAimsSearchModel SearchModel { get; set; }

        public IEnumerable<LearningAimModel> LearningAimModels { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public BreadcrumbsModel LearningAimBreadcrumbs { get; set; }
    }
}