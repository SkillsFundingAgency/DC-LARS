using System.Threading.Tasks;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface IFrameworkService
    {
        Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode);
    }
}