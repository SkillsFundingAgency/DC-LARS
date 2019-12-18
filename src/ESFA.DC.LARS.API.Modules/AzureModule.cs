using Autofac;
using ESFA.DC.LARS.API.AzureSearch;
using ESFA.DC.LARS.API.AzureSearch.Clients;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.Modules
{
    public class AzureModule : Module
    {
        public IAzureSettings AzureSettings { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => new LearningAimsClient(AzureSettings))
                .As<ILearningDeliveryIndexService>()
                .SingleInstance();

            builder.RegisterType<AzureSearchService>().As<IAzureSearchService>();
            builder.RegisterType<AzureLearningAimsMapper>().As<IMapper<LearningAimModel, Models.LearningAimModel>>();
        }
    }
}