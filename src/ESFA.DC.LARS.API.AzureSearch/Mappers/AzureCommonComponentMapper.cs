using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureCommonComponentMapper : IMapper<CommonComponentModel, Models.CommonComponentModel>
    {
        public Models.CommonComponentModel Map(CommonComponentModel input)
        {
            return new Models.CommonComponentModel
            {
                CommonComponent = input.CommonComponent,
                Description = input.Description,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                MinLevel = input.MinLevel,
            };
        }
    }
}
