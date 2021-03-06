﻿using System.Collections.Generic;
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
            var searchModel = new LearningAimsSearchModel
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
            var searchModel = new LearningAimsSearchModel
            {
                TeachingYears = new List<string>
                {
                    "1920"
                },
                FundingStreams = new List<string>
                {
                    "TEST"
                }
            };
            var fundingStreamOData =
                $"(AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{searchModel.FundingStreams.Single()}') and ay/AcademicYear eq '{searchModel.TeachingYears.Single()}'))";

            var filter = new FundingStreamODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(fundingStreamOData);
        }

        [Fact]
        public void ApplyFilter_Multiple_Terms_Returns_Valid_OData()
        {
            var searchModel = new LearningAimsSearchModel
            {
                TeachingYears = new List<string>
                {
                    "1920"
                },
                FundingStreams = new List<string>
                {
                    "TEST1",
                    "TEST2"
                }
            };
            var fundingStreamOData =
                $"(AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{searchModel.FundingStreams[0]}') and ay/AcademicYear eq '{searchModel.TeachingYears.Single()}')" +
                $" or AcademicYears/any(ay:ay/Validities/any(v:v/ValidityCategory eq '{searchModel.FundingStreams[1]}') and ay/AcademicYear eq '{searchModel.TeachingYears.Single()}'))";

            var filter = new FundingStreamODataFilter();
            var result = filter.ApplyFilter(searchModel);

            result.Should().Be(fundingStreamOData);
        }
    }
}