using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.Azure.Models
{
    public class LookUpModel
    {
        [Key]
        [IsSearchable]
        public string LookUpKey { get; set; }

        public List<NotionalNVQLevel2LookupModel> NotionalNvqLevel2Lookups { get; set; }

        public List<AcademicYearLookupModel> AcademicYearLookups { get; set; }

        public List<AwardingBodyLookupModel> AwardingBodyLookups { get; set; }

        public List<ValidityCategoryLookupModel> ValidityCategoryLookups { get; set; }

        public List<ValidityFundingMappingLookupModel> ValidityFundingMappingLookups { get; set; }

        public List<FrameworkTypeLookupModel> FrameworkTypeLookups { get; set; }

        public List<FrameworkTypeLookupModel> TLevelTypeLookups { get; set; }

        public List<IssuingAuthorityLookupModel> IssuingAuthorityLookups { get; set; }

        public List<StandardSectorLookupModel> StandardSectorLookups { get; set; }

        public List<SectorSubjectAreaTier1LookupModel> SectorSubjectAreaTier1Lookups { get; set; }
    }
}