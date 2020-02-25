using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class SearchResultsViewModel
    {
        public SearchResultsViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; set; }

        public SearchModel SearchModel { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public IEnumerable<LearningAimModel> LearningAimModels { get; set; }
    }
}