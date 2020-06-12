using static DCT.Automation.Framework.Settings.Repository;
using DCT.Automation.Framework.TestBase;
using OpenQA.Selenium;
using System;

namespace ESFA.DC.LARS.Web.Automation.Tests.WebPages
{
    public class HomePage : Driver
    {
        private readonly DriverConfig _driverConfig;

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
