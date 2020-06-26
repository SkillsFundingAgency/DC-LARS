using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class RelatedLearningAimsService : IRelatedLearningAimsService
    {
        private const string UnitLearnAimRefType = "1448";

        public async Task<ILookup<string, RelatedLearningAimModel>> GetStandardRelatedLearningAims(LarsContext context)
        {
            var results = await context.LarsStandardAims.Select(sa => new
            {
                sa.StandardCode,
                sa.LearnAimRef,
                sa.LearnAimRefNavigation.LearnAimRefTitle,
                sa.LearnAimRefNavigation.AwardOrgCode,
                sa.LearnAimRefNavigation.NotionalNvqlevelv2,
                sa.EffectiveFrom,
                sa.EffectiveTo,
                sa.StandardComponentType
            }).ToListAsync();

            return results.ToLookup(sa => sa.StandardCode.ToString(), sa => new RelatedLearningAimModel
            {
                LearnAimRef = sa.LearnAimRef,
                LearningAimTitle = sa.LearnAimRefTitle,
                AwardingBodyCode = sa.AwardOrgCode,
                Level = sa.NotionalNvqlevelv2,
                EffectiveFrom = sa.EffectiveFrom,
                EffectiveTo = sa.EffectiveTo,
                ComponentType = sa.StandardComponentType
            });
        }

        public async Task<ILookup<string, RelatedLearningAimModel>> GetFrameworkRelatedLearningAims(LarsContext context)
        {
            var results = await context.LarsFrameworkAims
            .Where(fa => fa.LearnAimRefNavigation.LearnAimRefType != UnitLearnAimRefType)
            .Select(fa => new
            {
                // Please note this must match the FrameworkID generated on initial population
                Id = string.Concat(fa.FworkCode, "-", fa.ProgType, "-", fa.PwayCode),
                fa.LearnAimRef,
                fa.LearnAimRefNavigation.LearnAimRefTitle,
                fa.LearnAimRefNavigation.AwardOrgCode,
                fa.LearnAimRefNavigation.NotionalNvqlevelv2,
                fa.EffectiveFrom,
                fa.EffectiveTo,
                ComponentType = fa.FrameworkComponentType,
            }).ToListAsync();

            return results.ToLookup(sa => sa.Id, sa => new RelatedLearningAimModel
            {
                LearnAimRef = sa.LearnAimRef,
                LearningAimTitle = sa.LearnAimRefTitle,
                AwardingBodyCode = sa.AwardOrgCode,
                Level = sa.NotionalNvqlevelv2,
                EffectiveFrom = sa.EffectiveFrom,
                EffectiveTo = sa.EffectiveTo,
                ComponentType = sa.ComponentType
            });
        }
    }
}
