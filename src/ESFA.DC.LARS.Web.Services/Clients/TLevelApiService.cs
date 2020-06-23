using ESFA.DC.LARS.Web.Interfaces.Services;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class TLevelApiService : FrameworkApiService, ITLevelsAPIService
    {
        public TLevelApiService(IClientService clientService)
            : base(clientService)
        {
            Url = "TLevel";
        }
    }
}