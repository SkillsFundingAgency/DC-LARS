using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; set; }

        public LookUpModel Lookups { get; set; }
    }
}