﻿using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.ODataFilters
{
    public class AwardingBodyODataFilter : BaseFilter, ILearningAimsODataFilter
    {
        protected override string FilterODataString => "AwardingBodyCode eq '{0}' or AwardingBodyName eq '{0}'";

        public string ApplyFilter(LearningAimsSearchModel searchModel)
        {
            return ApplyFilter(searchModel.AwardingBodies);
        }
    }
}