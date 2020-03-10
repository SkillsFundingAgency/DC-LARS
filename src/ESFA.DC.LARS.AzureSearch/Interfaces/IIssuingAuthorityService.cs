using System.Collections.Generic;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIssuingAuthorityService
    {
        IDictionary<string, string> GetIssuingAuthorities(LarsContext context);
    }
}