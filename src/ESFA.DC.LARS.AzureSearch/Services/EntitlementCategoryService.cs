using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class EntitlementCategoryService : IEntitlementCategoryService
    {
        public async Task<IDictionary<string, List<EntitlementCategoryModel>>> GetEntitlementCategoriesAsync(LarsContext context)
        {
            var categories = await context.LarsAnnualValues
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
                .ToListAsync();

            return categories
                .GroupBy(gb => gb.LearnAimRef, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(av => av.Key, em => em.ToList(), StringComparer.OrdinalIgnoreCase);
        }
    }
}
