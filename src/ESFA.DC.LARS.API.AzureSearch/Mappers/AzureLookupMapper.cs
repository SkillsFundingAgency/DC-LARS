using System.Linq;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureLookupMapper : IMapper<LookUpModel, Models.LookUpModel>
    {
        private readonly IMapper<AcademicYearLookupModel, Models.AcademicYearLookupModel> _yearMapper;
        private readonly IMapper<NotionalNVQLevel2LookupModel, Models.NotionalNVQLevel2Model> _nvqMapper;
        private readonly IMapper<ValidityFundingMappingLookupModel, Models.ValidityFundingMappingLookupModel> _mappingMapper;

        public AzureLookupMapper(
            IMapper<AcademicYearLookupModel, Models.AcademicYearLookupModel> yearMapper,
            IMapper<NotionalNVQLevel2LookupModel, Models.NotionalNVQLevel2Model> nvqMapper,
            IMapper<ValidityFundingMappingLookupModel, Models.ValidityFundingMappingLookupModel> mappingMapper)
        {
            _yearMapper = yearMapper;
            _nvqMapper = nvqMapper;
            _mappingMapper = mappingMapper;
        }

        public Models.LookUpModel Map(LookUpModel input)
        {
            return new Models.LookUpModel
            {
                LookUpKey = input.LookUpKey,
                AcademicYearLookups = input.AcademicYearLookups?.Select(_yearMapper.Map),
                NotionalNvqLevel2Lookups = input.NotionalNvqLevel2Lookups?.Select(_nvqMapper.Map),
                ValidityFundingMappingLookups = input.ValidityFundingMappingLookups?.Select(_mappingMapper.Map)
            };
        }
    }
}