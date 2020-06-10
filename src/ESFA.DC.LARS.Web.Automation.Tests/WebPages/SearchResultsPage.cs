using DCT.Automation.Core.Framework.TestBase;
using OpenQA.Selenium;

namespace ESFA.DC.LARS.Web.Automation.Tests.WebPages
{
    public class SearchResultsPage : Driver
    {
        private DriverConfig _driverConfig;
        public SearchResultsPage(DriverConfig driverConfig)
            : base(driverConfig)
        {
            _driverConfig = driverConfig;
        }
        IWebElement searchBtn => _driverConfig.WebDriver.FindElement(By.XPath("//h1[text()='Search qualifications']"));

        public bool IsPageLoaded()
        {
            return searchBtn.Displayed;
        }
    }
}
