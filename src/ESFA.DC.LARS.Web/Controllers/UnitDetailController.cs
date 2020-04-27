using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("UnitDetails")]
    public class UnitDetailController : LearningAimDetailController
    {
       public UnitDetailController(
            IUnitsApiService unitsApiService,
            ILookupApiService lookupApiService)
        : base(unitsApiService, lookupApiService)
        {
            FormContoller = "UnitDetail";
            CategoryController = "UnitCategory";
        }
    }
}