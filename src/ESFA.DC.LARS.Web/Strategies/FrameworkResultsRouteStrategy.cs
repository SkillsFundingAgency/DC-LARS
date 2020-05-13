using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Strategies
{
    public class FrameworkResultsRouteStrategy : ISearchResultsRouteStrategy
    {
        public LearningType SearchType => LearningType.Frameworks;

        public (string Action, string Controller) Route => (Action: "Index", Controller: "FrameworkSearchResult");
    }
}
