using System.Collections.Generic;
using System.Threading.Tasks;
using StandardModel = ESFA.DC.LARS.Web.Models.StandardModel;
using StandardSearchModel = ESFA.DC.LARS.Web.Models.StandardSearchModel;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface IStandardApiService
    {
        Task<StandardModel> GetStandard(string standardCode);

        Task<IEnumerable<StandardModel>> GetStandards(StandardSearchModel content);
    }
}