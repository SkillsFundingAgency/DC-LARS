using DCT.Automation.Core.Framework.TestBase;
using ESFA.DC.LARS.Web.Automation.Tests.Repository;
using ESFA.DC.LARS.Web.Automation.Tests.WebPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ESFA.DC.LARS.Web.Automation.Tests.StepDefinitions
{
    [Binding]
    public sealed class HomePageSteps : Driver
    {
        private DriverConfig _driverConfig;
        private PageRepository _pageRepository;
        public HomePageSteps(DriverConfig driverConfig, PageRepository pageRepository)
            : base(driverConfig)
        {
            _driverConfig = driverConfig;
            _pageRepository = pageRepository;
        }

        [Given(@"I have navigated to LRS webui")]
        public void GivenIHaveNavigatedToLRSWebui()
        {
            _pageRepository.HomePage = new HomePage(_driverConfig);
            _pageRepository.HomePage.NavigateToSite();
        }

        [When(@"I click on Search button")]
        public void WhenIClickOnSearchButton()
        {
            _pageRepository.SearchResultsPage = _pageRepository.HomePage.ClickSearchBtn<SearchResultsPage>();
        }


    }
}
