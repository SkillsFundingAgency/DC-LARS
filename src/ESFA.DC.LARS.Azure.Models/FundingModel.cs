using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    [ExcludeFromCodeCoverage]
    public class FundingModel
    {
        [IsSearchable]
        public string LearnAimRef { get; set; }

        [IsSearchable]
        public string FundingCategoryDescription { get; set; }

        [IsFilterable]
        public DateTime EffectiveFrom { get; set; }

        [IsFilterable]
        public DateTime? EffectiveTo { get; set; }

        public string RateWeighted { get; set; }

        public string RateUnWeighted { get; set; }

        public string WeightingFactor { get; set; }
    }
}