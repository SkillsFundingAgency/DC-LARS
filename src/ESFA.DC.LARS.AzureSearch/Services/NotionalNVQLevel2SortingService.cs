using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class NotionalNVQLevel2SortingService : ISortingService<NotionalNVQLevel2LookupModel>
    {
        private const string EntryNotionalNVQLevel = "E";

        public IEnumerable<NotionalNVQLevel2LookupModel> Sort(IEnumerable<NotionalNVQLevel2LookupModel> lookups)
        {
            // Default Alphanumeric sort on level
            lookups = lookups.OrderBy(a => a.NotionalNVQLevelV2);

            // Business Rule:  Entry level must be first item displayed.
            lookups = MoveEntryLevelToStartOfList(lookups.ToList());

            return lookups;
        }

        private IEnumerable<NotionalNVQLevel2LookupModel> MoveEntryLevelToStartOfList(List<NotionalNVQLevel2LookupModel> itemsToSort)
        {
            var entryLevel = itemsToSort.SingleOrDefault(i => string.Equals(i.NotionalNVQLevelV2, EntryNotionalNVQLevel, StringComparison.OrdinalIgnoreCase));

            if (entryLevel != null)
            {
                itemsToSort.Remove(entryLevel);
                itemsToSort.Insert(0, entryLevel);
            }

            return itemsToSort;
        }
    }
}
