using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class LookUpModel
    {
        public List<AcademicYearLookupModel> AcademicYearLookups { get; set; }

        public List<NotionalNVQLevel2Model> NotionalNvqLevel2Lookups { get; set; }

        public List<AwardingBodyLookupModel> AwardingBodyLookups { get; set; }

        public List<ValidityCategoryLookupModel> ValidityCategoryLookups { get; set; }

        public List<ValidityFundingMappingLookupModel> ValidityFundingMappingLookups { get; set; }

        public IEnumerable<FrameworkTypeLookupModel> FrameworkTypeLookups { get; set; }

        public IEnumerable<IssuingAuthorityLookupModel> IssuingAuthorityLookups { get; set; }
    }
}