using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.Services.ODataFilters;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class FundingStreamODataFilterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ApplyFilter_Returns_Empty_String_For_No_Filter(string searchFilter)
        {
            var searchModel = new SearchModel
            {
                FundingStreams = new List<string>
                {
                    searchFilter
                }
            };
            var fundingStreamOData = string.Empty;

            var filter = new FundingStreamODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(fundingStreamOData);
        }

        [Fact]
        public void ApplyFilter_Returns_Valid_OData()
        {
            var searchModel = new SearchModel
            {
                FundingStreams = new List<string>
                {
                    "Test"
                }
            };
            var fundingStreamOData = $"(AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{searchModel.FundingStreams.Single()}')))";

            var filter = new FundingStreamODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(fundingStreamOData);
        }

        [Fact]
        public void ApplyFilter_Multiple_Terms_Returns_Valid_OData()
        {
            var searchModel = new SearchModel
            {
                FundingStreams = new List<string>
                {
                    "Test1",
                    "Test2"
                }
            };
            var fundingStreamOData = $"(AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{searchModel.FundingStreams[0]}'))" +
                                     $" or AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{searchModel.FundingStreams[1]}')))";

            var filter = new FundingStreamODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(fundingStreamOData);
        }
    }
}