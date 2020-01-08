using System;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;

namespace ESFA.DC.LARS.AzureSearch
{
    public class IndexService : IIndexService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IIndexPopulationService _indexPopulationService;
        private readonly ISearchServiceClient _serviceClient;

        public IndexService(
            ISearchServiceClient serviceClient,
            IConfigurationRoot configuration,
            IIndexPopulationService indexPopulationService)
        {
            _configuration = configuration;
            _indexPopulationService = indexPopulationService;
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
            _indexPopulationService.UploadDocuments(_configuration, indexClient);

            Console.WriteLine("{0}", "Complete.  Press any key to end application...\n");
            Console.ReadKey();
        }
    }
}