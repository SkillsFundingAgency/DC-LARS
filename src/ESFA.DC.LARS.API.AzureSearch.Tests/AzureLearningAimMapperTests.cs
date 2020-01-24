﻿using System.Collections.Generic;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
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
            var mapper = new AzureLearningAimsMapper();

            var azureModel = new LearningAimModel
            {
                LearnAimRef = "testRef",
                LearningAimTitle = "testTitle",
                Type = "testType",
                AwardingBody = "testAwardingBody",
                Level = "testLevel",
                GuidedLearningHours = 12
            };

            var result = mapper.Map(azureModel);

            result.LearnAimRef.Should().Be(azureModel.LearnAimRef);
            result.LearningAimTitle.Should().Be(azureModel.LearningAimTitle);
            result.Type.Should().Be(azureModel.Type);
            result.AwardingBody.Should().Be(azureModel.AwardingBody);
            result.Level.Should().Be(azureModel.Level);
            result.GuidedLearningHours.Should().Be(azureModel.GuidedLearningHours);
        }
    }
}
