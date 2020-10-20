using ESFA.DC.LARS.Web.Interfaces.Services;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class UnitsApiService : LearningAimsApiService, IUnitsApiService
    {
        public UnitsApiService(IClientService clientService)
        : base(clientService)
        {
            Url = "Units";
        }
    }
}