using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureRelatedLearningAimMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var model = new RelatedLearningAimModel
            {
                LearnAimRef = "testaimref",
                LearningAimTitle = "testtitle",
                Level = "testlevel",
                AwardingBodyCode = "testcode",
                EffectiveFrom = new DateTime(2020, 1, 1),
                EffectiveTo = new DateTime(2022, 1, 1),
                ComponentType = 1,
                ComponentTypeDesc = "testdesc"
            };

            var mapper = new AzureRelatedLearningAimMapper();
            var result = mapper.Map(model);

            result.LearnAimRef.Should().Be(model.LearnAimRef);
            result.LearningAimTitle.Should().Be(model.LearningAimTitle);
            result.Level.Should().Be(model.Level);
            result.AwardingBodyCode.Should().Be(model.AwardingBodyCode);
            result.EffectiveFrom.Should().Be(model.EffectiveFrom);
            result.EffectiveTo.Should().Be(model.EffectiveTo);
            result.ComponentType.Should().Be(model.ComponentType);
            result.ComponentTypeDesc.Should().Be(model.ComponentTypeDesc);
        }
    }
}