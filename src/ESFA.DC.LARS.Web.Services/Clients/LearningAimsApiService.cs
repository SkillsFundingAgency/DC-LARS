using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class LearningAimsApiService : ILearningAimsApiService
    {
        protected string Url;
        private const string LearnAimRefParameterName = "learnAimRef";

        private readonly IClientService _clientService;

        public LearningAimsApiService(
            IClientService clientService)
        {
            Url = "LearningAims";

            _clientService = clientService;
        }

        public async Task<IEnumerable<LearningAimModel>> GetLearningAims(LearningAimsSearchModel content)
        {
            var response = await _clientService.PostAsync<LearningAimsSearchModel, IEnumerable<LearningAimModel>>(Url, content);

            return response;
        }

        public async Task<LearningAimModel> GetLearningAim(string learnAimRef)
        {
            var parameters = new Dictionary<string, object> { { LearnAimRefParameterName, learnAimRef } };

            var response = await _clientService.GetAsync<LearningAimModel>(Url, parameters);

            return response;
        }
    }
}