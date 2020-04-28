using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("UnitDetails")]
    public class UnitDetailController : AbstractAimDetailController
    {
       public UnitDetailController(
            IUnitsApiService unitsApiService,
            ILookupApiService lookupApiService)
        : base(unitsApiService, lookupApiService)
        {
            FormController = "UnitDetail";
            CategoryController = "UnitCategory";
        }
    }
}