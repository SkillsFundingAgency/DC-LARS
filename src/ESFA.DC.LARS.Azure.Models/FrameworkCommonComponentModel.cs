using System;

namespace ESFA.DC.LARS.Azure.Models
{
    public class FrameworkCommonComponentModel
    {
        public int CommonComponent { get; set; }

        public int ProgramType { get; set; }

        public int PathwayCode { get; set; }

        public int FrameworkCode { get; set; }

        public string Description { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string MinLevel { get; set; }
    }
}
