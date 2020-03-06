using System;

namespace ESFA.DC.LARS.Web.Models
{
    public class FrameworkAimModel
    {
        public string LearnAimRef { get; set; }

        public string LearningAimTitle { get; set; }

        public string Level { get; set; }

        public string AwardingBodyCode { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public int? ComponentType { get; set; }

        public string ComponentTypeDesc { get; set; }
    }
}