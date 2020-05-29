using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureStandardMapper : IMapper<StandardModel, Models.StandardModel>
    {
        private readonly IMapper<StandardFundingModel, Models.StandardFundingModel> _standardFundingModelMapper;
        private readonly IMapper<StandardApprenticeshipFundingModel, Models.StandardApprenticeshipFundingModel> _standardApprenticeshipFundingModelMapper;

        public AzureStandardMapper(
            IMapper<StandardFundingModel, Models.StandardFundingModel> standardFundingModelMapper,
            IMapper<StandardApprenticeshipFundingModel, Models.StandardApprenticeshipFundingModel> standardApprenticeshipFundingModelMapper)
        {
            _standardFundingModelMapper = standardFundingModelMapper;
            _standardApprenticeshipFundingModelMapper = standardApprenticeshipFundingModelMapper;
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
                StandardFundingModels = input.StandardFundingModels?.Select(sf => _standardFundingModelMapper.Map(sf)).ToList(),
                StandardApprenticeshipFundingModels = input.StandardApprenticeshipFundingModels?.Select(sf => _standardApprenticeshipFundingModelMapper.Map(sf)).ToList(),
            };
        }
    }
}