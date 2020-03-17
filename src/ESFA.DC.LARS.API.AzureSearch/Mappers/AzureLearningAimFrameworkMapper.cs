using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLearningAimFrameworkMapper : IMapper<LearningAimFrameworkModel, Models.LearningAimFrameworkModel>
    {
        public Models.LearningAimFrameworkModel Map(LearningAimFrameworkModel input)
        {
            return new Models.LearningAimFrameworkModel
            {
                LearnAimRef = input.LearnAimRef,
                LearningAimTitle = input.LearningAimTitle,
                FrameworkTitle = input.FrameworkTitle,
                FrameworkCode = input.FrameworkCode,
                PathwayCode = input.PathwayCode,
                ProgramType = input.ProgramType,
                PathwayName = input.PathwayName,
                ProgramTypeDesc = input.ProgramTypeDesc,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                ComponentType = input.ComponentType,
                ComponentTypeDesc = input.ComponentTypeDesc,
                IssuingAuthority = input.IssuingAuthority,
                IssuingAuthorityDesc = input.IssuingAuthorityDesc
            };
        }
    }
}
