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
    public class ValidityService : IValidityService
    {
        public async Task<Dictionary<string, List<ValidityModel>>> GetValiditiesAsync(LarsContext context)
        {
            var validities = await context.LarsValidities
            .Select(lv => new ValidityModel
            {
                LearnAimRef = lv.LearnAimRef,
                StartDate = lv.StartDate,
                EndDate = lv.EndDate,
                LastNewStartDate = lv.LastNewStartDate,
                ValidityCategory = lv.ValidityCategory.ToUpper(),
                ValidityCategoryDescription = lv.ValidityCategoryNavigation.ValidityCategoryDesc2
            })
            .ToListAsync();

            return validities
                .GroupBy(l => l.LearnAimRef, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(k => k.Key, v => v.ToList(), StringComparer.OrdinalIgnoreCase);
        }
    }
}
