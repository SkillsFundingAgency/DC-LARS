using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureLookupMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var nvqMapperMock = new Mock<IMapper<NotionalNVQLevel2Model, Models.NotionalNVQLevel2Model>>();
            nvqMapperMock
                .Setup(m => m.Map(It.IsAny<NotionalNVQLevel2Model>()))
                .Returns(new Models.NotionalNVQLevel2Model());

            var model = new LookUpModel
            {
                LookUpKey = "1",
                NotionalNvqLevel2Lookups = new List<NotionalNVQLevel2Model>
                {
                    new NotionalNVQLevel2Model()
                }
            };

            var mapper = new AzureLookupMapper(nvqMapperMock.Object);
            var result = mapper.Map(model);

            result.LookUpKey.Should().Be(model.LookUpKey);
            result.NotionalNvqLevel2Lookups.Count().Should().Be(model.NotionalNvqLevel2Lookups.Count());
        }
    }
}