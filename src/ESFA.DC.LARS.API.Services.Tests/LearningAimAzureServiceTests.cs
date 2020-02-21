using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class LearningAimAzureServiceTests
    {
        [Fact]
        public async Task GetLearningAims_CallsAzureSearchService_GetLarsLearningDeliveries()
        {
            var searchModel = new SearchModel();
            var learningAimModels = new List<LearningAimModel>();

            var azureServiceMock = new Mock<IAzureLearningAimsService>();
            azureServiceMock
                .Setup(m => m.GetLarsLearningDeliveries(searchModel))
                .ReturnsAsync(learningAimModels);

            var searchCleaningServiceMock = new Mock<ISearchCleaningService>();
            searchCleaningServiceMock
                .Setup(m => m.EscapeSpecialCharacters(It.IsAny<string>()))
                .Returns(string.Empty);

            var service = new LearningAimAzureService(azureServiceMock.Object, searchCleaningServiceMock.Object);
            var result = await service.GetLearningAims(searchModel);

            azureServiceMock.Verify(m => m.GetLarsLearningDeliveries(searchModel), Times.Once);
            result.Should().AllBeEquivalentTo(learningAimModels);
        }

        [Fact]
        public async Task GetLearningAim_CallsAzureSearchService_GetLarsLearningAim()
        {
            var learnAimRef = "6014838X";
            var learningAimModel = new LearningAimModel();

            var azureServiceMock = new Mock<IAzureLearningAimsService>();
            azureServiceMock
                .Setup(m => m.GetLarsLearningAim(learnAimRef))
                .ReturnsAsync(learningAimModel);

            var searchCleaningServiceMock = new Mock<ISearchCleaningService>();
            searchCleaningServiceMock
                .Setup(m => m.EscapeSpecialCharacters(It.IsAny<string>()))
                .Returns(learnAimRef);

            var service = new LearningAimAzureService(azureServiceMock.Object, searchCleaningServiceMock.Object);
            var result = await service.GetLearningAim(learnAimRef);

            azureServiceMock.Verify(m => m.GetLarsLearningAim(learnAimRef), Times.Once);
            result.Should().BeEquivalentTo(learningAimModel);
        }
    }
}
