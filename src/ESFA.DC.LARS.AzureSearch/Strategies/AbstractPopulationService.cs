using System.Threading.Tasks;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Index = Microsoft.Azure.Search.Models.Index;

namespace ESFA.DC.LARS.AzureSearch.Strategies
{
    public abstract class AbstractPopulationService<T> : IIndexPopulationService
    {
        protected const string UnitLearnAimRefType = "1448";

        protected readonly IPopulationConfiguration _populationConfiguration;
        private readonly ISearchServiceClient _searchServiceClient;

        protected AbstractPopulationService(ISearchServiceClient searchServiceClient, IPopulationConfiguration populationConfiguration)
        {
            _searchServiceClient = searchServiceClient;
            _populationConfiguration = populationConfiguration;
        }

        protected abstract string IndexName { get; }

        public abstract Task PopulateIndexAsync();

        public void CreateIndex()
        {
            var definition = new Index
            {
                Name = IndexName,
                Fields = FieldBuilder.BuildForType<T>()
            };

            _searchServiceClient.Indexes.Create(definition);
        }

        public void DeleteIndex()
        {
            if (_searchServiceClient.Indexes.Exists(IndexName))
            {
                _searchServiceClient.Indexes.Delete(IndexName);
            }
        }

        protected ISearchIndexClient GetIndexClient()
        {
            return _searchServiceClient.Indexes.GetClient(IndexName);
        }
    }
}
