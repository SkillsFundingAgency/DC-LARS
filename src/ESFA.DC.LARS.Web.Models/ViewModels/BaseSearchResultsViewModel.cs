using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public abstract class BaseSearchResultsViewModel
    {
        public abstract string SearchTerm { get; }

        public List<string> ValidationErrors { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public BreadcrumbsModel Breadcrumbs { get; set; }
    }
}
