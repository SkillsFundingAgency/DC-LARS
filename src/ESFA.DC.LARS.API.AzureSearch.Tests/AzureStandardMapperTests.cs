using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureStandardMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new StandardModel
            {
                StandardCode = "547",
                StandardName = "Broadcast and Media Systems Technician",
                StandardSectorCode = "11",
                StandardSectorCodeDesc2 = "Newspaper and Broadcast Media",
                Version = 1,
                NotionalEndLevel = "5",
                EffectiveFrom = new DateTime(2020, 03, 06),
                EffectiveTo = new DateTime(2022, 1, 1),
                LastDateStarts = new DateTime(2021, 1, 1),
                SectorSubjectAreaTier1 = "4",
                SectorSubjectAreaTier1Desc = "Engineering and Manufacturing Technolgies",
                SectorSubjectAreaTier2 = "4.10",
                SectorSubjectAreaTier2Desc = "Engineering",
                IntegratedDegreeStandard = "Y",
                OtherBodyApprovalRequired = "N"
            };

            var commonComponentMapperMock = new Mock<IMapper<CommonComponentModel, Models.CommonComponentModel>>();
            var standardFundingModelMapperMock = new Mock<IMapper<StandardFundingModel, Models.StandardFundingModel>>();
            var standardApprenticeModelMapperMock = new Mock<IMapper<StandardApprenticeshipFundingModel, Models.StandardApprenticeshipFundingModel>>();
            var aimMapperMock = new Mock<IMapper<RelatedLearningAimModel, Models.RelatedLearningAimModel>>();

            var mapper = new AzureStandardMapper(standardFundingModelMapperMock.Object, standardApprenticeModelMapperMock.Object, commonComponentMapperMock.Object, aimMapperMock.Object);
            var result = mapper.Map(model);

            result.StandardCode.Should().Be(model.StandardCode);
            result.StandardName.Should().Be(model.StandardName);
            result.StandardSectorCode.Should().Be(model.StandardSectorCode);
            result.StandardSectorCodeDesc2.Should().Be(model.StandardSectorCodeDesc2);
            result.Version.Should().Be(model.Version);
            result.EffectiveFrom.Should().Be(model.EffectiveFrom);
            result.EffectiveTo.Should().Be(model.EffectiveTo);
            result.NotionalEndLevel.Should().Be(model.NotionalEndLevel);
            result.LastDateStarts.Should().Be(model.LastDateStarts);
            result.SectorSubjectAreaTier1.Should().Be(model.SectorSubjectAreaTier1);
            result.SectorSubjectAreaTier1Desc.Should().Be(model.SectorSubjectAreaTier1Desc);
            result.SectorSubjectAreaTier2.Should().Be(model.SectorSubjectAreaTier2);
            result.SectorSubjectAreaTier2Desc.Should().Be(model.SectorSubjectAreaTier2Desc);
            result.IntegratedDegreeStandard.Should().Be(model.IntegratedDegreeStandard);
            result.OtherBodyApprovalRequired.Should().Be(model.OtherBodyApprovalRequired);
        }
    }
}