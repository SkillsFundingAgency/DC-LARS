using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class IssuingAuthoritySortingService : ISortingService<IssuingAuthorityLookupModel>
    {
        public IEnumerable<IssuingAuthorityLookupModel> Sort(IEnumerable<IssuingAuthorityLookupModel> lookups)
        {
            return lookups.OrderBy(c => int.Parse(c.IssuingAuthority) > 0 ? 0 : 1).ThenBy(c => c.IssuingAuthorityDesc);
        }
    }
}
