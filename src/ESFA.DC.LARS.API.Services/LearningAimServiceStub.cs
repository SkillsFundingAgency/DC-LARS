using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.Services.Factories;

namespace ESFA.DC.LARS.API.Services
{
    public class LearningAimServiceStub : ILearningAimService
    {
        public async Task<IEnumerable<LearningAimModel>> GetLearningAims(SearchModel searchParameters)
        {
            LearningAimFactoryStub learningAimFactory = new LearningAimFactoryStub();

            return new List<LearningAimModel> { learningAimFactory.GetLearningAim() };
        }
    }
}