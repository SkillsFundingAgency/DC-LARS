﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    public class FrameworkModel
    {
        [Key]
        public string Id { get; set; }

        public int FrameworkCode { get; set; }

        [IsSearchable]
        public string SearchableFrameworkCode { get; set; }

        [IsFilterable]
        public int ProgramType { get; set; }

        public int PathwayCode { get; set; }

        [IsSearchable]
        public string FrameworkTitle { get; set; }

        public string PathwayName { get; set; }

        public string ProgramTypeName { get; set; }

        public bool IsTLevel { get; set; }

        [IsFilterable]
        public string IssuingAuthority { get; set; }

        public string IssuingAuthorityDesc { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        [IsFilterable]
        public string SectorSubjectAreaTier1 { get; set; }

        public string SectorSubjectAreaTier1Desc { get; set; }

        public string SectorSubjectAreaTier2 { get; set; }

        public string SectorSubjectAreaTier2Desc { get; set; }

        public List<RelatedLearningAimModel> LearningAims { get; set; }

        public List<CommonComponentModel> CommonComponents { get; set; }

        public string NasTitle { get; set; }
    }
}