using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using Microsoft.Azure.Search.Models;
using StandardModel = ESFA.DC.LARS.Azure.Models.StandardModel;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureStandardService : AzureBaseService, IAzureStandardService
    {
        private readonly IStandardIndexService _standardIndexService;
        private readonly IMapper<StandardModel, Models.StandardModel> _mapper;
        private readonly IAzureService _azureService;
        private readonly IODataQueryService _oDataQueryService;

        public AzureStandardService(
            IStandardIndexService standardIndexService,
            IMapper<StandardModel, Models.StandardModel> mapper,
            IAzureService azureService,
            IODataQueryService oDataQueryService,
            ISearchTermFormattingService searchTermFormattingService)
            : base(searchTermFormattingService)
        {
            _standardIndexService = standardIndexService;
            _mapper = mapper;
            _azureService = azureService;
            _oDataQueryService = oDataQueryService;
        }

        public async Task<IEnumerable<Models.StandardModel>> GetStandards(StandardSearchModel searchModel)
        {
            var parameters = GetDefaultParameters();

            SetFilters(searchModel, parameters);

            var searchTerm = FormatSearchTerm(searchModel.SearchTerm);

            IEnumerable<Models.StandardModel> standards;
            standards = await SearchIndex(searchTerm, parameters);

            return standards;
        }

        public async Task<Models.StandardModel> GetStandard(string standardCode)
        {
            Models.StandardModel standard;
            var result = await _azureService.GetAsync<StandardModel>(_standardIndexService, standardCode);

            standard = _mapper.Map(result);
            return standard;
        }

        private async Task<IEnumerable<Models.StandardModel>> SearchIndex(string searchTerm, SearchParameters parameters)
        {
            IEnumerable<Models.StandardModel> standards;

            var result = await _azureService.SearchIndexAsync<StandardModel>(_standardIndexService, searchTerm, parameters);

            standards = result.Results
                .Select(r => r.Document)
                .Select(d => _mapper.Map(d))
                .OrderBy(f => f.StandardName);

            return standards;
        }

        private void SetFilters(StandardSearchModel searchModel, SearchParameters parameters)
        {
            _oDataQueryService.SetStandardFilters(searchModel, parameters);
        }
    }
}