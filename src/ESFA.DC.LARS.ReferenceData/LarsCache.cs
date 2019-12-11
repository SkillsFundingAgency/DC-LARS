using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.ReferenceData
{
    public class LarsCache : ILarsCache
    {
        private readonly ILarsJsonService _larsJsonService;

        public LarsCache(ILarsJsonService larsJsonService)
        {
            _larsJsonService = larsJsonService;
        }

        private static IEnumerable<LearningAimModel> LearningAims { get; set; }

        public async Task<IEnumerable<LearningAimModel>> GetLearningAims(bool isDirty)
        {
            if (LearningAims == null || isDirty)
            {
                LearningAims = await _larsJsonService.GetLarsLearningDeliveriesFromJsonFile();
            }

            return LearningAims;
        }
    }
}