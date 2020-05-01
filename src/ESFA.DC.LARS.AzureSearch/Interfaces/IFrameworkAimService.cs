using System.Collections.Generic;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IFrameworkAimService
    {
        Dictionary<string, List<LearningAimFrameworkModel>> GetLearningAimFrameworkAims(LarsContext context, bool units);

        Dictionary<string, List<FrameworkAimModel>> GetFrameworkLearningAims(LarsContext context, bool units);
    }
}