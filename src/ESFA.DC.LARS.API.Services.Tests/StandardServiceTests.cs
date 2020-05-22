using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class StandardServiceTests
    {
        private Mock<IAzureStandardService> _azureServiceMock;
        private Mock<ISearchCleaningService> _searchCleaningServiceMock;

        public StandardServiceTests()
        {
            _azureServiceMock = new Mock<IAzureStandardService>();
            _searchCleaningServiceMock = new Mock<ISearchCleaningService>();
        }

        [Fact]
        public async Task GetStandard_Returns_Valid_Standard()
        {
            var standardCode = "547";
            var standardName = "Broadcast and Media Systems Technician";
            var standardSectorCode = "11";

            var apiStandard = new Models.StandardModel
            {
                StandardCode = standardCode,
                StandardName = standardName,
                StandardSectorCode = standardSectorCode
            };

            _azureServiceMock
                .Setup(m => m.GetStandard(standardCode))
                .ReturnsAsync(apiStandard);

            var service = new StandardService(_azureServiceMock.Object, _searchCleaningServiceMock.Object);
            var result = await service.GetStandard(standardCode);

            _azureServiceMock.Verify(m => m.GetStandard(standardCode), Times.Once);

            result.StandardCode.Should().Be(standardCode);
            result.StandardName.Should().Be(standardName);
            result.StandardSectorCode.Should().Be(standardSectorCode);
        }

        [Fact]
        public async Task GetStandards_EncodesSearchTerm()
        {
            // Arrange
            var searchTerm = "test/test";
            var searchModel = new StandardSearchModel()
            {
                SearchTerm = searchTerm
            };

            var service = new StandardService(_azureServiceMock.Object, _searchCleaningServiceMock.Object);

            // Act
            var result = await service.GetStandards(searchModel);

            // Assert
            _searchCleaningServiceMock.Verify(m => m.EscapeSearchSpecialCharacters(searchTerm), Times.Once);
        }
    }
}