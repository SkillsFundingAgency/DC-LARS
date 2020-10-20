using DCT.Automation.Framework.TestBase;
using TechTalk.SpecFlow;

namespace ESFA.DC.LARS.Web.Automation.Tests.Hooks
{
    [Binding]
    public sealed class InitializeHooks : Driver
    {
        private readonly DriverConfig _driverConfig;
        private readonly string _testConfigJsonFile = "appSettings.json";
        public InitializeHooks(DriverConfig driverConfig)
            : base(driverConfig)
        {
            _driverConfig = driverConfig;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            InitSettings(_testConfigJsonFile);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driverConfig.WebDriver.Close();
        }
    }
}
