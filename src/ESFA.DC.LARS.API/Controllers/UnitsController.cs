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
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpPost]
        public async Task<IEnumerable<LearningAimModel>> GetLearningAimsAsync(
            [FromBody] LearningAimsSearchModel searchParameters)
        {
            var aims = await _unitService.GetLearningAims(searchParameters);
            return aims;
        }

        [HttpGet]
        public async Task<LearningAimModel> GetLearningAimAsync(string learnAimRef)
        {
            return await _unitService.GetLearningAim(learnAimRef);
        }
    }
}