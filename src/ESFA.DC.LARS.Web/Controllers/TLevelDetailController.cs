using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("TLevelDetails")]
    public class TLevelDetailController : AbstractFrameworkDetailController
    {
        private const string CommonComponentController = "TLevelCommonComponent";

        public TLevelDetailController(ITLevelApiService tLevelAPIService)
            : base(tLevelAPIService, CommonComponentController)
        {
        }
    }
}