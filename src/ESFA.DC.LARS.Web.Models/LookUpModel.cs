using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class LookUpModel
    {
        public IEnumerable<AcademicYearLookupModel> AcademicYearLookups { get; set; }

        public IEnumerable<NotionalNVQLevel2Model> NotionalNvqLevel2Lookups { get; set; }

        public IEnumerable<ValidityFundingMappingLookupModel> ValidityFundingMappingLookups { get; set; }
    }
}