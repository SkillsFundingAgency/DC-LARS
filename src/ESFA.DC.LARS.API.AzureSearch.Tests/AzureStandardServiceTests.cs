using System.Threading.Tasks;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureStandardServiceTests
    {
        [Fact]
        public async Task GetStandard_Returns_Valid_Standard()
        {
            var standardCode = "547";
            var standardName = "Broadcast and Media Systems Technician";
            var StandardSectorCode = "11";

            var azureStandard = new StandardModel
            {
                StandardCode = standardCode,
                StandardName = standardName,
                StandardSectorCode = StandardSectorCode
            };
            var apiStandard = new Models.StandardModel
            {
                StandardCode = standardCode,
                StandardName = standardName,
                StandardSectorCode = StandardSectorCode
            };

            var mapperMock = new Mock<IMapper<StandardModel, Models.StandardModel>>();
            mapperMock.Setup(m => m.Map(azureStandard)).Returns(apiStandard);

            var indexServiceMock = new Mock<IStandardIndexService>();

            var azureServiceMock = new Mock<IAzureService>();
            azureServiceMock
                .Setup(m => m.GetAsync<StandardModel>(It.IsAny<IStandardIndexService>(), standardCode))
                .ReturnsAsync(azureStandard);

            var queryServiceMock = new Mock<IODataQueryService>();

            var service = new AzureStandardService(
                indexServiceMock.Object,
                mapperMock.Object,
                azureServiceMock.Object,
                queryServiceMock.Object);

            var result = await service.GetStandard(standardCode);

            azureServiceMock.Verify(m => m.GetAsync<StandardModel>(It.IsAny<IStandardIndexService>(), standardCode), Times.Once);

            result.Should().BeSameAs(apiStandard);
        }
    }
}