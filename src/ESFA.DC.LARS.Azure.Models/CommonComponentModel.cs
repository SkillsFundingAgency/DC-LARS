using System;

namespace ESFA.DC.LARS.Azure.Models
{
    public class CommonComponentModel
    {
        public string Id { get; set; }

        public int CommonComponent { get; set; }

        public string Description { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string MinLevel { get; set; }
    }
}
