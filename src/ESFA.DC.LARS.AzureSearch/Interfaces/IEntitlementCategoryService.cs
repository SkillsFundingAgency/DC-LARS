using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IEntitlementCategoryService
    {
        Task<IDictionary<string, List<EntitlementCategoryModel>>> GetEntitlementCategoriesAsync(LarsContext context);
    }
}