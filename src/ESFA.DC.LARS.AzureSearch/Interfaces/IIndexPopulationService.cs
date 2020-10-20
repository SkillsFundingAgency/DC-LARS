using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexPopulationService
    {
        Task PopulateIndexAsync(CancellationToken cancellationToken);

        void CreateIndex();

        void DeleteIndex();
    }
}