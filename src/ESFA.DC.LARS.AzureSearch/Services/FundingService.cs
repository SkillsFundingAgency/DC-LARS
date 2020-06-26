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
    public class FundingService : IFundingService
    {
        public async Task<IDictionary<string, List<FundingModel>>> GetFundingsAsync(LarsContext context)
        {
            var fundingList = await context.LarsFundings
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
            .ToListAsync();

            return fundingList
                .GroupBy(l => l.LearnAimRef, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(k => k.Key, v => v.ToList(), StringComparer.OrdinalIgnoreCase);
        }
    }
}
