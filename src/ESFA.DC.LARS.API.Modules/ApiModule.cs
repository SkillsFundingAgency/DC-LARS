using Autofac;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Services;

namespace ESFA.DC.LARS.API.Modules
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LearningAimAzureService>().As<ILearningAimService>();
            builder.RegisterType<UnitService>().As<IUnitService>();
            builder.RegisterType<LookupService>().As<ILookupService>();
            builder.RegisterType<FrameworkService>().As<IFrameworkService>();
            builder.RegisterType<StandardService>().As<IStandardService>();
            builder.RegisterType<TLevelService>().As<ITLevelService>();
        }
    }
}