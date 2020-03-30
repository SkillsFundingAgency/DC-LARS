using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureFrameworkTypeLookupMapper : IMapper<FrameworkTypeLookupModel, Models.FrameworkTypeLookupModel>
    {
        public Models.FrameworkTypeLookupModel Map(FrameworkTypeLookupModel input)
        {
            return new Models.FrameworkTypeLookupModel
            {
                FrameworkType = input.FrameworkType,
                FrameworkTypeDesc = input.FrameworkTypeDesc
            };
        }
    }
}