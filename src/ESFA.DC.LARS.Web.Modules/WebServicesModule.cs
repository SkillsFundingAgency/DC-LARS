using Autofac;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Mappers;
using ESFA.DC.LARS.Web.Services;
using ESFA.DC.LARS.Web.Services.Clients;
using ESFA.DC.LARS.Web.Services.Factories;

namespace ESFA.DC.LARS.Web.Modules
{
    public class WebServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ClientService>().As<IClientService>();
            containerBuilder.RegisterType<LearningAimsApiService>().As<ILearningAimsApiService>();
            containerBuilder.RegisterType<UnitsApiService>().As<IUnitsApiService>();
            containerBuilder.RegisterType<LookupApiService>().As<ILookupApiService>();
            containerBuilder.RegisterType<FrameworkApiService>().As<IFrameworkApiService>();
            containerBuilder.RegisterType<StandardApiService>().As<IStandardApiService>();
            containerBuilder.RegisterType<UnitsApiService>().As<IUnitsApiService>();

            containerBuilder.RegisterType<ClientValidationService>().As<IClientValidationService>();

            containerBuilder.RegisterType<LearningAimMapper>().As<IMapper<LearningAimModel, Models.LearningAimModel>>();
            containerBuilder.RegisterType<FrameworkMapper>().As<IMapper<FrameworkModel, Models.FrameworkModel>>();
            containerBuilder.RegisterType<StandardMapper>().As<IMapper<StandardModel, Models.StandardModel>>();

            containerBuilder.RegisterType<SearchModelFactory>().As<ISearchModelFactory>();
        }
    }
}