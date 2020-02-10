using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.Services.ODataFilters;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class AwardingBodyODataFilterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ApplyFilter_Returns_Empty_String_For_No_Filter(string searchFilter)
        {
            var searchModel = new SearchModel
            {
                AwardingBodies = new List<string>
                {
                    searchFilter
                }
            };
            var awardingBodyOData = string.Empty;

            var filter = new AwardingBodyODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(awardingBodyOData);
        }

        [Fact]
        public void ApplyFilter_Returns_Valid_OData()
        {
            var searchModel = new SearchModel
            {
                AwardingBodies = new List<string>
                {
                    "Test"
                }
            };
            var awardingBodyOData = $"(AwardingBodyCode eq '{searchModel.AwardingBodies.Single()}' or AwardingBodyName eq '{searchModel.AwardingBodies.Single()}')";

            var filter = new AwardingBodyODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(awardingBodyOData);
        }

        [Fact]
        public void ApplyFilter_Multiple_Terms_Returns_Valid_OData()
        {
            var searchModel = new SearchModel
            {
                AwardingBodies = new List<string>
                {
                    "Test1",
                    "Test2"
                }
            };
            var awardingBodyOData = $"(AwardingBodyCode eq '{searchModel.AwardingBodies[0]}' or AwardingBodyName eq '{searchModel.AwardingBodies[0]}'" +
                                    $" or AwardingBodyCode eq '{searchModel.AwardingBodies[1]}' or AwardingBodyName eq '{searchModel.AwardingBodies[1]}')";

            var filter = new AwardingBodyODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(awardingBodyOData);
        }
    }
}