using ESFA.DC.LARS.Web.Interfaces.Services;

namespace ESFA.DC.LARS.Web.Mappers
{
    public class StandardMapper : IMapper<API.Models.StandardModel, Models.StandardModel>
    {
        public Models.StandardModel Map(API.Models.StandardModel input)
        {
            return new Models.StandardModel
            {
                StandardCode = input.StandardCode,
                StandardName = input.StandardName,
                StandardSectorCodeDesc2 = input.StandardSectorCodeDesc2,
                Version = input.Version,
                NotionalEndLevel = input.NotionalEndLevel,
                EffectiveFrom = input.EffectiveFrom,
                LastDateStarts = input.LastDateStarts,
                EffectiveTo = input.EffectiveTo
            };
        }
    }
}