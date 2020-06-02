using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureRelatedLearningAimMapper : IMapper<RelatedLearningAimModel, Models.RelatedLearningAimModel>
    {
        public Models.RelatedLearningAimModel Map(RelatedLearningAimModel input)
        {
            return new Models.RelatedLearningAimModel
            {
                LearnAimRef = input.LearnAimRef,
                LearningAimTitle = input.LearningAimTitle,
                Level = input.Level,
                AwardingBodyCode = input.AwardingBodyCode,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                ComponentType = input.ComponentType,
                ComponentTypeDesc = input.ComponentTypeDesc
            };
        }
    }
}