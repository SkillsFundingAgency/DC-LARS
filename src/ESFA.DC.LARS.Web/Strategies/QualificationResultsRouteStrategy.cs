﻿using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Strategies
{
    public class QualificationResultsRouteStrategy : ISearchResultsRouteStrategy
    {
        public LearningType SearchType => LearningType.Qualifications;

        public (string Action, string Controller) Route => (Action: "Index", Controller: "LearningAimSearchResult");
    }
}
