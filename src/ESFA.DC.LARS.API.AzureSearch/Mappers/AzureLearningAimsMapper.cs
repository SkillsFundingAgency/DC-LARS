using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLearningAimsMapper : IMapper<LearningAimModel, Models.LearningAimModel>
    {
        public Models.LearningAimModel Map(LearningAimModel input)
        {
            return new Models.LearningAimModel
            {
                LearnAimRef = input.LearnAimRef,
                LearningAimTitle = input.LearningAimTitle,
                Type = input.Type,
                AwardingBody = input.AwardingBody,
                Level = input.Level,
                GuidedLearningHours = input.GuidedLearningHours
            };
        }
    }
}