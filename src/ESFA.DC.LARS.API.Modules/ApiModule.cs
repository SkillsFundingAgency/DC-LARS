using Autofac;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Services;

namespace ESFA.DC.LARS.API.Modules
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LearningAimJsonService>().As<ILearningAimService>();
        }
    }
}