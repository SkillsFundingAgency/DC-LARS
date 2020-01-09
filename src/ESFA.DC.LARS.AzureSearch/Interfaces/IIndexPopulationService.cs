using ESFA.DC.LARS.AzureSearch.Configuration;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexPopulationService
    {
        void UploadDocuments(ISearchIndexClient indexClient, ConnectionStrings connectionStrings);

        void DeleteIndexIfExists(string indexName, ISearchServiceClient serviceClient);

        void CreateIndex(string indexName, ISearchServiceClient serviceClient);
    }
}