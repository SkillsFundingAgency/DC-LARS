using System;
using Autofac;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Services.Clients;

namespace ESFA.DC.LARS.Web.Modules
{
    public class WebServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<LearningAimsApiService>().As<ILearningAimsApiService>();
        }
    }
}