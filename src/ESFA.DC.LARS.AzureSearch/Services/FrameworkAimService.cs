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
    public class FrameworkAimService : IFrameworkAimService
    {
        protected const string UnitLearnAimRefType = "1448";

        public async Task<IDictionary<string, List<LearningAimFrameworkModel>>> GetLearningAimFrameworkAimsAsync(LarsContext context, bool units)
        {
            var frameworkQuerable = GetQuerable(context, units);

            var frameworks = await frameworkQuerable.Select(fa => new LearningAimFrameworkModel
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
            .ToListAsync();

            return frameworks
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
