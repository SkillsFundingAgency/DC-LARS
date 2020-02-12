using System;

namespace ESFA.DC.LARS.API.Models
{
    public class ValidityFundingMappingLookupModel
    {
        public string ValidityCategory { get; set; }

        public string FundingCategory { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}