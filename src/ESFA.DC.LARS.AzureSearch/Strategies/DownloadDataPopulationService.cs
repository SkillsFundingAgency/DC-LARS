using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public class DownloadDataPopulationService : AbstractPopulationService<DownloadDetailsModel>
    {
        private readonly IDowloadDataProviderService _dowloadDataProviderService;

        public DownloadDataPopulationService(
            ISearchServiceClient searchServiceClient,
            IPopulationConfiguration populationConfiguration,
            IDowloadDataProviderService dowloadDataProviderService)
            : base(searchServiceClient, populationConfiguration)
        {
            _dowloadDataProviderService = dowloadDataProviderService;
        }

        protected override string IndexName => _populationConfiguration.DownloadDataIndexName;

        public async override Task PopulateIndexAsync()
        {
            var indexClient = GetIndexClient();

            var downloadData = await _dowloadDataProviderService.GetDownloadDetails();

            var indexActions = downloadData.Select(IndexAction.Upload);

            var batch = IndexBatch.New(indexActions);

            if (batch.Actions.Any())
            {
                indexClient.Documents.Index(batch);
            }
        }
    }
}