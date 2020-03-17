using System.Collections.Generic;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IComponentTypeService
    {
        IDictionary<int, string> GetComponentTypes(LarsContext context);
    }
}