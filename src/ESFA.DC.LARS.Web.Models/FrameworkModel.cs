﻿using System;
using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class FrameworkModel
    {
        public int FrameworkCode { get; set; }

        public int ProgramType { get; set; }

        public int PathwayCode { get; set; }

        public string PathwayName { get; set; }

        public string ProgramTypeName { get; set; }

        public string FrameworkTitle { get; set; }

        public string IssuingAuthority { get; set; }

        public string IssuingAuthorityDesc { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string SectorSubjectAreaTier1 { get; set; }

        public string SectorSubjectAreaTier1Desc { get; set; }

        public string SectorSubjectAreaTier2 { get; set; }

        public string SectorSubjectAreaTier2Desc { get; set; }

        public List<RelatedLearningAimModel> LearningAims { get; set; }

        public List<CommonComponentModel> CommonComponents { get; set; }

        public bool IsTLevel { get; set; }

        public string NasTitle { get; set; }
    }
}