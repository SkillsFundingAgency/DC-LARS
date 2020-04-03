using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class FrameworkAzureServiceTests
    {
        private Mock<IAzureFrameworkService> _azureServiceMock;
        private Mock<ISearchCleaningService> _searchCleaningServiceMock;

        public FrameworkAzureServiceTests()
        {
            _azureServiceMock = new Mock<IAzureFrameworkService>();
            _searchCleaningServiceMock = new Mock<ISearchCleaningService>();
        }

        [Fact]
        public async Task GetFramework_Returns_Valid_Framework()
        {
            var frameworkCode = 1;
            var programType = 2;
            var pathwayCode = 3;

            var apiFramework = new FrameworkModel
            {
                FrameworkCode = frameworkCode,
                ProgramType = programType,
                PathwayCode = pathwayCode
            };

            _azureServiceMock
                .Setup(m => m.GetFramework(frameworkCode, programType, pathwayCode))
                .ReturnsAsync(apiFramework);

            var service = new FrameworkService(_azureServiceMock.Object, _searchCleaningServiceMock.Object);
            var result = await service.GetFramework(frameworkCode, programType, pathwayCode);

            _azureServiceMock.Verify(m => m.GetFramework(frameworkCode, programType, pathwayCode), Times.Once);

            result.FrameworkCode.Should().Be(frameworkCode);
            result.ProgramType.Should().Be(programType);
            result.PathwayCode.Should().Be(pathwayCode);
        }

        [Fact]
        public async Task GetFrameworks_EncodesSearchTerm()
        {
            // Arrange
            var searchTerm = "test/test";
            var searchModel = new FrameworkSearchModel
            {
                SearchTerm = searchTerm
            };

            var service = new FrameworkService(_azureServiceMock.Object, _searchCleaningServiceMock.Object);

            // Act
            var result = await service.GetFrameworks(searchModel);

            // Assert
            _searchCleaningServiceMock.Verify(m => m.EscapeSearchSpecialCharacters(searchTerm), Times.Once);
        }
    }
}