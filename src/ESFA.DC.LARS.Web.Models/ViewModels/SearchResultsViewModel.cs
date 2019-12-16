using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class SearchResultsViewModel
    {
        public SearchModel SearchModel { get; set; }

        public IEnumerable<LearningAimModel> LearningAimModels { get; set; }
    }
}