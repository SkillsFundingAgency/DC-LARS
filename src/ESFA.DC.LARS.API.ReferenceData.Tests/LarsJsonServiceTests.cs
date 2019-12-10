using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.ReferenceDataService.Model.LARS;
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
            var fileReference = @"ReferenceData\LARSReferenceData.zip";
            IEnumerable<LearningAimModel> result;

            var larsLearningDeliveries = new List<LARSLearningDelivery>();

            var mapperMock = new Mock<LarsLearningAimMapper>();

            using (var stream = new FileStream(fileReference, FileMode.Open))
            {
                var fileServiceMock = new Mock<IFileService>();
                fileServiceMock
                    .Setup(m => m.OpenReadStreamAsync(fileReference, null, CancellationToken.None))
                    .ReturnsAsync(stream);

                var jsonSerializationServiceMock = new Mock<IJsonSerializationService>();
                jsonSerializationServiceMock
                    .Setup(m => m.Deserialize<List<LARSLearningDelivery>>(It.IsAny<Stream>()))
                    .Returns(larsLearningDeliveries);

                //var jsonSerializationServiceMock = new JsonSerializationService();

                var sut = new LarsJsonService(fileServiceMock.Object, jsonSerializationServiceMock.Object, mapperMock.Object);
                result = await sut.GetLarsLearningDeliveriesFromJsonFile();
            }

            result.Should().BeEquivalentTo(larsLearningDeliveries);
        }
    }
}