using System;
using System.Linq;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLearningAimsMapper : IMapper<LearningAimModel, Models.LearningAimModel>
    {
        public Models.LearningAimModel Map(LearningAimModel input)
        {
            return new Models.LearningAimModel
            {
                LearnAimRef = input.LearnAimRef,
                LearningAimTitle = input.LearningAimTitle,
                Type = input.Type,
                AwardingBody = input.AwardingBody,
                Level = input.Level,
                GuidedLearningHours = input.GuidedLearningHours,
                Level2Category = input.Level2Category,
                Level3Category = input.Level3Category,
                Categories = input.Categories.Select(Map),
                FundingModels = input.FundingModels.Select(Map)
            };
        }

        private Models.CategoryModel Map(CategoryModel input)
        {
            return new Models.CategoryModel
            {
                Title = input.Title,
                Reference = input.Reference,
                Description = input.Description,
                ParentReference = input.ParentReference,
                ParentDescription = input.ParentDescription,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo
            };
        }

        private Models.FundingModel Map(FundingModel input)
        {
            return new Models.FundingModel
            {
                LearnAimRef = input.LearnAimRef,
                FundingCategoryDescription = input.FundingCategoryDescription,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                WeightingFactor = input.WeightingFactor,
                RateWeighted = decimal.TryParse(input.RateWeighted, out var weighted) ? weighted : default(decimal),
                RateUnWeighted = decimal.TryParse(input.RateUnWeighted, out var unWeighted) ? unWeighted : default(decimal)
            };
        }
    }
}