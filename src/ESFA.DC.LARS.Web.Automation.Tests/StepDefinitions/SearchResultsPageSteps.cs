using DCT.Automation.Standard.Framework.TestBase;
using ESFA.DC.LARS.Web.Automation.Tests.Repository;
using TechTalk.SpecFlow;
using Xunit;

namespace ESFA.DC.LARS.Web.Automation.Tests.StepDefinitions
{
    [Binding]
    public sealed class SearchResultsPageSteps : Driver
    {
        private DriverConfig _driverConfig;
        private PageRepository _pageRepository;
        public SearchResultsPageSteps(DriverConfig driverConfig, PageRepository pageRepository)
            : base(driverConfig)
        {
            _driverConfig = driverConfig;
            _pageRepository = pageRepository;
        }

        [Then(@"I should see the search results page")]
        public void ThenIShouldSeeTheSearchResultsPage()
        {
            Assert.True(_pageRepository.SearchResultsPage.IsPageLoaded());
        }

    }
}
