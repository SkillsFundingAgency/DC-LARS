using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class SearchResultsViewModel<TSearchModel, TResultsModel> : AbstractSearchResultsViewModel
        where TSearchModel : AbstractSearchModel
    {
        public SearchResultsViewModel()
        {
            ValidationErrors = new List<string>();
        }

        public TSearchModel SearchModel { get; set; }

        public List<TResultsModel> Results { get; set; }

        public override string SearchTerm => SearchModel.SearchTerm;

        public override LearningType? SearchType => SearchModel.SearchType;
    }
}
