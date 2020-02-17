using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureAwardingBodyLookupMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var lookup = new AwardingBodyLookupModel
            {
                AwardingBodyCode = "testOrg",
                AwardingBodyName = "testName"
            };

            var mapper = new AzureAwardingBodyLookupMapper();

            var result = mapper.Map(lookup);

            result.AwardingBodyCode.Should().Be(lookup.AwardingBodyCode);
            result.AwardingBodyName.Should().Be(lookup.AwardingBodyName);
        }
    }
}