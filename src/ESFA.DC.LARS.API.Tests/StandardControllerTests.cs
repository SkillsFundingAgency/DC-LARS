using System.Threading.Tasks;
using ESFA.DC.LARS.API.Controllers;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Tests
{
    public class StandardControllerTests
    {
        [Fact]
        public async Task GetStandardAsync_Calls_StandardService_GetStandard()
        {
            var standardCode = "547";
            var standardName = "Broadcast and Media Systems Technician";
            var StandardSectorCode = "11";

            var standard = new StandardModel
            {
                StandardCode = standardCode,
                StandardName = standardName,
                StandardSectorCode = StandardSectorCode
            };

            var serviceMock = new Mock<IStandardService>();
            serviceMock
                .Setup(m => m.GetStandard(standardCode))
                .ReturnsAsync(standard);

            var controller = new StandardController(serviceMock.Object);
            var result = await controller.GetStandardAsync(standardCode);

            serviceMock.Verify(m => m.GetStandard(standardCode), Times.Once);
            result.Should().BeEquivalentTo(standard);
        }
    }
}