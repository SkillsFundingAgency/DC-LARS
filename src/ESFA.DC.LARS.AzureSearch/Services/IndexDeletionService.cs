using ESFA.DC.LARS.AzureSearch.Interfaces;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class IndexDeletionService : IIndexDeletionService
    {
        public void DeleteIndexIfExists(string indexName, ISearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists(indexName))
            {
                serviceClient.Indexes.Delete(indexName);
            }
        }
    }
}