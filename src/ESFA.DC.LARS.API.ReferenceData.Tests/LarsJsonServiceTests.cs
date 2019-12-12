using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.ReferenceDataService.Model.LARS;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.ReferenceData.Mappers;
using ESFA.DC.Serialization.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.ReferenceData.Tests
{
    public class LarsJsonServiceTests
    {
        [Fact]
        public async Task GetLarsLearningDeliveriesFromJsonFile_Returns_Collection_Of_LarsLearningDeliveries()
        {
            var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", string.Empty));
            var fileReference = @"ReferenceData\LARSReferenceData.zip";
            var path = $@"{rootPath}\{fileReference}";

            IEnumerable<LearningAimModel> result;

            var larsLearningDeliveries = new List<LARSLearningDelivery>();

            var mapperMock = new Mock<LarsLearningAimMapper>();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                var fileServiceMock = new Mock<IFileService>();
                fileServiceMock
                    .Setup(m => m.OpenReadStreamAsync(It.IsAny<string>(), null, CancellationToken.None))
                    .ReturnsAsync(stream);

                var jsonSerializationServiceMock = new Mock<IJsonSerializationService>();
                jsonSerializationServiceMock
                    .Setup(m => m.Deserialize<List<LARSLearningDelivery>>(It.IsAny<Stream>()))
                    .Returns(larsLearningDeliveries);

                //var jsonSerializationServiceMock = new JsonSerializationService();

                var pathProviderMock = new Mock<IPathProvider>();
                pathProviderMock
                    .Setup(m => m.GetFileLocation(fileReference))
                    .Returns(path);

                var sut = new LarsJsonService(fileServiceMock.Object, jsonSerializationServiceMock.Object, mapperMock.Object, pathProviderMock.Object);
                result = await sut.GetLarsLearningDeliveriesFromJsonFile(null);
            }

            result.Should().BeEquivalentTo(larsLearningDeliveries);
        }
    }
}