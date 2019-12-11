using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class LearningAimJsonService : ILearningAimService
    {
        private readonly ILarsCache _larsCache;

        public LearningAimJsonService(ILarsCache larsCache)
        {
            _larsCache = larsCache;
        }

        public Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters)
        {
            return _larsCache.GetLearningAims(false);
        }
    }
}