using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class EntitlementCategoryService : IEntitlementCategoryService
    {
        public Dictionary<string, List<EntitlementCategoryModel>> GetEntitlementCategories(LarsContext context)
        {
            var entitlementCategoryList = context.LarsAnnualValues
            .Select(av => new EntitlementCategoryModel
            {
                LearnAimRef = av.LearnAimRef,
                EffectiveFrom = av.EffectiveFrom,
                EffectiveTo = av.EffectiveTo,
                Category2Description =
                    av.FullLevel2EntitlementCategoryNavigation.FullLevel2EntitlementCategoryDesc,
                Category3Description =
                    av.FullLevel3EntitlementCategoryNavigation.FullLevel3EntitlementCategoryDesc
            })
            .ToList();

            return entitlementCategoryList
            .GroupBy(gb => gb.LearnAimRef)
            .ToDictionary(av => av.Key, em => em.Select(x => x).ToList(), StringComparer.OrdinalIgnoreCase);
        }
    }
}
