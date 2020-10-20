using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IComponentTypeService
    {
        Task<IDictionary<int, string>> GetComponentTypesAsync(LarsContext context);
    }
}