using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLookupMapper : IMapper<LookUpModel, Models.LookUpModel>
    {
        private readonly IMapper<NotionalNVQLevel2Model, Models.NotionalNVQLevel2Model> _nvqMapper;

        public AzureLookupMapper(
            IMapper<NotionalNVQLevel2Model, Models.NotionalNVQLevel2Model> nvqMapper)
        {
            _nvqMapper = nvqMapper;
        }

        public Models.LookUpModel Map(LookUpModel input)
        {
            return new Models.LookUpModel
            {
                LookUpKey = input.LookUpKey,
                NotionalNvqLevel2Lookups = input.NotionalNvqLevel2Lookups.Select(_nvqMapper.Map)
            };
        }
    }
}