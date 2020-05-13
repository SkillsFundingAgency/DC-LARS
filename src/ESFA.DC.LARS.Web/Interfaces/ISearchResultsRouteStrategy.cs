using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Interfaces
{
    public interface ISearchResultsRouteStrategy
    {
        public LearningType SearchType { get; }

        public (string Action, string Controller) Route { get; }
    }
}
