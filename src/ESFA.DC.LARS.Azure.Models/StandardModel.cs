﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    public class StandardModel
    {
        [Key]
        [IsSearchable]
        public string StandardCode { get; set; }

        [IsSearchable]
        public string StandardName { get; set; }

        [IsFilterable]
        public string StandardSectorCode { get; set; }

        public string StandardSectorCodeDesc2 { get; set; }

        public int Version { get; set; }

        [IsFilterable]
        public string NotionalEndLevel { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? LastDateStarts { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string SectorSubjectAreaTier1 { get; set; }

        public string SectorSubjectAreaTier1Desc { get; set; }

        public string SectorSubjectAreaTier2 { get; set; }

        public string SectorSubjectAreaTier2Desc { get; set; }

        public string IntegratedDegreeStandard { get; set; }

        public string OtherBodyApprovalRequired { get; set; }

        public List<CommonComponentModel> CommonComponents { get; set; }

        public List<StandardFundingModel> StandardFundingModels { get; set; }

        public List<StandardApprenticeshipFundingModel> StandardApprenticeshipFundingModels { get; set; }

        public List<RelatedLearningAimModel> LearningAims { get; set; }
    }
}
