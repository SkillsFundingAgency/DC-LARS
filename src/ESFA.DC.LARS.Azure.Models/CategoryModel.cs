using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    [ExcludeFromCodeCoverage]
    public class CategoryModel
    {
        [IsFilterable]
        public int Reference { get; set; }

        [IsSearchable]
        public string Title { get; set; }

        [IsSearchable]
        public string Description { get; set; }

        [IsFilterable]
        public int ParentReference { get; set; }

        [IsSearchable]
        public string ParentDescription { get; set; }

        [IsFilterable]
        public DateTime? EffectiveFrom { get; set; }

        [IsFilterable]
        public DateTime? EffectiveTo { get; set; }
    }
}