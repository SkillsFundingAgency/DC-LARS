using System;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.Telemetry.Interfaces;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureLookupService : IAzureLookupService
    {
        private const string LookupIndexKey = "1";

        private readonly ITelemetry _telemetry;
        private readonly ILookupIndexService _lookupIndexService;
        private readonly IMapper<LookUpModel, Models.LookUpModel> _mapper;
        private readonly IAzureService _azureService;

        public AzureLookupService(
            ITelemetry telemetry,
            ILookupIndexService lookupIndexService,
            IMapper<LookUpModel, Models.LookUpModel> mapper,
            IAzureService azureService)
        {
            _telemetry = telemetry;
            _lookupIndexService = lookupIndexService;
            _mapper = mapper;
            _azureService = azureService;
        }

        public async Task<Models.LookUpModel> GetLookups()
        {
            Models.LookUpModel lookups;

            var result = await _azureService.GetAsync<LookUpModel>(_lookupIndexService, LookupIndexKey);
            lookups = _mapper.Map(result);

            return lookups;
        }
    }
}