using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureCommonComponentMapper : IMapper<FrameworkCommonComponentModel, Models.FrameworkCommonComponentModel>
    {
        public Models.FrameworkCommonComponentModel Map(FrameworkCommonComponentModel input)
        {
            return new Models.FrameworkCommonComponentModel
            {
                CommonComponent = input.CommonComponent,
                Description = input.Description,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo,
                FrameworkCode = input.FrameworkCode,
                PathwayCode = input.PathwayCode,
                MinLevel = input.MinLevel,
                ProgramType = input.ProgramType
            };
        }
    }
}
