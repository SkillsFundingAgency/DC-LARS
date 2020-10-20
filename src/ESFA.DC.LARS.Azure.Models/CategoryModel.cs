using System;
using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.Azure.Models
{
    [ExcludeFromCodeCoverage]
    public class CategoryModel
    {
        public string LearnAimRef { get; set; }

        public int Reference { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ParentReference { get; set; }

        public string ParentDescription { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}