using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class ValidityService : IValidityService
    {
        public Dictionary<string, List<ValidityModel>> GetValidities(LarsContext context)
        {
            var larsValiditiesList = context.LarsValidities
            .Select(lv => new ValidityModel
            {
                LearnAimRef = lv.LearnAimRef,
                StartDate = lv.StartDate,
                EndDate = lv.EndDate,
                LastNewStartDate = lv.LastNewStartDate,
                ValidityCategory = lv.ValidityCategory.ToUpper(),
                ValidityCategoryDescription = lv.ValidityCategoryNavigation.ValidityCategoryDesc2
            })
            .ToList();

            return larsValiditiesList
            .GroupBy(l => l.LearnAimRef, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                k => k.Key,
                v => v.Select(l => l).ToList(),
                StringComparer.OrdinalIgnoreCase);
        }

        public List<ValidityModel> GetValidityByLearnRef(Dictionary<string, List<ValidityModel>> validities, string learnRef)
        {
            return validities?.ContainsKey(learnRef) == true ? validities[learnRef] : new List<ValidityModel>();
        }
    }
}
