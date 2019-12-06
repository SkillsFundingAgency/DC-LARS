using Autofac;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Services;

namespace ESFA.DC.LARS.Web.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<TelemetryWrapper>().As<ITelemetryWrapper>();
        }
    }
}