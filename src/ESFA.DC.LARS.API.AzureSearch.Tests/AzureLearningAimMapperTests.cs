using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureLearningAimMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var azureCategory = new CategoryModel();
            var azureAcademicYear = new AcademicYearModel();

            var academicMapperMock = new Mock<IMapper<AcademicYearModel, Models.AcademicYearModel>>();
            academicMapperMock
                .Setup(m => m.Map(azureAcademicYear))
                .Returns(new Models.AcademicYearModel());

            var categoryMapperMock = new Mock<IMapper<CategoryModel, Models.CategoryModel>>();
            categoryMapperMock
                .Setup(m => m.Map(azureCategory))
                .Returns(new Models.CategoryModel());

            var mapper = new AzureLearningAimsMapper(categoryMapperMock.Object, academicMapperMock.Object);

            var azureModel = new LearningAimModel
            {
                LearnAimRef = "testRef",
                LearningAimTitle = "testTitle",
                Type = "testType",
                AwardingBodyName = "testAwardingBody",
                LevelDescription = "testLevel",
                GuidedLearningHours = 12
            };

            var result = mapper.Map(azureModel);

            result.LearnAimRef.Should().Be(azureModel.LearnAimRef);
            result.LearningAimTitle.Should().Be(azureModel.LearningAimTitle);
            result.Type.Should().Be(azureModel.Type);
            result.AwardingBodyCode.Should().Be(azureModel.AwardingBodyCode);
            result.AwardingBodyName.Should().Be(azureModel.AwardingBodyName);
            result.Level.Should().Be(azureModel.LevelDescription);
            result.GuidedLearningHours.Should().Be(azureModel.GuidedLearningHours);
        }
    }
}
