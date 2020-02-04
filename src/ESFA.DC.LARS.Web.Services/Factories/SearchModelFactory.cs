using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Factories
{
    public class SearchModelFactory : ISearchModelFactory
    {
        public SearchModel GetSearchModel(BasicSearchModel basicSearchModel)
        {
            var searchModel = new SearchModel();

            if (!string.IsNullOrEmpty(basicSearchModel.SearchTerm))
            {
                searchModel.SearchTerm = basicSearchModel.SearchTerm;
            }

            if (!string.IsNullOrEmpty(basicSearchModel.Level))
            {
                searchModel.Levels.Add(basicSearchModel.Level);
            }

            if (!string.IsNullOrEmpty(basicSearchModel.AwardingBody))
            {
                searchModel.AwardingBodies.Add(basicSearchModel.AwardingBody);
            }

            return searchModel;
        }
    }
}