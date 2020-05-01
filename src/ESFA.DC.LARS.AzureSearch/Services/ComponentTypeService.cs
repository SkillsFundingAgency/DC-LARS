using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class ComponentTypeService : IComponentTypeService
    {
        public async Task<IDictionary<int, string>> GetComponentTypesAsync(LarsContext context)
        {
            return await context.LarsApprenticeshipComponentTypeLookups
            .ToDictionaryAsync(
                ct => ct.ApprenticeshipComponentType,
                ct => ct.ApprenticeshipComponentTypeDesc);
        }
    }
}