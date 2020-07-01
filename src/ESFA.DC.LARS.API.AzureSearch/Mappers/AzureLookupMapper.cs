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
        private readonly IMapper<ValidityCategoryLookupModel, Models.ValidityCategoryLookupModel> _validityMapper;
        private readonly IMapper<AwardingBodyLookupModel, Models.AwardingBodyLookupModel> _awardingBodyMapper;
        private readonly IMapper<FrameworkTypeLookupModel, Models.FrameworkTypeLookupModel> _frameworkTypeMapper;
        private readonly IMapper<IssuingAuthorityLookupModel, Models.IssuingAuthorityLookupModel> _issuingAuthorityMapper;
        private readonly IMapper<StandardSectorLookupModel, Models.StandardSectorLookupModel> _standardSectorMapper;
        private readonly IMapper<SectorSubjectAreaTier1LookupModel, Models.SectorSubjectAreaTier1LookupModel> _sectorSubjectAreaTier1Mapper;

        public AzureLookupMapper(
            IMapper<AcademicYearLookupModel, Models.AcademicYearLookupModel> yearMapper,
            IMapper<NotionalNVQLevel2LookupModel, Models.NotionalNVQLevel2Model> nvqMapper,
            IMapper<ValidityFundingMappingLookupModel, Models.ValidityFundingMappingLookupModel> mappingMapper,
            IMapper<ValidityCategoryLookupModel, Models.ValidityCategoryLookupModel> validityMapper,
            IMapper<AwardingBodyLookupModel, Models.AwardingBodyLookupModel> awardingBodyMapper,
            IMapper<FrameworkTypeLookupModel, Models.FrameworkTypeLookupModel> frameworkTypeMapper,
            IMapper<IssuingAuthorityLookupModel, Models.IssuingAuthorityLookupModel> issuingAuthorityMapper,
            IMapper<StandardSectorLookupModel, Models.StandardSectorLookupModel> standardSectorMapper,
            IMapper<SectorSubjectAreaTier1LookupModel, Models.SectorSubjectAreaTier1LookupModel> sectorSubjectAreaTier1Mapper)
        {
            _yearMapper = yearMapper;
            _nvqMapper = nvqMapper;
            _mappingMapper = mappingMapper;
            _validityMapper = validityMapper;
            _awardingBodyMapper = awardingBodyMapper;
            _frameworkTypeMapper = frameworkTypeMapper;
            _issuingAuthorityMapper = issuingAuthorityMapper;
            _standardSectorMapper = standardSectorMapper;
            _sectorSubjectAreaTier1Mapper = sectorSubjectAreaTier1Mapper;
        }

        public Models.LookUpModel Map(LookUpModel input)
        {
            return new Models.LookUpModel
            {
                LookUpKey = input.LookUpKey,
                AcademicYearLookups = input.AcademicYearLookups?.Select(_yearMapper.Map).ToList(),
                NotionalNvqLevel2Lookups = input.NotionalNvqLevel2Lookups?.Select(_nvqMapper.Map).ToList(),
                ValidityCategoryLookups = input.ValidityCategoryLookups?.Select(_validityMapper.Map).ToList(),
                ValidityFundingMappingLookups = input.ValidityFundingMappingLookups?.Select(_mappingMapper.Map).ToList(),
                AwardingBodyLookups = input.AwardingBodyLookups?.Select(_awardingBodyMapper.Map).ToList(),
                FrameworkTypeLookups = input.FrameworkTypeLookups?.Select(_frameworkTypeMapper.Map).ToList(),
                TLevelTypeLookups = input.TLevelTypeLookups?.Select(_frameworkTypeMapper.Map).ToList(),
                IssuingAuthorityLookups = input.IssuingAuthorityLookups?.Select(_issuingAuthorityMapper.Map).ToList(),
                StandardSectorLookups = input.StandardSectorLookups?.Select(_standardSectorMapper.Map).ToList(),
                SectorSubjectAreaTier1Lookups = input.SectorSubjectAreaTier1Lookups?.Select(_sectorSubjectAreaTier1Mapper.Map).ToList()
            };
        }
    }
}