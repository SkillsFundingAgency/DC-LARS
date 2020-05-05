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
    public class LearningDeliveryCategoryService : ILearningDeliveryCategoryService
    {
        public async Task<Dictionary<string, List<CategoryModel>>> GetLearningDeliveryCategoriesAsync(LarsContext context)
        {
            var catergoryList = await context.LarsLearningDeliveryCategories.Select(cat => new CategoryModel
            {
                LearnAimRef = cat.LearnAimRef,
                Reference = cat.CategoryRef,
                EffectiveTo = cat.EffectiveTo,
                EffectiveFrom = cat.EffectiveFrom,
                Title = cat.CategoryRefNavigation.CategoryName,
                Description = cat.CategoryRefNavigation.CategoryName,
                ParentReference = cat.CategoryRefNavigation.ParentCategoryRef,
                ParentDescription = context.LarsCategoryLookups
                .Where(l => l.CategoryRef == cat.CategoryRefNavigation.ParentCategoryRef)
                .Select(l => l.CategoryName)
                .FirstOrDefault()
            })
            .ToListAsync();

            return catergoryList
               .GroupBy(l => l.LearnAimRef, StringComparer.OrdinalIgnoreCase)
               .ToDictionary(k => k.Key, v => v.ToList(), StringComparer.OrdinalIgnoreCase);
        }
    }
}
