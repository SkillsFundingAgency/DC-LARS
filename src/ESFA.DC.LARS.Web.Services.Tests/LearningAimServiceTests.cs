using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Services.Clients;
using ESFA.DC.LARS.Web.Services.Tests.Factories;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.Web.Services.Tests
{
    public class LearningAimServiceTests
    {
        [Fact]
        public async Task GetLearningAimsCallsClientServicePost()
        {
            var url = "LearningAims";
            var searchModel = SearchModelFactory.CreateLearningAimsSearchModel();

            var learningAims = new List<LearningAimModel> { LearningAimFactory.GetLearningAim() };

            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock
                .Setup(m => m.PostAsync<LearningAimsSearchModel, IEnumerable<LearningAimModel>>(url, searchModel))
                .ReturnsAsync(learningAims);

            var sut = new LearningAimsApiService(clientServiceMock.Object);
            var result = (await sut.GetLearningAims(searchModel)).ToList();

            clientServiceMock.Verify(m => m.PostAsync<LearningAimsSearchModel, IEnumerable<LearningAimModel>>(url, searchModel), Times.Once);

            result.Should().BeEquivalentTo(learningAims);
        }

        [Fact]
        public async Task GetLearningAimCallsClientServiceGet()
        {
            var url = "LearningAims";
            var learnAimRef = "6014838X";
            var parameter = new Dictionary<string, object> { { "learnAimRef", learnAimRef } };

            var learningAim = LearningAimFactory.GetLearningAim();

            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock
                .Setup(m => m.GetAsync<LearningAimModel>(url, parameter))
                .ReturnsAsync(learningAim);

            var sut = new LearningAimsApiService(clientServiceMock.Object);
            var result = await sut.GetLearningAim(learnAimRef);

            clientServiceMock.Verify(m => m.GetAsync<LearningAimModel>(url, parameter), Times.Once);

            result.Should().BeEquivalentTo(learningAim);
        }
    }
}
