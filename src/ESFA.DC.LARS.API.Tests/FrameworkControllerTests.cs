using System.Threading.Tasks;
using ESFA.DC.LARS.API.Controllers;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Tests
{
    public class FrameworkControllerTests
    {
        [Fact]
        public async Task GetLearningAimAsync_Calls_LearningAimService_GetLearningAim()
        {
            var frameworkCode = 1;
            var programType = 2;
            var pathwayCode = 3;

            var framework = new FrameworkModel
            {
                FrameworkCode = frameworkCode,
                ProgramType = programType,
                PathwayCode = pathwayCode
            };

            var serviceMock = new Mock<IFrameworkService>();
            serviceMock
                .Setup(m => m.GetFramework(frameworkCode, programType, pathwayCode))
                .ReturnsAsync(framework);

            var controller = new FrameworkController(serviceMock.Object);
            var result = await controller.GetFramework(frameworkCode, programType, pathwayCode);

            serviceMock.Verify(m => m.GetFramework(frameworkCode, programType, pathwayCode), Times.Once);
            result.Should().BeEquivalentTo(framework);
        }
    }
}