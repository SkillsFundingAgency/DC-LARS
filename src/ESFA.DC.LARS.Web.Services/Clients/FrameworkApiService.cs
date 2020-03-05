using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Clients
{
    public class FrameworkApiService : IFrameworkApiService
    {
        private const string Url = "Framework";
        private const string FrameworkCodeParameterName = "frameworkCode";
        private const string ProgramTypeParameterName = "programType";
        private const string PathwayCodeParameterName = "pathwayCode";

        private readonly IClientService _clientService;

        public FrameworkApiService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<FrameworkModel> GetLearningAim(int frameworkCode, int programType, int pathwayCode)
        {
            var parameters = new Dictionary<string, object>
            {
                { FrameworkCodeParameterName, frameworkCode },
                { ProgramTypeParameterName, programType },
                { PathwayCodeParameterName, pathwayCode }
            };

            var response = await _clientService.GetAsync<FrameworkModel>(Url, parameters);

            return response;
        }
    }
}