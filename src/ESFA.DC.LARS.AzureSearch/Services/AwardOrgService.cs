using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class AwardOrgService : IAwardOrgService
    {
        public Dictionary<string, string> GetAwardingOrgNames(LarsContext context)
        {
            return context.LarsAwardOrgCodeLookups
                 .ToDictionary(
                     ab => ab.AwardOrgCode,
                     ab => ab.AwardOrgName,
                     StringComparer.OrdinalIgnoreCase);
        }
    }
}
