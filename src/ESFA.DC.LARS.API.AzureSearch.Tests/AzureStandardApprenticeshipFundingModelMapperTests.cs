using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureStandardApprenticeshipFundingModelMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new StandardApprenticeshipFundingModel()
            {
                FundingCategoryDescription = "Standard Trailblazer",
                BandNumber = 9,
                EffectiveFrom = new DateTime(2020, 03, 06),
                EffectiveTo = new DateTime(2022, 1, 1),
                ProviderAdditionalPayment1618 = "11",
                EmployerAdditionalPayment1618 = "1000",
                FrameworkUplift1618 = "2000",
                CareLeaverAdditionalPayment = "3000",
                Duration = "48",
                MaxEmployerLevyCap = "15000",
                FundableWithoutEmployer = "N",
            };

            var mapper = new AzureStandardApprenticeshipFundingModelMapper();
            var result = mapper.Map(model);

            result.FundingCategoryDescription.Should().Be(model.FundingCategoryDescription);
            result.BandNumber.Should().Be(model.BandNumber);
            result.EffectiveFrom.Should().Be(model.EffectiveFrom);
            result.EffectiveTo.Should().Be(model.EffectiveTo);
            result.ProviderAdditionalPayment1618.Should().Be(model.ProviderAdditionalPayment1618);
            result.EmployerAdditionalPayment1618.Should().Be(model.EmployerAdditionalPayment1618);
            result.FrameworkUplift1618.Should().Be(model.FrameworkUplift1618);
            result.CareLeaverAdditionalPayment.Should().Be(model.CareLeaverAdditionalPayment);
            result.Duration.Should().Be(model.Duration);
            result.MaxEmployerLevyCap.Should().Be(model.MaxEmployerLevyCap);
            result.FundableWithoutEmployer.Should().Be(model.FundableWithoutEmployer);
        }

        [Fact]
        public void decimaltest()
        {
            string stringValue = "1000.0000";
            var value = Convert.ToDecimal(stringValue);
            var s = $"£{value:F0}";
            var s5 = $"£{value:F}";
            var s1 = string.Format("F", value);
            var s2 = string.Format("D1", value);
            var s3 = string.Format("{0:#####}", value);
            var s4 = $"{value:C3}";

            var truncate = decimal.Truncate(Convert.ToDecimal(stringValue)).ToString("C");
            var truncate1 = decimal.Truncate(Convert.ToDecimal(stringValue)).ToString("C0");
            var truncate2 = Convert.ToDecimal(stringValue).ToString("C0").Replace(",", string.Empty);
            Assert.True(true);
        }
    }
}