using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class FrameworkAimService : IFrameworkAimService
    {
        protected const string UnitLearnAimRefType = "1448";

        public Dictionary<string, List<LearningAimFrameworkModel>> GetLearningAimFrameworkAims(LarsContext context, bool units)
        {
            var frameworkQuerable = GetQuerable(context, units);

            return frameworkQuerable.Select(fa => new LearningAimFrameworkModel
            {
                LearnAimRef = fa.LearnAimRef,
                LearningAimTitle = fa.LearnAimRefNavigation.LearnAimRefTitle,
                FrameworkTitle = fa.LarsFramework.IssuingAuthorityTitle,
                FrameworkCode = fa.FworkCode,
                PathwayCode = fa.PwayCode,
                ProgramType = fa.ProgType,
                EffectiveFrom = fa.LarsFramework.EffectiveFrom,
                EffectiveTo = fa.LarsFramework.EffectiveTo,
                PathwayName = fa.LarsFramework.PathwayName,
                ProgramTypeDesc = fa.LarsFramework.ProgTypeNavigation.ProgTypeDesc,
                IssuingAuthority = fa.LarsFramework.IssuingAuthority,
                ComponentType = fa.FrameworkComponentType
            })
            .AsEnumerable()
            .GroupBy(gb => gb.LearnAimRef, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(av => av.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);
        }

        public Dictionary<string, List<FrameworkAimModel>> GetFrameworkLearningAims(LarsContext context, bool units)
        {
            var frameworkQuerable = GetQuerable(context, units);

            return frameworkQuerable.Select(fa => new FrameworkAimModel
            {
                LearnAimRef = fa.LearnAimRef,
                LearningAimTitle = fa.LearnAimRefNavigation.LearnAimRefTitle,
                AwardingBodyCode = fa.LearnAimRefNavigation.AwardOrgCode,
                Level = fa.LearnAimRefNavigation.NotionalNvqlevelv2,
                EffectiveFrom = fa.EffectiveFrom,
                EffectiveTo = fa.EffectiveTo,
                ComponentType = fa.FrameworkComponentType,
            })
            .AsEnumerable()
            .GroupBy(gb => gb.LearnAimRef, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(av => av.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);
        }

        private IQueryable<LarsFrameworkAim> GetQuerable(LarsContext context, bool units)
        {
            var frameworkQuerable = context.LarsFrameworkAims.AsQueryable();

            if (units)
            {
                return frameworkQuerable.Where(fa => fa.LearnAimRefNavigation.LearnAimRefType == UnitLearnAimRefType);
            }

            return frameworkQuerable = frameworkQuerable.Where(fa => fa.LearnAimRefNavigation.LearnAimRefType != UnitLearnAimRefType);
        }
    }
}
