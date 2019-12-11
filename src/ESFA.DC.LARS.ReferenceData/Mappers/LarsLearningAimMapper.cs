using ESFA.DC.ILR.ReferenceDataService.Model.LARS;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.ReferenceData.Mappers
{
    public class LarsLearningAimMapper : IMapper<LARSLearningDelivery, LearningAimModel>
    {
         public LearningAimModel Map(LARSLearningDelivery ld)
        {
            return new LearningAimModel
            {
                LearnAimRef = ld.LearnAimRef,
                Level = ld.NotionalNVQLevel,
                AwardingBody = ld.AwardOrgCode,
                LearningAimTitle = ld.LearnAimRefTitle,
                Type = ld.LearnAimRefType
            };
        }
    }
}