using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface ICommonComponentService
    {
        Task<ILookup<string, CommonComponentModel>> GetFrameworkCommonComponents(LarsContext context);

        Task<ILookup<string, CommonComponentModel>> GetStandardCommonComponents(LarsContext context);
    }
}
