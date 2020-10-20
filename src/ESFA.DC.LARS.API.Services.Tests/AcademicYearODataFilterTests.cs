using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.Services.ODataFilters;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class AcademicYearODataFilterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ApplyFilter_Returns_Empty_String_For_No_Filter(string searchFilter)
        {
            var searchModel = new LearningAimsSearchModel
            {
                TeachingYears = new List<string>
                {
                    searchFilter
                }
            };
            var academicYearOData = string.Empty;

            var filter = new AcademicYearODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(academicYearOData);
        }

        [Fact]
        public void ApplyFilter_Returns_Valid_OData()
        {
            var searchModel = new LearningAimsSearchModel
            {
                TeachingYears = new List<string>
                {
                    "1920"
                }
            };
            var oData = $"(AcademicYears/any(ay:ay/AcademicYear eq '{searchModel.TeachingYears.Single()}'))";

            var filter = new AcademicYearODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(oData);
        }

        [Fact]
        public void ApplyFilter_Multiple_Terms_Returns_Valid_OData()
        {
            var searchModel = new LearningAimsSearchModel
            {
                TeachingYears = new List<string>
                {
                    "1819",
                    "1920"
                }
            };
            var oData = $"(AcademicYears/any(ay:ay/AcademicYear eq '{searchModel.TeachingYears[0]}')" +
                                    $" or AcademicYears/any(ay:ay/AcademicYear eq '{searchModel.TeachingYears[1]}'))";

            var filter = new AcademicYearODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(oData);
        }
    }
}