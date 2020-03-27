using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.Telemetry.Interfaces;
using FluentAssertions;
using Microsoft.Azure.Search.Models;
using Moq;
using Xunit;
using LearningAimModel = ESFA.DC.LARS.Azure.Models.LearningAimModel;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureLearningAimsServiceTests
    {
        [Fact]
        public async Task GetLarsLearningDeliveries_Returns_Valid_Aims()
        {
            var learnAimRef = "12345678";
            var azureLearningAim = new LearningAimModel
            {
                LearnAimRef = learnAimRef
            };
            var apiLearningAim = new Models.LearningAimModel
            {
                LearnAimRef = learnAimRef
            };

            var searchModel = new LearningAimsSearchModel
            {
                SearchTerm = learnAimRef
            };

            var searchResult = new DocumentSearchResult<LearningAimModel>(
                new List<SearchResult<LearningAimModel>>
                {
                    new SearchResult<LearningAimModel>(azureLearningAim)
                },
                1,
                0,
                null,
                SearchContinuationToken.CreateTestToken("foo"));

            var telemetryMock = new Mock<ITelemetry>();

            var mapperMock = new Mock<IMapper<LearningAimModel, Models.LearningAimModel>>();
            mapperMock.Setup(m => m.Map(azureLearningAim)).Returns(apiLearningAim);

            var indexServiceMock = new Mock<ILearningDeliveryIndexService>();

            var azureServiceMock = new Mock<IAzureService>();
            azureServiceMock
                .Setup(m => m.SearchIndexAsync<LearningAimModel>(indexServiceMock.Object, learnAimRef, It.IsAny<SearchParameters>()))
                .ReturnsAsync(searchResult);

            var queryServiceMock = new Mock<IODataQueryService>();

            var service = new AzureLearningAimsService(
                telemetryMock.Object,
                mapperMock.Object,
                indexServiceMock.Object,
                queryServiceMock.Object,
                azureServiceMock.Object);

            var result = await service.GetLarsLearningDeliveries(searchModel);

            azureServiceMock.Verify(m => m.SearchIndexAsync<LearningAimModel>(indexServiceMock.Object, learnAimRef, It.IsAny<SearchParameters>()), Times.Once);

            result.Should().HaveCount(1);
            result.Single().Should().BeSameAs(apiLearningAim);
        }

        [Fact]
        public async Task GetLarsLearningAim_Returns_Valid_Aim()
        {
            var learnAimRef = "12345678";

            var azureLearningAim = new LearningAimModel();
            var apiLearningAim = new Models.LearningAimModel();

            var telemetryMock = new Mock<ITelemetry>();

            var mapperMock = new Mock<IMapper<LearningAimModel, Models.LearningAimModel>>();
            mapperMock.Setup(m => m.Map(azureLearningAim)).Returns(apiLearningAim);

            var indexServiceMock = new Mock<ILearningDeliveryIndexService>();

            var azureServiceMock = new Mock<IAzureService>();
            azureServiceMock
                .Setup(m => m.GetAsync<LearningAimModel>(indexServiceMock.Object, learnAimRef))
                .ReturnsAsync(azureLearningAim);

            var queryServiceMock = new Mock<IODataQueryService>();

            var service = new AzureLearningAimsService(
                telemetryMock.Object,
                mapperMock.Object,
                indexServiceMock.Object,
                queryServiceMock.Object,
                azureServiceMock.Object);

            var result = await service.GetLarsLearningAim(learnAimRef);

            mapperMock.Verify(m => m.Map(azureLearningAim), Times.Once);
            azureServiceMock.Verify(m => m.GetAsync<LearningAimModel>(indexServiceMock.Object, learnAimRef), Times.Once);

            result.Should().BeSameAs(apiLearningAim);
        }
    }
}