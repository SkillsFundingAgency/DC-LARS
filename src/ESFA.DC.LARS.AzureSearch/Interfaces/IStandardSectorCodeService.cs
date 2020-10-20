using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IStandardSectorCodeService
    {
        Task<IDictionary<string, string>> GetStandardSectorCodeDescriptionsAsync(LarsContext context);
    }
}
