using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureFrameworkAimMapper : IMapper<FrameworkAimModel, Models.FrameworkAimModel>
    {
        public Models.FrameworkAimModel Map(FrameworkAimModel input)
        {
            return new Models.FrameworkAimModel
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