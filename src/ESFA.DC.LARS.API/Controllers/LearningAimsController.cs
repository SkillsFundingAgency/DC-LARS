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
    public class LearningAimsController : ControllerBase
    {
        private readonly ILearningAimService _learningAimService;

        public LearningAimsController(ILearningAimService learningAimService)
        {
            _learningAimService = learningAimService;
        }

        [HttpPost]
        public async Task<IEnumerable<LearningAimModel>> GetLearningAimsAsync([FromBody]LearningAimsSearchModel searchParameters)
        {
            var aims = await _learningAimService.GetLearningAims(searchParameters);
            return aims;
        }

        [HttpGet]
        public async Task<LearningAimModel> GetLearningAimAsync(string learnAimRef)
        {
            return await _learningAimService.GetLearningAim(learnAimRef);
        }
    }
}