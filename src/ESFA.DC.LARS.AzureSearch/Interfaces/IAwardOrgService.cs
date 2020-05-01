using System.Collections.Generic;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IAwardOrgService
    {
        Dictionary<string, string> GetAwardingOrgNames(LarsContext context);
    }
}
