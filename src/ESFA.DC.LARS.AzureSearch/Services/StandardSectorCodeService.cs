using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class StandardSectorCodeService : IStandardSectorCodeService
    {
        public async Task<Dictionary<string, string>> GetStandardSectorCodeDescriptionsAsync(LarsContext context)
        {
            return await context.LarsStandardSectorCodeLookups
                .ToDictionaryAsync(
                    lookup => lookup.StandardSectorCode,
                    ab => ab.StandardSectorCodeDesc2,
                    StringComparer.OrdinalIgnoreCase);
        }
    }
}
