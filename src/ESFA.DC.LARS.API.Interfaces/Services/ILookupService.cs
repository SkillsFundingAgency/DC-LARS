using System.Threading.Tasks;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface ILookupService
    {
        Task<Models.LookUpModel> GetLookups();
    }
}