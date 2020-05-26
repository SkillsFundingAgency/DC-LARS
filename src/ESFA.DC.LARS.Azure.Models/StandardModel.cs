using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    public class StandardModel
    {
        [Key]
        public string StandardCode { get; set; }

        [IsSearchable]
        public string StandardName { get; set; }

        public string StandardSectorCode { get; set; }

        public string StandardSectorCodeDesc2 { get; set; }

        public int Version { get; set; }

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
    }
}
