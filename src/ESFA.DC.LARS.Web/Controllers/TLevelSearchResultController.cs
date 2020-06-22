using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("TLevelSearchResult")]
    public class TLevelSearchResultController : AbstractResultsController<TLevelSearchModel, TLevelModel>
    {
        private const string ResultsTemplate = "_SearchResults";
        private readonly ITLevelsAPIService _tlevelsApiService;

        public TLevelSearchResultController(
            ILookupApiService lookupApiService,
            ITLevelsAPIService itLevelsApiService,
            IEnumerable<ISearchResultsRouteStrategy> resultRouteStrategies)
            : base(resultRouteStrategies, lookupApiService, ResultsTemplate, LearningType.TLevels)
        {
            _tlevelsApiService = itLevelsApiService;
        }

        protected override TLevelSearchModel GetSearchModel(BasicSearchModel basicSearchModel)
        {
            return new TLevelSearchModel();
        }

        protected override Task<IEnumerable<TLevelModel>> GetSearchResults(TLevelSearchModel searchModel)
        {
            return Task.FromResult(Enumerable.Empty<TLevelModel>());
        }

        protected override void ValidateSearch(TLevelSearchModel searchModel, SearchResultsViewModel<TLevelSearchModel, TLevelModel> viewModel)
        {
        }
    }
}