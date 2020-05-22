using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IStandardSectorCodeService
    {
        Task<Dictionary<string, string>> GetStandardSectorCodeDescriptionsAsync(LarsContext context);
    }
}
