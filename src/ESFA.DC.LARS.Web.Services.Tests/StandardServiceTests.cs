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
    public class StandardServiceTests
    {
        private const string Url = "Standard";
        private const string StandardCodeParameterName = "standardCode";

        [Fact]
        public async Task GetStandard_Calls_Client_Get()
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

            var parameters = new Dictionary<string, object>
            {
                { StandardCodeParameterName, standardCode }
            };

            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock
                .Setup(m => m.GetAsync<StandardModel>(Url, parameters))
                .ReturnsAsync(standard);

            var service = new StandardApiService(clientServiceMock.Object);
            var result = await service.GetStandard(standardCode);

            clientServiceMock.Verify(m => m.GetAsync<StandardModel>(Url, parameters), Times.Once);

            result.Should().BeEquivalentTo(standard);
        }
    }
}