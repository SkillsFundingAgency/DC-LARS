using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TLevelController : ControllerBase
    {
        private readonly ITLevelService _tlevelService;

        public TLevelController(ITLevelService tlevelService)
        {
            _tlevelService = tlevelService;
        }

        [HttpGet]
        public async Task<FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            return await _tlevelService.GetFramework(frameworkCode, programType, pathwayCode);
        }

        [HttpPost]
        public async Task<IEnumerable<FrameworkModel>> GetFrameworks(FrameworkSearchModel searchModel)
        {
            return await _tlevelService.GetFrameworks(searchModel);
        }
    }
}