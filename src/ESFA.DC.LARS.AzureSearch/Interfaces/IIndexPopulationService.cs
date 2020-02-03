using System.Threading.Tasks;
using ESFA.DC.LARS.AzureSearch.Configuration;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexPopulationService
    {
        bool IsMatch(SearchIndexes index);

        void PopulateIndex(
            ISearchIndexClient indexClient,
            ConnectionStrings connectionStrings);

        void CreateIndex(string indexName, ISearchServiceClient serviceClient);
    }
}