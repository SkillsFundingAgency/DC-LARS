using System;

namespace ESFA.DC.LARS.API.Models
{
    public class CategoryModel
    {
        public int Reference { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ParentReference { get; set; }

        public string ParentDescription { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}