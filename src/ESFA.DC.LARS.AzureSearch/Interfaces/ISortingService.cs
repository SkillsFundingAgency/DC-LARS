using System.Collections.Generic;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface ISortingService<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> lookups);
    }
}
