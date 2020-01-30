using System.Threading.Tasks;

namespace ESFA.DC.LARS.API.Interfaces.AzureSearch
{
    public interface IAzureLookupService
    {
        Task<Models.LookUpModel> GetLookups();
    }
}