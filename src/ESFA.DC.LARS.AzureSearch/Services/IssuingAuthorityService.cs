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
    public class IssuingAuthorityService : IIssuingAuthorityService
    {
        public async Task<IDictionary<string, string>> GetIssuingAuthoritiesAsync(LarsContext context)
        {
            return await context.LarsIssuingAuthorityLookups
            .ToDictionaryAsync(
                ia => ia.IssuingAuthority.ToString(), // field is different type between tables...
                ia => ia.IssuingAuthorityDesc,
                StringComparer.OrdinalIgnoreCase);
        }

        public async Task<IEnumerable<IssuingAuthorityLookupModel>> GetIssuingAuthoritiesLookupAsync(LarsContext context)
        {
            return await context.LarsIssuingAuthorityLookups.OrderBy(c => CustomOrdering(c))
                .ThenBy(c => c.IssuingAuthorityDesc)
                .Select(ia => new IssuingAuthorityLookupModel
                {
                    IssuingAuthority = ia.IssuingAuthority.ToString(),
                    IssuingAuthorityDesc = ia.IssuingAuthorityDesc
                }).ToListAsync();
        }

        private int CustomOrdering(LarsIssuingAuthorityLookup lookup)
        {
            return lookup.IssuingAuthority > 0 ? 0 : 1;
        }
    }
}