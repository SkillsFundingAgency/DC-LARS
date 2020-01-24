using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.Telemetry.Interfaces;

namespace ESFA.DC.LARS.API.Services
{
    public class LearningAimJsonService : ILearningAimService
    {
        private readonly ITelemetry _telemetry;
        private readonly ILarsCache _larsCache;

        public LearningAimJsonService(
            ITelemetry telemetry,
            ILarsCache larsCache)
        {
            _telemetry = telemetry;
            _larsCache = larsCache;
        }

        public Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters)
        {
            _telemetry.TrackEvent("In Get Learning Aims");
            return _larsCache.GetLearningAims(searchParameters, false);
        }

        public Task<LearningAimModel> GetLearningAim(string learnAimRef)
        {
            throw new System.NotImplementedException();
        }
    }
}