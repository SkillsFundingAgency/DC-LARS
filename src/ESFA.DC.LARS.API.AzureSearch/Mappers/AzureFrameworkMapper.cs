using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureFrameworkMapper : IMapper<FrameworkModel, Models.FrameworkModel>
    {
        public Models.FrameworkModel Map(FrameworkModel input)
        {
            return new Models.FrameworkModel
            {
                FrameworkCode = input.FrameworkCode,
                ProgramType = input.ProgramType,
                PathwayCode = input.PathwayCode,
                PathwayName = input.PathwayName,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                SectorSubjectAreaTier2 = input.SectorSubjectAreaTier2,
                SectorSubjectAreaTier2Desc = input.SectorSubjectAreaTier2Desc,
                IssuingAuthority = input.IssuingAuthority,
                IssuingAuthorityDesc = input.IssuingAuthorityDesc
            };
        }
    }
}