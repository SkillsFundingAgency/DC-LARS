using System;
using Microsoft.Azure.Search;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceBus.Messaging;

namespace ESFA.DC.LARS.AzureSearch
{
    public class Functions
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IIndexPopulationService _indexPopulationService;
        private readonly ISearchServiceClient _serviceClient;

        public Functions(
            ISearchServiceClient serviceClient,
            IConfigurationRoot configuration,
            IIndexPopulationService indexPopulationService)
        {
            _configuration = configuration;
            _indexPopulationService = indexPopulationService;
            _serviceClient = serviceClient;
        }

        public void ProcessQueueMessage([ServiceBusTrigger(queueName: "LARSUpdate", Connection = "AzureWebJobsServiceBus")]BrokeredMessage message)
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