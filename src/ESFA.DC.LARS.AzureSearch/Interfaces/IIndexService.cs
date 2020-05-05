using System.Threading.Tasks;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IIndexService
    {
        Task UpdateIndexesAsync();
    }
}