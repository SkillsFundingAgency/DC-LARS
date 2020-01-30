using System;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.Azure.Search;

namespace ESFA.DC.LARS.API.AzureSearch
{
    public class AzureLookupService : IAzureLookupService
    {
        private const string LookupIndexKey = "1";

        private readonly ITelemetry _telemetry;
        private readonly ILookupIndexService _lookupIndexService;
        private readonly IMapper<LookUpModel, Models.LookUpModel> _mapper;

        public AzureLookupService(
            ITelemetry telemetry,
            ILookupIndexService lookupIndexService,
            IMapper<LookUpModel, Models.LookUpModel> mapper)
        {
            _telemetry = telemetry;
            _lookupIndexService = lookupIndexService;
            _mapper = mapper;
        }

        public async Task<Models.LookUpModel> GetLookups()
        {
            Models.LookUpModel lookups;

            try
            {
                var result = await _lookupIndexService.Documents.GetAsync<LookUpModel>(LookupIndexKey);
                lookups = _mapper.Map(result);
            }
            catch (Exception ex)
            {
                _telemetry.TrackEvent(ex.Message);
                throw;
            }

            return lookups;
        }
    }
}