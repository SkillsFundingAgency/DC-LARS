using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IFrameworkAimService
    {
        Task<Dictionary<string, List<LearningAimFrameworkModel>>> GetLearningAimFrameworkAimsAsync(LarsContext context, bool units);

        Task<Dictionary<string, List<FrameworkAimModel>>> GetFrameworkLearningAimsAsync(LarsContext context, bool units);
    }
}