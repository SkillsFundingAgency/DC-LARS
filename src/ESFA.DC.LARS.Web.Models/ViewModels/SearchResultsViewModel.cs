using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class SearchResultsViewModel<TSearchModel, TResultsModel>
    {
        public SearchResultsViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; set; }

        public LookUpModel LookUpModel { get; set; }

        public BreadcrumbsModel Breadcrumbs { get; set; }

        public TSearchModel SearchModel { get; set; }

        public IEnumerable<TResultsModel> Results { get; set; }
    }
}
