using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Interfaces.AzureSearch
{
    public interface IAzureFrameworkService
    {
        Task<FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode);

        Task<IEnumerable<FrameworkModel>> GetFrameworks(FrameworkSearchModel searchModel);
    }
}