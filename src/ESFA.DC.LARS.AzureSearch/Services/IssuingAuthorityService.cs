using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}