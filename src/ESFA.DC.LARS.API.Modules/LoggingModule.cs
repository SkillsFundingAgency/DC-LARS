using Autofac;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using ESFA.DC.Telemetry;
using ESFA.DC.Telemetry.Interfaces;

namespace ESFA.DC.LARS.API.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ApplicationInsightsTelemetry>().As<ITelemetry>();
            containerBuilder.RegisterType<JsonSerializationService>().As<IJsonSerializationService>();
        }
    }
}