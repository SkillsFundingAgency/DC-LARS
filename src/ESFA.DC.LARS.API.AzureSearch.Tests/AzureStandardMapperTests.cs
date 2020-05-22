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
                LastDateStarts = new DateTime(2021, 1, 1)
            };

            var mapper = new AzureStandardMapper();
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
        }
    }
}