using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Strategies
{
    public class UnitResultsRouteStrategy : ISearchResultsRouteStrategy
    {
        public LearningType SearchType => LearningType.Units;

        public string Action => "Index";

        public string Controller => "UnitSearchResult";
    }
}
