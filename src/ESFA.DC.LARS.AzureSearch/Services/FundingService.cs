using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class FundingService : IFundingService
    {
        public Dictionary<string, List<FundingModel>> GetFundings(LarsContext context)
        {
            var fundingList = context.LarsFundings
            .Select(lf => new FundingModel
            {
                LearnAimRef = lf.LearnAimRef,
                EffectiveFrom = lf.EffectiveFrom,
                EffectiveTo = lf.EffectiveTo,
                FundingCategory = lf.FundingCategory,
                FundingCategoryDescription = lf.FundingCategoryNavigation.FundingCategoryDesc2,
                RateWeighted = lf.RateWeighted.ToString(),
                RateUnWeighted = lf.RateUnWeighted.ToString(),
                WeightingFactor = lf.WeightingFactor
            })
            .ToList();

            return fundingList
            .GroupBy(l => l.LearnAimRef, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                k => k.Key,
                v => v.Select(l => l).ToList(),
                StringComparer.OrdinalIgnoreCase);
        }

        public List<FundingModel> GetFundingsByLearnRef(Dictionary<string, List<FundingModel>> fundings, string learnRef)
        {
            return fundings?.ContainsKey(learnRef) == true ? fundings[learnRef] : new List<FundingModel>();
        }
    }
}
