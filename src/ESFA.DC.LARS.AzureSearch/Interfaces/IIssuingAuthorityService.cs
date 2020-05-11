using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIssuingAuthorityService
    {
        Task<IDictionary<string, string>> GetIssuingAuthoritiesAsync(LarsContext context);
    }
}