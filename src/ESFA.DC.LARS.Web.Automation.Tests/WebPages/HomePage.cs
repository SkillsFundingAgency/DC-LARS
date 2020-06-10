using static DCT.Automation.Core.Framework.Settings.Repository;
using DCT.Automation.Core.Framework.TestBase;
using OpenQA.Selenium;
using System;
using ESFA.DC.LARS.Web.Automation.Tests.Repository;

namespace ESFA.DC.LARS.Web.Automation.Tests.WebPages
{
    public class HomePage : Driver
    {
        private DriverConfig _driverConfig;

        public HomePage(DriverConfig driverConfig)
            : base(driverConfig)
        {
            _driverConfig = driverConfig;
        }

        IWebElement searchBtn => _driverConfig.WebDriver.FindElement(By.Id("search"));
        public void NavigateToSite()
        {
            _driverConfig.WebDriver.Navigate().GoToUrl(WebUI);
        }

        public T ClickSearchBtn<T>()
        {
            searchBtn.Click();
            return (T)Activator.CreateInstance(typeof(T), _driverConfig);
        }
    }
}
