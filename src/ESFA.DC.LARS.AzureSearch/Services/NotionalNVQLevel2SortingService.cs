using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class NotionalNVQLevel2SortingService : INotionalNVQLevel2SortingService
    {
        private const string EntryNotionalNVQLevel = "E";

        public IEnumerable<NotionalNVQLevel2LookupModel> SortForDisplay(List<NotionalNVQLevel2LookupModel> itemsToSort)
        {
            // Default Alphanumeric sort on level
            itemsToSort = itemsToSort.OrderBy(a => a.NotionalNVQLevelV2).ToList();

            // Business Rule:  Entry level must be first item displayed.
            MoveEntryLevelToStartOfList(itemsToSort);

            return itemsToSort;
        }

        private void MoveEntryLevelToStartOfList(List<NotionalNVQLevel2LookupModel> itemsToSort)
        {
            var entryLevel = itemsToSort.SingleOrDefault(i => string.Equals(i.NotionalNVQLevelV2, EntryNotionalNVQLevel, StringComparison.OrdinalIgnoreCase));

            if (entryLevel != null)
            {
                itemsToSort.Remove(entryLevel);
                itemsToSort.Insert(0, entryLevel);
            }
        }
    }
}
