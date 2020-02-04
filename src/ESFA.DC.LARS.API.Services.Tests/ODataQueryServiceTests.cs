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
        public void SetLevelFilters_ReturnValid_OData_Query_Single_Level()
        {
            var searchModel = new SearchModel
            {
                Levels = new List<string>
                {
                    "1"
                }
            };

            var oDataQuery = $"Level eq '{searchModel.Levels.Single()}'";

            var filterMock = new Mock<IODataFilter>();
            filterMock
                .Setup(m => m.ApplyFilter(searchModel))
                .Returns(oDataQuery);

            var oDataLevelFilters = new List<IODataFilter>
            {
                filterMock.Object
            };

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService(oDataLevelFilters);

            queryService.SetFilters(searchModel, parameters);

            parameters.Filter.Should().Be(oDataQuery);
        }

        [Fact]
        public void SetLevelFilters_ReturnValid_OData_Query_Multiple_Levels()
        {
            var searchModel = new SearchModel
            {
                Levels = new List<string>
                {
                    "1",
                    "Entry Level",
                    "3"
                }
            };

            var oDataQuery = $"Level eq '{searchModel.Levels[0]}' or Level eq '{searchModel.Levels[1]}' or Level eq '{searchModel.Levels[2]}'";

            var filterMock = new Mock<IODataFilter>();
            filterMock
                .Setup(m => m.ApplyFilter(searchModel))
                .Returns(oDataQuery);

            var oDataLevelFilters = new List<IODataFilter>
            {
                filterMock.Object
            };

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService(oDataLevelFilters);

            queryService.SetFilters(searchModel, parameters);

            parameters.Filter.Should().Be(oDataQuery);
        }
    }
}