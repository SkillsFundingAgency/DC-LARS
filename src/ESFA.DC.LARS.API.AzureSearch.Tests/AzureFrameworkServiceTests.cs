using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.Telemetry.Interfaces;
using FluentAssertions;
using Microsoft.Azure.Search.Models;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureFrameworkServiceTests
    {
        [Fact]
        public async Task GetFramework_Returns_Valid_Framework()
        {
            var frameworkCode = 1;
            var programType = 2;
            var pathwayCode = 3;

            var azureFramework = new FrameworkModel
            {
                FrameworkCode = frameworkCode,
                ProgramType = programType,
                PathwayCode = pathwayCode
            };
            var apiFramework = new Models.FrameworkModel
            {
                FrameworkCode = frameworkCode,
                ProgramType = programType,
                PathwayCode = pathwayCode
            };

            var searchResult = new DocumentSearchResult<FrameworkModel>(
                new List<SearchResult<FrameworkModel>>
                {
                    new SearchResult<FrameworkModel>(azureFramework)
                },
                1,
                0,
                null,
                SearchContinuationToken.CreateTestToken("foo"));

            var mapperMock = new Mock<IMapper<FrameworkModel, Models.FrameworkModel>>();
            mapperMock.Setup(m => m.Map(azureFramework)).Returns(apiFramework);

            var telemetryMock = new Mock<ITelemetry>();

            var indexServiceMock = new Mock<IFrameworkIndexService>();

            var searchCleaningServiceMock = new Mock<ISearchCleaningService>();
            searchCleaningServiceMock
                .Setup(m => m.EscapeSearchSpecialCharacters(It.IsAny<string>()))
                .Returns(string.Empty);

            var azureServiceMock = new Mock<IAzureService>();
            azureServiceMock
                .Setup(m => m.SearchIndexAsync<FrameworkModel>(indexServiceMock.Object, string.Empty, It.IsAny<SearchParameters>()))
                .ReturnsAsync(searchResult);

            var service = new AzureFrameworkService(
                telemetryMock.Object,
                indexServiceMock.Object,
                mapperMock.Object,
                azureServiceMock.Object,
                searchCleaningServiceMock.Object);

            var result = await service.GetFramework(frameworkCode, programType, pathwayCode);

            azureServiceMock.Verify(m => m.SearchIndexAsync<FrameworkModel>(indexServiceMock.Object, string.Empty, It.IsAny<SearchParameters>()), Times.Once);

            result.Should().BeSameAs(apiFramework);
        }
    }
}