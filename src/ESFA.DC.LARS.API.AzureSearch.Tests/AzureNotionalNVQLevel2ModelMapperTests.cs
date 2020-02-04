using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureNotionalNVQLevel2ModelMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new NotionalNVQLevel2Model()
            {
                NotionalNVQLevelV2 = "X",
                NotionalNVQLevelV2Desc = "test desc"
            };

            var mapper = new AzureNotionalNVQLevel2ModelMapper();
            var result = mapper.Map(model);

            result.NotionalNVQLevelV2.Should().Be(model.NotionalNVQLevelV2);
            result.NotionalNVQLevelV2Desc.Should().Be(model.NotionalNVQLevelV2Desc);
        }
    }
}