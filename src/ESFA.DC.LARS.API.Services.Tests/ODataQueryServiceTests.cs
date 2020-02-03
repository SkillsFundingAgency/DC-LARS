using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.API.Models;
using FluentAssertions;
using Microsoft.Azure.Search.Models;
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

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService();

            queryService.SetLevelFilters(searchModel, parameters);

            parameters.Filter.Should().Be($"Level eq '{searchModel.Levels.Single()}'");
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

            var parameters = new SearchParameters();

            var queryService = new ODataQueryService();

            queryService.SetLevelFilters(searchModel, parameters);

            parameters.Filter.Should().Be($"Level eq '{searchModel.Levels[0]}' or Level eq '{searchModel.Levels[1]}' or Level eq '{searchModel.Levels[2]}'");
        }
    }
}