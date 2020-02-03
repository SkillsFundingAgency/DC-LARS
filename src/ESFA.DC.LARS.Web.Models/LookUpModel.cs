using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class LookUpModel
    {
        public string LookUpKey { get; set; }

        public IEnumerable<NotionalNVQLevel2Model> NotionalNvqLevel2Lookups { get; set; }
    }
}