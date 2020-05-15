using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Strategies
{
    public class StandardsResultsRouteStrategy : ISearchResultsRouteStrategy
    {
        public LearningType SearchType => LearningType.Standards;

        public string Action => "Index";

        public string Controller => "StandardsSearchResult";
    }
}
