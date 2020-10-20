using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Interfaces.AzureSearch
{
    public interface IAzureLearningAimsService
    {
        Task<IEnumerable<Models.LearningAimModel>> GetLarsLearningDeliveries(LearningAimsSearchModel searchModel);

        Task<Models.LearningAimModel> GetLarsLearningAim(string learnAimRef);
    }
}