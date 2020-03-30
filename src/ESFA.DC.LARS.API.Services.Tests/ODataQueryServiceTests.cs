using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Microsoft.Azure.Search.Models;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.API.Services.Tests
{
    public class ODataQueryServiceTests
    {
        [Fact]
        public void SetFilters_Returns_Empty_String_For_No_Filter()
        {
            var searchModel = new LearningAimsSearchModel();

            var oDataQuery = string.Empty;

            var frameworkFilterMock = new Mock<IFrameworkODataFilter>();
            var frameworkFilters = new List<IFrameworkODataFilter> { frameworkFilterMock.Object };

            var filterMock = new Mock<ILearningAimsODataFilter>();
            filterMock
                .Setup(m => m.ApplyFilter(searchModel))
                .Returns(oDataQuery);

            var oDataLevelFilters = new List<ILearningAimsODataFilter>
            {
                filterMock.Object
            };

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService(oDataLevelFilters, frameworkFilters);

            queryService.SetLearningAimFilters(searchModel, parameters);

            parameters.Filter.Should().Be(oDataQuery);
        }

        [Fact]
        public void SetFilters_ReturnValid_OData_Query_Single_Level()
        {
            var searchModel = new LearningAimsSearchModel
            {
                Levels = new List<string>
                {
                    "1"
                }
            };

            var oDataQuery = $"(Level eq '{searchModel.Levels.Single()}')";

            var frameworkFilterMock = new Mock<IFrameworkODataFilter>();
            var frameworkFilters = new List<IFrameworkODataFilter> { frameworkFilterMock.Object };

            var filterMock = new Mock<ILearningAimsODataFilter>();
            filterMock
                .Setup(m => m.ApplyFilter(searchModel))
                .Returns(oDataQuery);

            var oDataLevelFilters = new List<ILearningAimsODataFilter>
            {
                filterMock.Object
            };

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService(oDataLevelFilters, frameworkFilters);

            queryService.SetLearningAimFilters(searchModel, parameters);

            parameters.Filter.Should().Be(oDataQuery);
        }

        [Fact]
        public void SetFilters_ReturnValid_OData_Query_Multiple_Levels()
        {
            var searchModel = new LearningAimsSearchModel
            {
                Levels = new List<string>
                {
                    "1",
                    "Entry Level",
                    "3"
                }
            };

            var oDataQuery = $"(Level eq '{searchModel.Levels[0]}' or Level eq '{searchModel.Levels[1]}' or Level eq '{searchModel.Levels[2]}')";

            var frameworkFilterMock = new Mock<IFrameworkODataFilter>();
            var frameworkFilters = new List<IFrameworkODataFilter> { frameworkFilterMock.Object };

            var filterMock = new Mock<ILearningAimsODataFilter>();
            filterMock
                .Setup(m => m.ApplyFilter(searchModel))
                .Returns(oDataQuery);

            var oDataLevelFilters = new List<ILearningAimsODataFilter>
            {
                filterMock.Object
            };

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService(oDataLevelFilters, frameworkFilters);

            queryService.SetLearningAimFilters(searchModel, parameters);

            parameters.Filter.Should().Be(oDataQuery);
        }

        [Fact]
        public void SetFilters_With_Different_Filters_Returns_Valid_OData_Query()
        {
            var searchModel = new LearningAimsSearchModel
            {
                Levels = new List<string>
                {
                    "1"
                },
                AwardingBodies = new List<string>
                {
                    "Test"
                }
            };

            var levelOData = $"(Level eq '{searchModel.Levels.Single()}')";
            var awardingBodyOData = $"(AwardingBodyCode eq '{searchModel.AwardingBodies.Single()}' or AwardingBodyName eq '{searchModel.AwardingBodies.Single()}')";
            var oDataQuery = $"{levelOData} and {awardingBodyOData}";

            var frameworkFilterMock = new Mock<IFrameworkODataFilter>();
            var frameworkFilters = new List<IFrameworkODataFilter> { frameworkFilterMock.Object };

            var levelFilterMock = new Mock<ILearningAimsODataFilter>();
            levelFilterMock
                .Setup(m => m.ApplyFilter(searchModel))
                .Returns(levelOData);

            var awardingBodyFilterMock = new Mock<ILearningAimsODataFilter>();
            awardingBodyFilterMock
                .Setup(m => m.ApplyFilter(searchModel))
                .Returns(awardingBodyOData);

            var oDataLevelFilters = new List<ILearningAimsODataFilter>
            {
                levelFilterMock.Object,
                awardingBodyFilterMock.Object
            };

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService(oDataLevelFilters, frameworkFilters);

            queryService.SetLearningAimFilters(searchModel, parameters);

            parameters.Filter.Should().Be(oDataQuery);
        }
    }
}