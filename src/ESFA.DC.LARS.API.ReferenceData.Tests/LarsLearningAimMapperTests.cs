using ESFA.DC.ILR.ReferenceDataService.Model.LARS;
using ESFA.DC.LARS.API.ReferenceData.Mappers;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.ReferenceData.Tests
{
    public class LarsLearningAimMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var larsLearningDelivery = new LARSLearningDelivery
            {
                LearnAimRef = "testRef",
                NotionalNVQLevelv2 = "testLevel",
                AwardOrgCode = "testCode",
                LearnAimRefTitle = "testTitle",
                LearnAimRefType = "testType"
            };

            var mapper = new LarsLearningAimMapper();
            var result = mapper.Map(larsLearningDelivery);

            result.LearnAimRef.Should().Be(larsLearningDelivery.LearnAimRef);
            result.Level.Should().Be(larsLearningDelivery.NotionalNVQLevelv2);
            result.AwardingBody.Should().Be(larsLearningDelivery.AwardOrgCode);
            result.LearningAimTitle.Should().Be(larsLearningDelivery.LearnAimRefTitle);
            result.Type.Should().Be(larsLearningDelivery.LearnAimRefType);
        }
    }
}