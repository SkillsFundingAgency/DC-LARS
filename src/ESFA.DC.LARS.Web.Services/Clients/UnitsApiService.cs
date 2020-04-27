using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class UnitsApiService : LearningAimsApiService, IUnitsApiService
    {
        public UnitsApiService(IClientService clientService)
        : base(clientService)
        {
            Url = "Units";
        }

        public async Task<LearningAimModel> GetLearningAim(string learnAimRef)
        {
            return await base.GetLearningAim(learnAimRef);
        }
    }
}