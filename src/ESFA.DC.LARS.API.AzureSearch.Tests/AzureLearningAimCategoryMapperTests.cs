using System;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureLearningAimCategoryMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var azureCategory = new CategoryModel
            {
                Reference = 3,
                Title = "TestTitle",
                Description = "TestDescription",
                EffectiveFrom = new DateTime(2020, 1, 20),
                EffectiveTo = new DateTime(2022, 1, 20),
                ParentReference = 2,
                ParentDescription = "TestParentDescription"
            };

            var mapper = new AzureCategoryMapper();
            var result = mapper.Map(azureCategory);

            result.Reference.Should().Be(azureCategory.Reference);
            result.Title.Should().Be(azureCategory.Title);
            result.Description.Should().Be(azureCategory.Description);
            result.EffectiveFrom.Should().Be(azureCategory.EffectiveFrom);
            result.EffectiveTo.Should().Be(azureCategory.EffectiveTo);
            result.ParentReference.Should().Be(azureCategory.ParentReference);
            result.ParentDescription.Should().Be(azureCategory.ParentDescription);
        }
    }
}