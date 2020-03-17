using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface IFrameworkApiService
    {
        Task<FrameworkModel> GetLearningAim(int frameworkCode, int programType, int pathwayCode);
    }
}