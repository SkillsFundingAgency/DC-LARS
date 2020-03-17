using System.Threading.Tasks;

namespace ESFA.DC.LARS.API.Interfaces.AzureSearch
{
    public interface IAzureFrameworkService
    {
        Task<Models.FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode);
    }
}