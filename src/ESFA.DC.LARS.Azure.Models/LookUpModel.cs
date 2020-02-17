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

        public IEnumerable<NotionalNVQLevel2LookupModel> NotionalNvqLevel2Lookups { get; set; }

        public IEnumerable<AcademicYearLookupModel> AcademicYearLookups { get; set; }

        public IEnumerable<AwardingBodyLookupModel> AwardingBodyLookups { get; set; }

        public IEnumerable<ValidityCategoryLookupModel> ValidityCategoryLookups { get; set; }

        public IEnumerable<ValidityFundingMappingLookupModel> ValidityFundingMappingLookups { get; set; }
    }
}