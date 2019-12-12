using System;
using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.API.Models
{
    [ExcludeFromCodeCoverage]
    public class FundingModel
    {
        public string LearnAimRef { get; set; }

        public string FundingCategoryDescription { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public decimal RateWeighted { get; set; }

        public decimal RateUnWeighted { get; set; }

        public string WeightingFactor { get; set; }
    }
}