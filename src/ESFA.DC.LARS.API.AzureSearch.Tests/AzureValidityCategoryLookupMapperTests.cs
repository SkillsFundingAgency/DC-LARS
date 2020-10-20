using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureValidityCategoryLookupMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new ValidityCategoryLookupModel
            {
                ValidityCategory = "test cat",
                ValidityCategoryDescription = "Test cat desc"
            };

            var mapper = new AzureValidityCategoryLookupMapper();

            var result = mapper.Map(model);

            result.ValidityCategory.Should().Be(model.ValidityCategory);
            result.ValidityCategoryDescription.Should().Be(model.ValidityCategoryDescription);
        }
    }
}