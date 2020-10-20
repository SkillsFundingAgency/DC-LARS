using System;

namespace ESFA.DC.LARS.Azure.Models
{
    public class EntitlementCategoryModel
    {
        public string LearnAimRef { get; set; }

        public string Category2Description { get; set; }

        public string Category3Description { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}