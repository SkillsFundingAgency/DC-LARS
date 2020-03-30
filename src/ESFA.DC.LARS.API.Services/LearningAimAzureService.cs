using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class LearningAimAzureService : ILearningAimService
    {
        private readonly IAzureLearningAimsService _azureSearchService;
        private readonly ISearchCleaningService _searchCleaningService;

        public LearningAimAzureService(
            IAzureLearningAimsService azureSearchService,
            ISearchCleaningService searchCleaningService)
        {
            _azureSearchService = azureSearchService;
            _searchCleaningService = searchCleaningService;
        }

        public Task<IEnumerable<LearningAimModel>> GetLearningAims(LearningAimsSearchModel searchParameters)
        {
            searchParameters.SearchTerm = _searchCleaningService.EscapeSearchSpecialCharacters(searchParameters.SearchTerm);
            searchParameters.FundingStreams = CleanFilter(searchParameters.FundingStreams);
            searchParameters.AwardingBodies = CleanFilter(searchParameters.AwardingBodies);
            searchParameters.Levels = CleanFilter(searchParameters.Levels);

            return _azureSearchService.GetLarsLearningDeliveries(searchParameters);
        }

        public Task<LearningAimModel> GetLearningAim(string learnAimRef)
        {
            learnAimRef = _searchCleaningService.EscapeSearchSpecialCharacters(learnAimRef);
            return _azureSearchService.GetLarsLearningAim(learnAimRef);
        }

        private List<string> CleanFilter(List<string> filters)
        {
            if (!(filters?.Any() ?? false))
            {
                return new List<string>();
            }

            return filters.Select(filter => _searchCleaningService.EscapeFilterSpecialCharacters(filter)).ToList();
        }
    }
}