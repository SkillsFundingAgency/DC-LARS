using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESFA.DC.LARS.Azure.Models
{
    public class FrameworkModel
    {
        [Key]
        public string Id { get; set; }

        public int FrameworkCode { get; set; }

        public int ProgramType { get; set; }

        public int PathwayCode { get; set; }

        public string FrameworkTitle { get; set; }

        public string PathwayName { get; set; }

        public string IssuingAuthority { get; set; }

        public string IssuingAuthorityDesc { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string SectorSubjectAreaTier2 { get; set; }

        public string SectorSubjectAreaTier2Desc { get; set; }

        public List<FrameworkAimModel> LearningAims { get; set; }
    }
}