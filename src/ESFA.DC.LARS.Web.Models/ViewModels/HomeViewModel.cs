using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public LearningType SearchType { get; set; }

        public List<SelectListItem> SearchTypeList { get; set; }
    }
}