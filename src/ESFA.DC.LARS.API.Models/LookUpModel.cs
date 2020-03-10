using System.Collections.Generic;

namespace ESFA.DC.LARS.API.Models
{
    public class LookUpModel
    {
        public string LookUpKey { get; set; }

        public List<AcademicYearLookupModel> AcademicYearLookups { get; set; }

        public List<NotionalNVQLevel2Model> NotionalNvqLevel2Lookups { get; set; }

        public List<AwardingBodyLookupModel> AwardingBodyLookups { get; set; }

        public List<ValidityCategoryLookupModel> ValidityCategoryLookups { get; set; }

        public List<ValidityFundingMappingLookupModel> ValidityFundingMappingLookups { get; set; }
    }
}