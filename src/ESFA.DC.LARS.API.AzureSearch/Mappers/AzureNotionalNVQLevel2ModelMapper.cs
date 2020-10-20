using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureNotionalNVQLevel2ModelMapper : IMapper<NotionalNVQLevel2LookupModel, Models.NotionalNVQLevel2Model>
    {
        public Models.NotionalNVQLevel2Model Map(NotionalNVQLevel2LookupModel input)
        {
            return new Models.NotionalNVQLevel2Model
            {
                NotionalNVQLevelV2 = input.NotionalNVQLevelV2,
                NotionalNVQLevelV2Desc = input.NotionalNVQLevelV2Desc
            };
        }
    }
}