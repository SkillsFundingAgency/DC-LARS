using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class AwardingBodySortingService : ISortingService<AwardingBodyLookupModel>
    {
        public IEnumerable<AwardingBodyLookupModel> Sort(IEnumerable<AwardingBodyLookupModel> lookups)
        {
            return lookups.OrderBy(l => l.AwardingBodyName);
        }
    }
}
