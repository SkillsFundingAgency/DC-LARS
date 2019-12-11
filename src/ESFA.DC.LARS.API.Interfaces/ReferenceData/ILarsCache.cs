using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Interfaces.ReferenceData
{
    public interface ILarsCache
    {
        Task<IEnumerable<LearningAimModel>> GetLearningAims(bool isDirty);
    }
}