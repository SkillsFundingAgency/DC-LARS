using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Services
{
    public class LearningAimJsonService : ILearningAimService
    {
        private readonly ILarsJsonService _larsJsonService;

        public LearningAimJsonService(ILarsJsonService larsJsonService)
        {
            _larsJsonService = larsJsonService;
        }

        public Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters)
        {
            return _larsJsonService.GetLarsLearningDeliveriesFromJsonFile();
        }
    }
}