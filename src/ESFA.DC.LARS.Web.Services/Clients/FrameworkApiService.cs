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

        public async Task<FrameworkModel> GetFramework(int frameworkCode, int programType, int pathwayCode)
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

        public async Task<IEnumerable<FrameworkModel>> GetFrameworks(FrameworkSearchModel content)
        {
            var response = await _clientService.PostAsync<FrameworkSearchModel, IEnumerable<FrameworkModel>>(Url, content);

            return response;
        }
    }
}