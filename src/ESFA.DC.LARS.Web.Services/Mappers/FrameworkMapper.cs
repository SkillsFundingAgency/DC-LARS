using ESFA.DC.LARS.Web.Interfaces.Services;

namespace ESFA.DC.LARS.Web.Mappers
{
    public class FrameworkMapper : IMapper<API.Models.FrameworkModel, Models.FrameworkModel>
    {
        public Models.FrameworkModel Map(API.Models.FrameworkModel input)
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
                SectorSubjectAreaTier1 = input.SectorSubjectAreaTier1,
                SectorSubjectAreaTier1Desc = input.SectorSubjectAreaTier1Desc,
                SectorSubjectAreaTier2 = input.SectorSubjectAreaTier2,
                SectorSubjectAreaTier2Desc = input.SectorSubjectAreaTier2Desc,
                IssuingAuthority = input.IssuingAuthority,
                IssuingAuthorityDesc = input.IssuingAuthorityDesc
            };
        }
    }
}