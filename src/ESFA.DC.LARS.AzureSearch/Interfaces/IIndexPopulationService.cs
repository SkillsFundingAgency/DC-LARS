using System.Threading.Tasks;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexPopulationService
    {
        Task PopulateIndexAsync();

        void CreateIndex();

        void DeleteIndex();
    }
}