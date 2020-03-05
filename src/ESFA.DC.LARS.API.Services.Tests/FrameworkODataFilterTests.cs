using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Services.ODataFilters;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class FrameworkODataFilterTests
    {
        [Fact]
        public void GetFilter_Returns_Valid_OData()
        {
            var frameworkCode = 1;
            var programType = 2;
            var pathwayCode = 3;

            var frameworkOData = $"FrameworkCode eq {frameworkCode} and ProgramType eq {programType} and PathwayCode eq {pathwayCode}";

            var searchCleaningServiceMock = new Mock<ISearchCleaningService>();
            searchCleaningServiceMock
                .Setup(m => m.EscapeFilterSpecialCharacters(frameworkOData))
                .Returns(frameworkOData);

            var filter = new FrameworkODataFilter(searchCleaningServiceMock.Object);
            var result = filter.GetFilter(frameworkCode, programType, pathwayCode);

            result.Should().Be(frameworkOData);
        }
    }
}