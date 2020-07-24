using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using Microsoft.Azure.Search.Models;
using LearningAimModel = ESFA.DC.LARS.Azure.Models.LearningAimModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureLearningAimsService : AzureBaseService, IAzureLearningAimsService
    {
        private readonly IMapper<LearningAimModel, Models.LearningAimModel> _mapper;
        private readonly ILearningDeliveryIndexService _learningDeliveryIndex;
        private readonly IODataQueryService _oDataQueryService;
        private readonly IAzureService _azureService;

        public AzureLearningAimsService(
            IMapper<LearningAimModel, Models.LearningAimModel> mapper,
            ILearningDeliveryIndexService learningDeliveryIndex,
            IODataQueryService oDataQueryService,
            IAzureService azureService)
        {
            _mapper = mapper;
            _learningDeliveryIndex = learningDeliveryIndex;
            _oDataQueryService = oDataQueryService;
            _azureService = azureService;
        }

        public async Task<IEnumerable<Models.LearningAimModel>> GetLarsLearningDeliveries(LearningAimsSearchModel searchModel)
        {
            var parameters = GetDefaultParameters();

            SetFilters(searchModel, parameters);

            var searchTerm = string.Empty;
            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                searchTerm = $"{searchModel.SearchTerm}";
            }

            IEnumerable<Models.LearningAimModel> learningAims;

            learningAims = await SearchIndex(searchTerm, parameters);
            return learningAims;
        }

        public async Task<Models.LearningAimModel> GetLarsLearningAim(string learnAimRef)
        {
            return await IndexSeek(learnAimRef);
        }

        private async Task<IEnumerable<Models.LearningAimModel>> SearchIndex(string searchTerm, SearchParameters parameters)
        {
            IEnumerable<Models.LearningAimModel> learningAims;
            var result = await _azureService.SearchIndexAsync<LearningAimModel>(_learningDeliveryIndex, searchTerm, parameters);
            learningAims = result.Results.Select(r => r.Document).Select(d => _mapper.Map(d));
            return learningAims;
        }

        private async Task<Models.LearningAimModel> IndexSeek(string searchTerm)
        {
            Models.LearningAimModel learningAim;
            var result = await _azureService.GetAsync<LearningAimModel>(_learningDeliveryIndex, searchTerm);
            learningAim = _mapper.Map(result);
            return learningAim;
        }

        private void SetFilters(LearningAimsSearchModel searchModel, SearchParameters parameters)
        {
            _oDataQueryService.SetLearningAimFilters(searchModel, parameters);
        }
    }
}
