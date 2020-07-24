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
    public class DownloadDataController : ControllerBase
    {
        private readonly IDownloadDataService _downloadDataService;

        public DownloadDataController(IDownloadDataService downloadDataService)
        {
            _downloadDataService = downloadDataService;
        }

        [HttpGet]
        public async Task<List<DownloadDetailsModel>> GetLookups(string key)
        {
            return (await _downloadDataService.GetDownloadDetails(key)).ToList();
        }
    }
}