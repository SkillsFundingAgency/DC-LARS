﻿using System;

namespace ESFA.DC.LARS.Web.Models
{
    public class ValidityFundingMappingLookupModel
    {
        public string ValidityCategory { get; set; }

        public string FundingCategory { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}