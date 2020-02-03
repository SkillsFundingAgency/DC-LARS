using System.Reflection;
using System.Runtime.Versioning;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Modules
{
    public class AppVersionService : IAppVersionService
    {
        public string Version =>
            Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        public string TargetFrameworkVersion =>
            Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
    }
}