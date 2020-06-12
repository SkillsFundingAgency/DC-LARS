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

        private IWebElement SearchBtn => _driverConfig.WebDriver.FindElement(By.XPath("//h1[text()='Search qualifications']"));

        public bool IsPageLoaded()
        {
            return SearchBtn.Displayed;
        }
    }
}
