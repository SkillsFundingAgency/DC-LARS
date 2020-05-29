using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureFrameworkMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new FrameworkModel
            {
                FrameworkCode = 1,
                ProgramType = 2,
                PathwayCode = 0,
                PathwayName = "nametest",
                FrameworkTitle = "testtitle",
                EffectiveFrom = new DateTime(2020, 1, 1),
                EffectiveTo = new DateTime(2022, 1, 1),
                SectorSubjectAreaTier2 = "-1",
                SectorSubjectAreaTier2Desc = "testdesc",
                IssuingAuthority = "1",
                IssuingAuthorityDesc = "testauthdesc"
            };

            var aimMapperMock = new Mock<IMapper<FrameworkAimModel, Models.FrameworkAimModel>>();
            var commonComponentMapperMock = new Mock<IMapper<CommonComponentModel, Models.CommonComponentModel>>();

            var mapper = new AzureFrameworkMapper(aimMapperMock.Object, commonComponentMapperMock.Object);
            var result = mapper.Map(model);

            result.FrameworkCode.Should().Be(model.FrameworkCode);
            result.PathwayCode.Should().Be(model.PathwayCode);
            result.ProgramType.Should().Be(model.ProgramType);
            result.PathwayName.Should().Be(model.PathwayName);
            result.FrameworkTitle.Should().Be(model.FrameworkTitle);
            result.EffectiveFrom.Should().Be(model.EffectiveFrom);
            result.EffectiveTo.Should().Be(model.EffectiveTo);
            result.SectorSubjectAreaTier2.Should().Be(model.SectorSubjectAreaTier2);
            result.SectorSubjectAreaTier2Desc.Should().Be(model.SectorSubjectAreaTier2Desc);
            result.IssuingAuthority.Should().Be(model.IssuingAuthority);
            result.IssuingAuthorityDesc.Should().Be(model.IssuingAuthorityDesc);
        }
    }
}