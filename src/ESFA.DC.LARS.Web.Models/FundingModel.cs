using System;
using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class FundingModel
    {
        public string LearnAimRef { get; set; }

        public string FundingCategoryDescription { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string RateWeighted { get; set; }

        public string RateUnWeighted { get; set; }

        public string WeightingFactor { get; set; }
    }
}