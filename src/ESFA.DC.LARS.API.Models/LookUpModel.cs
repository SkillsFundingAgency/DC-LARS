using System.Collections.Generic;

namespace ESFA.DC.LARS.API.Models
{
    public class LookUpModel
    {
        public string LookUpKey { get; set; }

        public IEnumerable<AcademicYearLookupModel> AcademicYearLookups { get; set; }

        public IEnumerable<NotionalNVQLevel2Model> NotionalNvqLevel2Lookups { get; set; }
    }
}