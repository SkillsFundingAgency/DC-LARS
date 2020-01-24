using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class LearningAimsApiService : ILearningAimsApiService
    {
        private const string Url = "LearningAims";
        private const string LearnAimRefParameterName = "learnAimRef";

        private readonly IClientService _clientService;

        public LearningAimsApiService(
            IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel content)
        {
            var response = await _clientService.PostAsync<SearchModel, IEnumerable<LearningAimModel>>(Url, content);

            return response;
        }

        public async Task<LearningAimModel> GetLearningAim(string learnAimRef)
        {
            var response = await _clientService.GetAsync<LearningAimModel>(Url, LearnAimRefParameterName, learnAimRef);

            return response;
        }
    }
}