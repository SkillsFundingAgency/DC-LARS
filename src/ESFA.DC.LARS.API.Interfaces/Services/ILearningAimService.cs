using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface ILearningAimService
    {
        Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters);

        Task<LearningAimModel> GetLearningAim(string learnAimRef);
    }
}