using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureStandardFundingModelMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new StandardFundingModel
            {
                FundingCategoryDescription = "Standard Trailblazer",
                BandNumber = 9,
                EffectiveFrom = new DateTime(2020, 03, 06),
                EffectiveTo = new DateTime(2022, 1, 1),
                CoreGovContributionCap = "11",
                Incentive1618 = "1000",
                SmallBusinessIncentive = "2000",
                AchievementIncentive = "3000"
            };

            var mapper = new AzureStandardFundingModelMapper();
            var result = mapper.Map(model);

            result.FundingCategoryDescription.Should().Be(model.FundingCategoryDescription);
            result.BandNumber.Should().Be(model.BandNumber);
            result.EffectiveFrom.Should().Be(model.EffectiveFrom);
            result.EffectiveTo.Should().Be(model.EffectiveTo);
            result.CoreGovContributionCap.Should().Be(model.CoreGovContributionCap);
            result.Incentive1618.Should().Be(model.Incentive1618);
            result.SmallBusinessIncentive.Should().Be(model.SmallBusinessIncentive);
            result.AchievementIncentive.Should().Be(model.AchievementIncentive);
        }
    }
}