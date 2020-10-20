using System.Collections.Generic;

namespace ESFA.DC.LARS.API.Models
{
    public class FrameworkSearchModel
    {
        public string SearchTerm { get; set; }

        public List<string> FrameworkTypes { get; set; }

        public List<string> IssuingAuthorities { get; set; }

        public List<string> SectorSubjectAreaTier1s { get; set; }
    }
}