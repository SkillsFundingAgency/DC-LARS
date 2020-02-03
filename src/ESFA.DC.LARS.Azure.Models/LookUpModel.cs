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

        public IEnumerable<NotionalNVQLevel2Model> NotionalNvqLevel2Lookups { get; set; }
    }
}