using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
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

        [IsFilterable]
        public string Level { get; set; }

        public string LevelDescription { get; set; }

        public string Type { get; set; }

        public int GuidedLearningHours { get; set; }

        [IsFilterable]
        public string AwardingBodyCode { get; set; }

        [IsFilterable]
        public string AwardingBodyName { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<FundingModel> FundingModels { get; set; }

        public List<ValidityModel> ValidityModels { get; set; }

        public List<AcademicYearModel> AcademicYears { get; set; }
    }
}