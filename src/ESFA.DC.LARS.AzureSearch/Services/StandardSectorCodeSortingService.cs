using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class StandardSectorCodeSortingService : ISortingService<StandardSectorLookupModel>
    {
        public IEnumerable<StandardSectorLookupModel> Sort(IEnumerable<StandardSectorLookupModel> lookups)
        {
            return lookups.OrderBy(l => l.StandardSectorCodeDesc);
        }
    }
}
