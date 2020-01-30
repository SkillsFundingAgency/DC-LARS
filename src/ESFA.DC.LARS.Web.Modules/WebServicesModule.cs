using Autofac;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Mappers;
using ESFA.DC.LARS.Web.Services.Builders;
using ESFA.DC.LARS.Web.Services.Clients;

namespace ESFA.DC.LARS.Web.Modules
{
    public class WebServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ClientService>().As<IClientService>();
            containerBuilder.RegisterType<LearningAimsApiService>().As<ILearningAimsApiService>();
            containerBuilder.RegisterType<LookupApiService>().As<ILookupApiService>();

            containerBuilder.RegisterType<LearningAimMapper>().As<IMapper<LearningAimModel, Models.LearningAimModel>>();
            containerBuilder.RegisterType<SearchResultsBuilder>().As<ISearchResultsBuilder>();
        }
    }
}