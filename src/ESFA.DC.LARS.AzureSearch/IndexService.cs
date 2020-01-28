using System;
using ESFA.DC.LARS.AzureSearch.Configuration;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;

namespace ESFA.DC.LARS.AzureSearch
{
    public class IndexService : IIndexService
    {
        private readonly IConfiguration _configuration;
        private readonly IIndexPopulationService _indexPopulationService;
        private readonly ConnectionStrings _connectionStrings;
        private readonly ISearchServiceClient _serviceClient;

        public IndexService(
            ISearchServiceClient serviceClient,
            IConfiguration configuration,
            IIndexPopulationService indexPopulationService,
            ConnectionStrings connectionStrings)
        {
            _configuration = configuration;
            _indexPopulationService = indexPopulationService;
            _connectionStrings = connectionStrings;
            _serviceClient = serviceClient;
        }

        public void UpdateIndexes()
        {
            string indexName = _configuration["SearchIndexName"];

            Console.WriteLine("{0}", "Deleting index...\n");
            _indexPopulationService.DeleteIndexIfExists(indexName, _serviceClient);

            Console.WriteLine("{0}", "Creating index...\n");
            _indexPopulationService.CreateIndex(indexName, _serviceClient);

            // Uncomment next 3 lines in "2 - Load documents"
            ISearchIndexClient indexClient = _serviceClient.Indexes.GetClient(indexName);
            Console.WriteLine("{0}", "Uploading documents...\n");
            _indexPopulationService.UploadDocuments(indexClient, _connectionStrings);
        }
    }
}