using System.Linq;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Services.Factories;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.Web.Services.Tests
{
    public class SearchModelFactoryTests
    {
        [Fact]
        public void GetSearchModel_Returns_Valid_Model()
        {
            var basicSearchModel = new BasicSearchModel
            {
                SearchTerm = "test term",
                Level = "test level"
            };

            var factory = new SearchModelFactory();
            var result = factory.GetSearchModel(basicSearchModel);

            result.SearchTerm.Should().Be(basicSearchModel.SearchTerm);
            result.Levels.Single().Should().Be(basicSearchModel.Level);
        }
    }
}