using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("UnitCategories")]
    public class UnitCategoryController : AbstractAimCategoryController
    {
        public UnitCategoryController(IUnitsApiService unitsApiService)
            : base(unitsApiService)
        {
        }
    }
}