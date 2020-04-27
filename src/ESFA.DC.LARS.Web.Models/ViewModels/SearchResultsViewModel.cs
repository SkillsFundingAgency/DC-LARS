using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class SearchResultsViewModel<TSearch, TResults>
    {
        public SearchResultsViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public BreadcrumbsModel Breadcrumbs { get; set; }

        public TSearch SearchModel { get; set; }

        public IEnumerable<TResults> Results { get; set; }
    }
}
