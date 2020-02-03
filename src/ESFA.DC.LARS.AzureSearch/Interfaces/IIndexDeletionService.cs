using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexDeletionService
    {
        void DeleteIndexIfExists(string indexName, ISearchServiceClient serviceClient);
    }
}