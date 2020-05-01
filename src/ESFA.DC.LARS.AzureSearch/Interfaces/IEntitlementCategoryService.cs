using System.Collections.Generic;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IEntitlementCategoryService
    {
        Dictionary<string, List<EntitlementCategoryModel>> GetEntitlementCategories(LarsContext context);
    }
}