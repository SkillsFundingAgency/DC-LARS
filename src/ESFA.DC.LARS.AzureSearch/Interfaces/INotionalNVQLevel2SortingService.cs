using System.Collections.Generic;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface INotionalNVQLevel2SortingService
    {
        IEnumerable<NotionalNVQLevel2LookupModel> SortForDisplay(List<NotionalNVQLevel2LookupModel> itemsToSort);
    }
}
