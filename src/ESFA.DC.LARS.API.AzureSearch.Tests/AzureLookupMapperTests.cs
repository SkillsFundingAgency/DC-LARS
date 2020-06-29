using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureLookupMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var mappingMapperMock = new Mock<IMapper<ValidityFundingMappingLookupModel, Models.ValidityFundingMappingLookupModel>>();
            mappingMapperMock
                .Setup(m => m.Map(It.IsAny<ValidityFundingMappingLookupModel>()))
                .Returns(new Models.ValidityFundingMappingLookupModel());

            var validityMapperMock = new Mock<IMapper<ValidityCategoryLookupModel, Models.ValidityCategoryLookupModel>>();
            validityMapperMock
                .Setup(m => m.Map(It.IsAny<ValidityCategoryLookupModel>()))
                .Returns(new Models.ValidityCategoryLookupModel());

            var academicYearLookupMapperMock = new Mock<IMapper<AcademicYearLookupModel, Models.AcademicYearLookupModel>>();
            academicYearLookupMapperMock
                .Setup(m => m.Map(It.IsAny<AcademicYearLookupModel>()))
                .Returns(new Models.AcademicYearLookupModel());

            var nvqMapperMock = new Mock<IMapper<NotionalNVQLevel2LookupModel, Models.NotionalNVQLevel2Model>>();
            nvqMapperMock
                .Setup(m => m.Map(It.IsAny<NotionalNVQLevel2LookupModel>()))
                .Returns(new Models.NotionalNVQLevel2Model());

            var awardingBodyMapperMock = new Mock<IMapper<AwardingBodyLookupModel, Models.AwardingBodyLookupModel>>();
            awardingBodyMapperMock
                .Setup(m => m.Map(It.IsAny<AwardingBodyLookupModel>()))
                .Returns(new Models.AwardingBodyLookupModel());

            var frameworkTypeMapperMock = new Mock<IMapper<FrameworkTypeLookupModel, Models.FrameworkTypeLookupModel>>();
            frameworkTypeMapperMock
                .Setup(m => m.Map(It.IsAny<FrameworkTypeLookupModel>()))
                .Returns(new Models.FrameworkTypeLookupModel());

            var issuingAuthorityMapperMock = new Mock<IMapper<IssuingAuthorityLookupModel, Models.IssuingAuthorityLookupModel>>();
            issuingAuthorityMapperMock
                .Setup(m => m.Map(It.IsAny<IssuingAuthorityLookupModel>()))
                .Returns(new Models.IssuingAuthorityLookupModel());

            var standardSectorCodeMapperMock = new Mock<IMapper<StandardSectorLookupModel, Models.StandardSectorLookupModel>>();
            standardSectorCodeMapperMock
                .Setup(m => m.Map(It.IsAny<StandardSectorLookupModel>()))
                .Returns(new Models.StandardSectorLookupModel());

            var sectorSubjectAreaTier1Mock = new Mock<IMapper<SectorSubjectAreaTier1LookupModel, Models.SectorSubjectAreaTier1LookupModel>>();
            sectorSubjectAreaTier1Mock
                .Setup(m => m.Map(It.IsAny<SectorSubjectAreaTier1LookupModel>()))
                .Returns(new Models.SectorSubjectAreaTier1LookupModel());

            var model = new LookUpModel
            {
                LookUpKey = "1",
                NotionalNvqLevel2Lookups = new List<NotionalNVQLevel2LookupModel>
                {
                    new NotionalNVQLevel2LookupModel()
                },
                AcademicYearLookups = new List<AcademicYearLookupModel>
                {
                    new AcademicYearLookupModel()
                },
                ValidityCategoryLookups = new List<ValidityCategoryLookupModel>
                {
                    new ValidityCategoryLookupModel()
                },
                ValidityFundingMappingLookups = new List<ValidityFundingMappingLookupModel>
                {
                    new ValidityFundingMappingLookupModel()
                },
                AwardingBodyLookups = new List<AwardingBodyLookupModel>
                {
                    new AwardingBodyLookupModel()
                },
                FrameworkTypeLookups = new List<FrameworkTypeLookupModel>
                {
                    new FrameworkTypeLookupModel()
                },
                IssuingAuthorityLookups = new List<IssuingAuthorityLookupModel>
                {
                    new IssuingAuthorityLookupModel()
                },
                StandardSectorLookups = new List<StandardSectorLookupModel>
                {
                    new StandardSectorLookupModel()
                },
                SectorSubjectAreaTier1Lookups = new List<SectorSubjectAreaTier1LookupModel>
                {
                    new SectorSubjectAreaTier1LookupModel()
                }
            };

            var mapper = new AzureLookupMapper(
                academicYearLookupMapperMock.Object,
                nvqMapperMock.Object,
                mappingMapperMock.Object,
                validityMapperMock.Object,
                awardingBodyMapperMock.Object,
                frameworkTypeMapperMock.Object,
                issuingAuthorityMapperMock.Object,
                standardSectorCodeMapperMock.Object,
                sectorSubjectAreaTier1Mock.Object);

            var result = mapper.Map(model);

            result.LookUpKey.Should().Be(model.LookUpKey);
            result.NotionalNvqLevel2Lookups.Should().HaveCount(model.NotionalNvqLevel2Lookups.Count());
            result.AcademicYearLookups.Should().HaveCount(model.AcademicYearLookups.Count());
            result.AwardingBodyLookups.Should().HaveCount(model.AwardingBodyLookups.Count());
            result.ValidityCategoryLookups.Should().HaveCount(model.ValidityCategoryLookups.Count());
            result.ValidityFundingMappingLookups.Should().HaveCount(model.ValidityFundingMappingLookups.Count());
            result.FrameworkTypeLookups.Should().HaveCount(model.FrameworkTypeLookups.Count());
            result.IssuingAuthorityLookups.Should().HaveCount(model.IssuingAuthorityLookups.Count());
            result.StandardSectorLookups.Should().HaveCount(model.StandardSectorLookups.Count());
        }
    }
}