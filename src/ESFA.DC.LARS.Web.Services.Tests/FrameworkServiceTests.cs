using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Services.Clients;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.Web.Services.Tests
{
    public class FrameworkServiceTests
    {
        private const string Url = "Framework";
        private const string FrameworkCodeParameterName = "frameworkCode";
        private const string ProgramTypeParameterName = "programType";
        private const string PathwayCodeParameterName = "pathwayCode";

        [Fact]
        public async Task GetLearningAim_Calls_Client_Get()
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

            var parameters = new Dictionary<string, object>
            {
                { FrameworkCodeParameterName, frameworkCode },
                { ProgramTypeParameterName, programType },
                { PathwayCodeParameterName, pathwayCode }
            };

            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock
                .Setup(m => m.GetAsync<FrameworkModel>(Url, parameters))
                .ReturnsAsync(framework);

            var service = new FrameworkApiService(clientServiceMock.Object);
            var result = await service.GetFramework(frameworkCode, programType, pathwayCode);

            clientServiceMock.Verify(m => m.GetAsync<FrameworkModel>(Url, parameters), Times.Once);

            result.Should().BeEquivalentTo(framework);
        }
    }
}