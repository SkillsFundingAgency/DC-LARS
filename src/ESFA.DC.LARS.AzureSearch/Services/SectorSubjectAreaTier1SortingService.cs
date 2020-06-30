using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class SectorSubjectAreaTier1SortingService : ISortingService<SectorSubjectAreaTier1LookupModel>
    {
        public IEnumerable<SectorSubjectAreaTier1LookupModel> Sort(IEnumerable<SectorSubjectAreaTier1LookupModel> lookups)
        {
            return lookups.OrderBy(l => l.SectorSubjectAreaTier1Desc);
        }
    }
}
