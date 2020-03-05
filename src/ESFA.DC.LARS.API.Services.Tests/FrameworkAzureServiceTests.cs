using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class FrameworkAzureServiceTests
    {
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

            var azureServiceMock = new Mock<IAzureFrameworkService>();
            azureServiceMock
                .Setup(m => m.GetFramework(frameworkCode, programType, pathwayCode))
                .ReturnsAsync(apiFramework);

            var service = new FrameworkService(azureServiceMock.Object);
            var result = await service.GetFramework(frameworkCode, programType, pathwayCode);

            azureServiceMock.Verify(m => m.GetFramework(frameworkCode, programType, pathwayCode), Times.Once);

            result.FrameworkCode.Should().Be(frameworkCode);
            result.ProgramType.Should().Be(programType);
            result.PathwayCode.Should().Be(pathwayCode);
        }
    }
}