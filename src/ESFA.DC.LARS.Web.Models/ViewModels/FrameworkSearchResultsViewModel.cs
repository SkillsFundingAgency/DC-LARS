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

        public IEnumerable<FrameworkModel> FrameworkModels { get; set; }
    }
}