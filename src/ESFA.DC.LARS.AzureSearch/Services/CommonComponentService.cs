using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class CommonComponentService : ICommonComponentService
    {
        public async Task<ILookup<string, CommonComponentModel>> GetFrameworkCommonComponents(LarsContext context)
        {
            var results = await context.LarsFrameworkCmnComps.Select(c => new
            {
                // Please note this must match the FrameworkID generated on initial population
                Id = string.Concat(c.FworkCode, "-", c.ProgType, "-", c.PwayCode),
                c.CommonComponent,
                c.EffectiveFrom,
                c.EffectiveTo,
                c.MinLevel
            }).ToListAsync();

            return results.ToLookup(c => c.Id, c => new CommonComponentModel
            {
                CommonComponent = c.CommonComponent,
                EffectiveFrom = c.EffectiveFrom,
                EffectiveTo = c.EffectiveTo,
                MinLevel = c.MinLevel
            });
        }

        public async Task<ILookup<string, CommonComponentModel>> GetStandardCommonComponents(LarsContext context)
        {
            var results = await context.LarsStandardCommonComponents.Select(c => new
            {
                Id = c.StandardCode.ToString(),
                c.CommonComponent,
                c.EffectiveFrom,
                c.EffectiveTo,
                c.MinLevel
            }).ToListAsync();

            return results.ToLookup(c => c.Id, c => new CommonComponentModel
            {
                CommonComponent = c.CommonComponent,
                EffectiveFrom = c.EffectiveFrom,
                EffectiveTo = c.EffectiveTo,
                MinLevel = c.MinLevel
            });
        }
    }
}
