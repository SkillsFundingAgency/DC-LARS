using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class TLevelTypeSortingService : ISortingService<FrameworkTypeLookupModel>
    {
        public IEnumerable<FrameworkTypeLookupModel> Sort(IEnumerable<FrameworkTypeLookupModel> lookups)
        {
            return lookups.OrderBy(l => l.FrameworkTypeDesc);
        }
    }
}
