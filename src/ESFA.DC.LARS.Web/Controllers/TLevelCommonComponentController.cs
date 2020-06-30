using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("TLevelCommonComponents")]
    public class TLevelCommonComponentController : AbstractFrameworkComponentController
    {
        public TLevelCommonComponentController(ITLevelApiService tlevelApiService)
            : base(tlevelApiService)
        {
        }
    }
}
