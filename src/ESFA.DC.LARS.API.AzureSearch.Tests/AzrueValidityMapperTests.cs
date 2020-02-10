using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzrueValidityMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new ValidityModel
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 2, 1)
            };

            var mapper = new AzureValidityMapper();

            var result = mapper.Map(model);

            result.StartDate.Should().Be(model.StartDate);
            result.EndDate.Should().Be(model.EndDate);
        }
    }
}