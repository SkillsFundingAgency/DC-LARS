using DCT.Automation.Framework.TestBase;
using OpenQA.Selenium;

namespace ESFA.DC.LARS.Web.Automation.Tests.WebPages
{
    public class SearchResultsPage : Driver
    {
        private readonly DriverConfig _driverConfig;
        public SearchResultsPage(DriverConfig driverConfig)
            : base(driverConfig)
        {
            _driverConfig = driverConfig;
        }

        private IWebElement SearchTxt => _driverConfig.WebDriver.FindElement(By.Id("autocomplete-overlay"));

        public bool IsPageLoaded()
        {
            return SearchTxt.Displayed;
        }
    }
}
