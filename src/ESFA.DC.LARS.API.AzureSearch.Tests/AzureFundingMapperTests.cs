using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureFundingMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new FundingModel
            {
                LearnAimRef = "Test Ref",
                EffectiveFrom = new DateTime(2020, 1, 1),
                EffectiveTo = new DateTime(2020, 2, 1),
                FundingCategory = "test cat",
                FundingCategoryDescription = "Test cat desc",
                RateWeighted = "12.12",
                RateUnWeighted = "14.54",
                WeightingFactor = "1"
            };

            var mapper = new AzureFundingModelMapper();

            var result = mapper.Map(model);

            result.LearnAimRef.Should().Be(model.LearnAimRef);
            result.EffectiveFrom.Should().Be(model.EffectiveFrom);
            result.EffectiveTo.Should().Be(model.EffectiveTo);
            result.FundingCategory.Should().Be(model.FundingCategory);
            result.FundingCategoryDescription.Should().Be(model.FundingCategoryDescription);
            result.RateWeighted.Should().Be(12.12M);
            result.RateUnWeighted.Should().Be(14.54M);
            result.WeightingFactor.Should().Be(model.WeightingFactor);
        }
    }
}