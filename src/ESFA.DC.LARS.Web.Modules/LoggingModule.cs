using Autofac;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using ESFA.DC.Telemetry;
using ESFA.DC.Telemetry.Interfaces;

namespace ESFA.DC.LARS.Web.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationInsightsTelemetry>().As<ITelemetry>();
            builder.RegisterType<JsonSerializationService>().As<IJsonSerializationService>();
        }
    }
}