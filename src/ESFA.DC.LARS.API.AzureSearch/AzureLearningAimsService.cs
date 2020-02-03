using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.Azure.Search.Models;
using LearningAimModel = ESFA.DC.LARS.Azure.Models.LearningAimModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureLearningAimsService : IAzureLearningAimsService
    {
        private readonly ITelemetry _telemetryClient;
        private readonly IMapper<LearningAimModel, Models.LearningAimModel> _mapper;
        private readonly ILearningDeliveryIndexService _learningDeliveryIndex;
        private readonly IODataQueryService _oDataQueryService;
        private readonly IAzureService _azureService;

        public AzureLearningAimsService(
            ITelemetry telemetryClient,
            IMapper<LearningAimModel, Models.LearningAimModel> mapper,
            ILearningDeliveryIndexService learningDeliveryIndex,
            IODataQueryService oDataQueryService,
            IAzureService azureService)
        {
            _telemetryClient = telemetryClient;
            _mapper = mapper;
            _learningDeliveryIndex = learningDeliveryIndex;
            _oDataQueryService = oDataQueryService;
            _azureService = azureService;
        }

        public async Task<IEnumerable<Models.LearningAimModel>> GetLarsLearningDeliveries(SearchModel searchModel)
        {
            var parameters = GetDefaultParameters();

            SetFilters(searchModel, parameters);

            var searchTerm = string.Empty;
            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                searchTerm = $"{searchModel.SearchTerm}";
            }

            IEnumerable<Models.LearningAimModel> learningAims;
            try
            {
                 learningAims = await SearchIndex(searchTerm, parameters);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackEvent(ex.Message);
                throw;
            }

            return learningAims;
        }

        public async Task<Models.LearningAimModel> GetLarsLearningAim(string learnAimRef)
        {
            return await IndexSeek(learnAimRef);
        }

        private async Task<IEnumerable<Models.LearningAimModel>> SearchIndex(string searchTerm, SearchParameters parameters)
        {
            IEnumerable<Models.LearningAimModel> learningAims;
            try
            {
                var result = await _azureService.SearchIndexAsync<LearningAimModel>(_learningDeliveryIndex, searchTerm, parameters);

                learningAims = result.Results.Select(r => r.Document).Select(d => _mapper.Map(d));
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackEvent(ex.Message);
                throw;
            }

            return learningAims;
        }

        private async Task<Models.LearningAimModel> IndexSeek(string searchTerm)
        {
            Models.LearningAimModel learningAim;

            try
            {
                var result = await _azureService.GetAsync<LearningAimModel>(_learningDeliveryIndex, searchTerm);
                learningAim = _mapper.Map(result);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackEvent(ex.Message);
                throw;
            }

            return learningAim;
        }

        private void SetFilters(SearchModel searchModel, SearchParameters parameters)
        {
            _oDataQueryService.SetLevelFilters(searchModel, parameters);
        }

        private SearchParameters GetDefaultParameters()
        {
            return new SearchParameters
            {
                QueryType = QueryType.Full,
                SearchMode = SearchMode.Any,
                IncludeTotalResultCount = true,
                Top = 10000
            };
        }
    }
}
