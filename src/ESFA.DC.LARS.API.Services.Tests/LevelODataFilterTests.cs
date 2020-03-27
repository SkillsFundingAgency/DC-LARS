using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.Services.ODataFilters;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class LevelODataFilterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ApplyFilter_Returns_Empty_String_For_No_Filter(string searchFilter)
        {
            var searchModel = new LearningAimsSearchModel
            {
                Levels = new List<string>
                {
                    searchFilter
                }
            };
            var awardingBodyOData = string.Empty;

            var filter = new LevelODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(awardingBodyOData);
        }

        [Fact]
        public void ApplyFilter_Returns_Valid_OData()
        {
            var searchModel = new LearningAimsSearchModel
            {
                Levels = new List<string>
                {
                    "Test"
                }
            };
            var awardingBodyOData = $"(Level eq '{searchModel.Levels.Single()}')";

            var filter = new LevelODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(awardingBodyOData);
        }

        [Fact]
        public void ApplyFilter_Multiple_Terms_Returns_Valid_OData()
        {
            var searchModel = new LearningAimsSearchModel
            {
                Levels = new List<string>
                {
                    "Test1",
                    "Test2"
                }
            };
            var awardingBodyOData = $"(Level eq '{searchModel.Levels[0]}' or Level eq '{searchModel.Levels[1]}')";

            var filter = new LevelODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(awardingBodyOData);
        }
    }
}