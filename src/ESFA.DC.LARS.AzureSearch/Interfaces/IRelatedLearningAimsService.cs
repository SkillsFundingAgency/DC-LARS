using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IRelatedLearningAimsService
    {
        Task<ILookup<string, RelatedLearningAimModel>> GetStandardRelatedLearningAims(LarsContext context);

        Task<ILookup<string, RelatedLearningAimModel>> GetFrameworkRelatedLearningAims(LarsContext context);
    }
}
