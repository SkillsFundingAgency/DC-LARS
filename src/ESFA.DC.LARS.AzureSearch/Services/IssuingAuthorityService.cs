using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class IssuingAuthorityService : IIssuingAuthorityService
    {
        public IDictionary<string, string> GetIssuingAuthorities(LarsContext context)
        {
            IDictionary<string, string> issuingAuthorities;

            issuingAuthorities = context.LarsIssuingAuthorityLookups
                .ToDictionary(
                    ia => ia.IssuingAuthority.ToString(), // field is different type between tables...
                    ia => ia.IssuingAuthorityDesc);

            return issuingAuthorities;
        }
    }
}