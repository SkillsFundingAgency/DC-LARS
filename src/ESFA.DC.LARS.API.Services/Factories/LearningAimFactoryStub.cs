﻿using System;
using System.Collections.Generic;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services.Factories
{
    public class LearningAimFactoryStub
    {
        public LearningAimModel GetLearningAim()
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
                FundingModels = new List<FundingModel>
                {
                    new FundingModel
                    {
                        LearnAimRef = "12313131",
                        FundingCategoryDescription = "Test description",
                        EffectiveFrom = new DateTime(2019, 09, 01),
                        EffectiveTo = new DateTime(2020, 08, 31),
                        RateWeighted = 45.5M,
                        RateUnWeighted = 50M,
                        WeightingFactor = "Test"
                    }
                },
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