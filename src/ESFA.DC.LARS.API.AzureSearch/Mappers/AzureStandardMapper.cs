using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureStandardMapper : IMapper<StandardModel, Models.StandardModel>
    {
        public AzureStandardMapper()
        {
        }

        public Models.StandardModel Map(StandardModel input)
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