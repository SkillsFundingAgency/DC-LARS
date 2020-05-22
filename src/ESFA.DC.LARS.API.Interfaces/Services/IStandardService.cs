using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface IStandardService
    {
        Task<StandardModel> GetStandard(string standardCode);

        Task<IEnumerable<StandardModel>> GetStandards(StandardSearchModel searchModel);
    }
}