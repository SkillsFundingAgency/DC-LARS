using ESFA.DC.LARS.Web.Interfaces.Services;

namespace ESFA.DC.LARS.Web.Mappers
{
    public class LearningAimMapper : IMapper<API.Models.LearningAimModel, Models.LearningAimModel>
    {
        public Models.LearningAimModel Map(API.Models.LearningAimModel input)
        {
            return new Models.LearningAimModel
            {
                LearnAimRef = input.LearnAimRef,
                Level = input.Level,
                AwardingBodyCode = input.AwardingBodyCode,
                AwardingBodyName = input.AwardingBodyName,
                LearningAimTitle = input.LearningAimTitle,
                Type = input.Type
            };
        }
    }
}