using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StandardController : ControllerBase
    {
        private readonly IStandardService _standardService;

        public StandardController(IStandardService standardService)
        {
            _standardService = standardService;
        }

        [HttpGet]
        public async Task<StandardModel> GetStandard(string standardCode)
        {
            return await _standardService.GetStandard(standardCode);
        }

        [HttpPost]
        public async Task<IEnumerable<StandardModel>> GetStandards(StandardSearchModel searchModel)
        {
            return await _standardService.GetStandards(searchModel);
        }
    }
}
