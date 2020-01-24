using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.Services.Factories;

namespace ESFA.DC.LARS.API.Services
{
    public class LearningAimServiceStub : ILearningAimService
    {
        public async Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters)
        {
            var learningAimFactory = new LearningAimFactoryStub();

            return new List<LearningAimModel> { learningAimFactory.GetLearningAim() };
        }

        public async Task<LearningAimModel> GetLearningAim(string learnAimRef)
        {
            var learningAimFactory = new LearningAimFactoryStub();

            return learningAimFactory.GetLearningAim();
        }
    }
}