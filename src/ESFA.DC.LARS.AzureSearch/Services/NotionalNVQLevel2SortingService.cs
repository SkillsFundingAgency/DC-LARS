using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class NotionalNVQLevel2SortingService : ISortingService<NotionalNVQLevel2LookupModel>
    {
        private readonly Dictionary<string, int> _sortOrder = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "E", 1 },
            { "1", 2 },
            { "1.5", 3 },
            { "2", 4 },
            { "3", 5 },
            { "4", 6 },
            { "5", 7 },
            { "6", 8 },
            { "7", 9 },
            { "8", 10 },
            { "H", 11 },
            { "M", 12 },
            { "X", 13 }
        };

        public IEnumerable<NotionalNVQLevel2LookupModel> Sort(IEnumerable<NotionalNVQLevel2LookupModel> lookups)
        {
            lookups = lookups.OrderBy(a => _sortOrder.GetValueOrDefault(a.NotionalNVQLevelV2, lookups.Count())).ThenBy(a => a.NotionalNVQLevelV2);
            return lookups;
        }
    }
}
