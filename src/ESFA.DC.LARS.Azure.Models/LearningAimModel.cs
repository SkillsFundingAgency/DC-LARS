using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    [ExcludeFromCodeCoverage]
    public class LearningAimModel
    {
        [Key]
        [IsSearchable]
        [IsSortable]
        public string LearnAimRef { get; set; }

        [IsSearchable]
        public string LearningAimTitle { get; set; }

        public string Level { get; set; }

        public string Type { get; set; }

        public int GuidedLearningHours { get; set; }

        public string AwardingBody { get; set; }

        public string Level2Category { get; set; }

        public string Level3Category { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<FundingModel> FundingModels { get; set; }
    }
}