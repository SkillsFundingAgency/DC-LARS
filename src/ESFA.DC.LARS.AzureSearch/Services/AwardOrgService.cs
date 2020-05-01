using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class AwardOrgService : IAwardOrgService
    {
        public async Task<Dictionary<string, string>> GetAwardingOrgNamesAsync(LarsContext context)
        {
            return await context.LarsAwardOrgCodeLookups
                 .ToDictionaryAsync(
                     ab => ab.AwardOrgCode,
                     ab => ab.AwardOrgName,
                     StringComparer.OrdinalIgnoreCase);
        }
    }
}
