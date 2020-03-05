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
    public class FrameworkController : ControllerBase
    {
        private readonly IFrameworkService _frameworkService;

        public FrameworkController(IFrameworkService frameworkService)
        {
            _frameworkService = frameworkService;
        }

        [HttpGet]
        public async Task<FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
        {
            return await _frameworkService.GetFramework(frameworkCode, programType, pathwayCode);
        }
    }
}