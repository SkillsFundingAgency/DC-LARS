using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexPopulationService
    {
        void UploadDocuments(IConfigurationRoot configuration, ISearchIndexClient indexClient);

        void DeleteIndexIfExists(string indexName, ISearchServiceClient serviceClient);

        void CreateIndex(string indexName, ISearchServiceClient serviceClient);
    }
}