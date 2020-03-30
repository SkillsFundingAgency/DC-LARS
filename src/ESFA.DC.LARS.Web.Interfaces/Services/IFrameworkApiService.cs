using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface IFrameworkApiService
    {
        Task<FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode);

        Task<IEnumerable<FrameworkModel>> GetFrameworks(FrameworkSearchModel content);
    }
}