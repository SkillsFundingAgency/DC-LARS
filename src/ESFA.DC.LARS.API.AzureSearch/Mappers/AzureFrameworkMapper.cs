using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureFrameworkMapper : IMapper<FrameworkModel, Models.FrameworkModel>
    {
        private readonly IMapper<FrameworkAimModel, Models.FrameworkAimModel> _frameworkAimMapper;

        public AzureFrameworkMapper(IMapper<FrameworkAimModel, Models.FrameworkAimModel> frameworkAimMapper)
        {
            _frameworkAimMapper = frameworkAimMapper;
        }

        public Models.FrameworkModel Map(FrameworkModel input)
        {
            return new Models.FrameworkModel
            {
                FrameworkCode = input.FrameworkCode,
                ProgramType = input.ProgramType,
                ProgramTypeName = input.ProgramTypeName,
                PathwayCode = input.PathwayCode,
                PathwayName = input.PathwayName,
                FrameworkTitle = input.FrameworkTitle,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                SectorSubjectAreaTier2 = input.SectorSubjectAreaTier2,
                SectorSubjectAreaTier2Desc = input.SectorSubjectAreaTier2Desc,
                IssuingAuthority = input.IssuingAuthority,
                IssuingAuthorityDesc = input.IssuingAuthorityDesc,
                LearningAims = input.LearningAims?.Select(aim => _frameworkAimMapper.Map(aim)).ToList()
            };
        }
    }
}