using ESFA.DC.LARS.API.AzureSearch.Constants;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch.Clients
{
    public class LearningAimsClient : SearchIndexClient, ILearningDeliveryIndexService
    {
        public LearningAimsClient(IAzureSettings settings)
            : base(settings.SearchServiceName, AzureIndexes.LearningDeliveryIndex, new SearchCredentials(settings.SearchServiceAdminApiKey))
        {
        }
    }
}