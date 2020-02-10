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
            result.FundingCategoryDescription.Should().Be(model.FundingCategoryDescription);
            result.RateWeighted.Should().Be(model.RateWeighted);
            result.RateUnWeighted.Should().Be(model.RateUnWeighted);
            result.WeightingFactor.Should().Be(model.WeightingFactor);
        }
    }
}