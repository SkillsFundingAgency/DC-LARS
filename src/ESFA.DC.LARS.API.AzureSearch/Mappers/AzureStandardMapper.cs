using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureStandardMapper : IMapper<StandardModel, Models.StandardModel>
    {
        private readonly IMapper<RelatedLearningAimModel, Models.RelatedLearningAimModel> _relatedAimMapper;
        private readonly IMapper<CommonComponentModel, Models.CommonComponentModel> _commonComponentMapper;

        public AzureStandardMapper(
                                    IMapper<CommonComponentModel, Models.CommonComponentModel> commonComponentMapper,
                                    IMapper<RelatedLearningAimModel, Models.RelatedLearningAimModel> relatedAimMapper)
        {
            _relatedAimMapper = relatedAimMapper;
            _commonComponentMapper = commonComponentMapper;
        }

        public Models.StandardModel Map(StandardModel input)
        {
            return new Models.StandardModel
            {
                StandardCode = input.StandardCode,
                StandardName = input.StandardName,
                StandardSectorCode = input.StandardSectorCode,
                StandardSectorCodeDesc2 = input.StandardSectorCodeDesc2,
                Version = input.Version,
                NotionalEndLevel = input.NotionalEndLevel,
                EffectiveFrom = input.EffectiveFrom,
                LastDateStarts = input.LastDateStarts,
                EffectiveTo = input.EffectiveTo,
                SectorSubjectAreaTier1 = input.SectorSubjectAreaTier1,
                SectorSubjectAreaTier1Desc = input.SectorSubjectAreaTier1Desc,
                SectorSubjectAreaTier2 = input.SectorSubjectAreaTier2,
                SectorSubjectAreaTier2Desc = input.SectorSubjectAreaTier2Desc,
                IntegratedDegreeStandard = input.IntegratedDegreeStandard,
                OtherBodyApprovalRequired = input.OtherBodyApprovalRequired,
                CommonComponents = input.CommonComponents?.Select(cc => _commonComponentMapper.Map(cc)).ToList(),
                LearningAims = input.LearningAims?.Select(aim => _relatedAimMapper.Map(aim)).ToList()
            };
        }
    }
}