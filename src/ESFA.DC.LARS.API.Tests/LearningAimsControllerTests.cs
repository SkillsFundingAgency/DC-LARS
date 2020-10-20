using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.API.Controllers;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Tests
{
    public class LearningAimsControllerTests
    {
        [Fact]
        public async Task GetLearningAimsAsync_Calls_LearningAimService_GetLearningAims()
        {
            var searchModel = new LearningAimsSearchModel();
            var learningAimModels = new List<LearningAimModel>();

            var serviceMock = new Mock<ILearningAimService>();
            serviceMock
                .Setup(m => m.GetLearningAims(searchModel))
                .ReturnsAsync(learningAimModels);

            var controller = new LearningAimsController(serviceMock.Object);
            var result = await controller.GetLearningAimsAsync(searchModel);

            serviceMock.Verify(m => m.GetLearningAims(searchModel), Times.Once);
            result.Should().AllBeEquivalentTo(learningAimModels);
        }

        [Fact]
        public async Task GetLearningAimAsync_Calls_LearningAimService_GetLearningAim()
        {
            var learnAimRef = "6014838X";
            var learningAimModel = new LearningAimModel();

            var serviceMock = new Mock<ILearningAimService>();
            serviceMock
                .Setup(m => m.GetLearningAim(learnAimRef))
                .ReturnsAsync(learningAimModel);

            var controller = new LearningAimsController(serviceMock.Object);
            var result = await controller.GetLearningAimAsync(learnAimRef);

            serviceMock.Verify(m => m.GetLearningAim(learnAimRef), Times.Once);
            result.Should().BeEquivalentTo(learningAimModel);
        }
    }
}
