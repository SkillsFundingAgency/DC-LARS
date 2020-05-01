using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class LearningDeliveryCategoryService : ILearningDeliveryCategoryService
    {
        public Dictionary<string, List<CategoryModel>> GetLearningDeliveryCategories(LarsContext context)
        {
            var larsLearningDeliveryCategoriesList = context.LarsLearningDeliveryCategories.Select(cat => new CategoryModel
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
            .ToList();

            return larsLearningDeliveryCategoriesList
            .GroupBy(l => l.LearnAimRef, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                k => k.Key,
                v => v.ToList(),
                StringComparer.OrdinalIgnoreCase);
        }
    }
}
