using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Interfaces
{
    public interface ILearningAimService
    {
        Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters);
    }
}