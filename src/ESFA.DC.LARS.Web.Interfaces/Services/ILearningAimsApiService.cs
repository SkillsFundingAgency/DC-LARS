using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface ILearningAimsApiService
    {
        Task<IEnumerable<LearningAimModel>> GetLearningAims(LearningAimsSearchModel content);

        Task<LearningAimModel> GetLearningAim(string learnAimRef);
    }
}