using System;
using System.Collections.Generic;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Tests.Factories
{
    public class LearningAimFactory
    {
        public static LearningAimModel GetLearningAim()
        {
            return new LearningAimModel
            {
                LearnAimRef = "12313131",
                AwardingBody = "Test Awarding Body",
                LearningAimTitle = "Learning Aim 1",
                Level = "1",
                Type = "Test type",
                GuidedLearningHours = 5,
                Level2Category = string.Empty,
                Level3Category = string.Empty,
                Categories = new List<CategoryModel>
                {
                    new CategoryModel
                    {
                        Title = "Category",
                        Description = "Some category",
                        Reference = 112312,
                        ParentReference = 0,
                        ParentDescription = string.Empty,
                        EffectiveFrom = new DateTime(2019, 09, 01),
                        EffectiveTo = new DateTime(2020, 08, 31)
                    }
                }
            };
        }
    }
}