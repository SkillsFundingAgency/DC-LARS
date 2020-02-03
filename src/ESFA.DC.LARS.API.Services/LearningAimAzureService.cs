using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class LearningAimAzureService : ILearningAimService
    {
        private readonly IAzureLearningAimsService _azureSearchService;

        public LearningAimAzureService(IAzureLearningAimsService azureSearchService)
        {
            _azureSearchService = azureSearchService;
        }

        public Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters)
        {
            return _azureSearchService.GetLarsLearningDeliveries(searchParameters);
        }

        public Task<LearningAimModel> GetLearningAim(string learnAimRef)
        {
            return _azureSearchService.GetLarsLearningAim(learnAimRef);
        }
    }
}