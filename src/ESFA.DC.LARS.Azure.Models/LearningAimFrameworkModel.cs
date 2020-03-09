using System;

namespace ESFA.DC.LARS.Azure.Models
{
    public class LearningAimFrameworkModel
    {
        public string LearnAimRef { get; set; }

        public string LearningAimTitle { get; set; }

        public string FrameworkTitle { get; set; }

        public int FrameworkCode { get; set; }

        public int ProgramType { get; set; }

        public int PathwayCode { get; set; }

        public string ProgramTypeDesc { get; set; }

        public string PathwayName { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string IssuingAuthority { get; set; }

        public string IssuingAuthorityDesc { get; set; }

        public int? ComponentType { get; set; }

        public string ComponentTypeDesc { get; set; }
    }
}