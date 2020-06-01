using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class CommonComponentLookupService : ICommonComponentLookupService
    {
        public async Task<IDictionary<int, CommonComponentLookupModel>> GetCommonComponentLookupsAsync(LarsContext context)
        {
            return await context.LarsCommonComponentLookups
                .ToDictionaryAsync(k => k.CommonComponent, v => new CommonComponentLookupModel
                {
                     CommonComponent = v.CommonComponent,
                     Description = v.CommonComponentDesc
                });
        }
    }
}
