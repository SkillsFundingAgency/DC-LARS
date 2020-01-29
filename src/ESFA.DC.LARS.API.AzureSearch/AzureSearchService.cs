using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using LearningAimModel = ESFA.DC.LARS.Azure.Models.LearningAimModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureSearchService : IAzureSearchService
    {
        private readonly ITelemetry _telemetryClient;
        private readonly IMapper<LearningAimModel, Models.LearningAimModel> _mapper;
        private readonly ILearningDeliveryIndexService _learningDeliveryIndex;

        public AzureSearchService(
            ITelemetry telemetryClient,
            IMapper<LearningAimModel, Models.LearningAimModel> mapper,
            ILearningDeliveryIndexService learningDeliveryIndex)
        {
            _telemetryClient = telemetryClient;
            _mapper = mapper;
            _learningDeliveryIndex = learningDeliveryIndex;
        }

        public async Task<IEnumerable<Models.LearningAimModel>> GetLarsLearningDeliveries(SearchModel searchModel)
        {
            IEnumerable<Models.LearningAimModel> learningAims;

            var parameters = GetDefaultParameters();
            parameters.SearchFields = new List<string> { "LearningAimTitle" };

            var searchTerm = string.Empty;
            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                searchTerm = $"{searchModel.SearchTerm}";
            }

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
            try
            {
                var result = await _learningDeliveryIndex.Documents.SearchAsync<LearningAimModel>(searchTerm, parameters);
                learningAims = result.Results.Select(r => _mapper.Map(r.Document));
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
                var result = await _learningDeliveryIndex.Documents.GetAsync<LearningAimModel>(searchTerm);
                learningAim = _mapper.Map(result);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackEvent(ex.Message);
                throw;
            }

            return learningAim;
        }

        private SearchParameters GetDefaultParameters()
        {
            return new SearchParameters
            {
                QueryType = QueryType.Simple,
                SearchMode = SearchMode.Any,
                IncludeTotalResultCount = true,
                Top = 10000
            };
        }
    }
}
