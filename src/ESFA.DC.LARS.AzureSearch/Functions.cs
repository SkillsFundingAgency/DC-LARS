using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace ESFA.DC.LARS.AzureSearch
{
    public class Functions
    {
        private const string QueueName = "LARSUpdate";
        private const string ServiceBusConnectionStringName = "AzureWebJobsServiceBus";

        private readonly IIndexService _indexService;

        public Functions(
            IIndexService indexService)
        {
            _indexService = indexService;
        }

        public void ProcessQueueMessage([ServiceBusTrigger(queueName: QueueName, Connection = ServiceBusConnectionStringName)]BrokeredMessage message)
        {
            _indexService.UpdateIndexes();
        }
    }
}