using Autofac;
using ESFA.DC.LARS.API.AzureSearch;
using ESFA.DC.LARS.API.AzureSearch.Clients;
using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.API.Interfaces;
using ESFA.DC.LARS.API.Interfaces.AzureSearch;
using ESFA.DC.LARS.API.Interfaces.IndexServices;
using ESFA.DC.LARS.API.Interfaces.Services;
using ESFA.DC.LARS.API.Services;
using ESFA.DC.LARS.API.Services.ODataFilters;
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

            builder
                .Register(c => new LookupClient(AzureSettings))
                .As<ILookupIndexService>()
                .SingleInstance();

            builder.RegisterType<AzureLearningAimsService>().As<IAzureLearningAimsService>();
            builder.RegisterType<AzureLookupService>().As<IAzureLookupService>();
            builder.RegisterType<ODataQueryService>().As<IODataQueryService>();
            builder.RegisterType<AzureService>().As<IAzureService>();

            builder.RegisterType<SearchCleaningService>().As<ISearchCleaningService>();

            builder.RegisterType<LevelODataFilter>().As<IODataFilter>();
            builder.RegisterType<AwardingBodyODataFilter>().As<IODataFilter>();
            builder.RegisterType<AcademicYearODataFilter>().As<IODataFilter>();
            builder.RegisterType<FundingStreamODataFilter>().As<IODataFilter>();

            builder.RegisterType<AzureLearningAimsMapper>().As<IMapper<LearningAimModel, Models.LearningAimModel>>();
            builder.RegisterType<AzureCategoryMapper>().As<IMapper<CategoryModel, Models.CategoryModel>>();
            builder.RegisterType<AzureFundingModelMapper>().As<IMapper<FundingModel, Models.FundingModel>>();
            builder.RegisterType<AzureAcademicYearMapper>().As<IMapper<AcademicYearModel, Models.AcademicYearModel>>();
            builder.RegisterType<AzureValidityMapper>().As<IMapper<ValidityModel, Models.ValidityModel>>();

            builder.RegisterType<AzureNotionalNVQLevel2ModelMapper>().As<IMapper<NotionalNVQLevel2LookupModel, Models.NotionalNVQLevel2Model>>();
            builder.RegisterType<AzureAcademicYearLookupMapper>().As<IMapper<AcademicYearLookupModel, Models.AcademicYearLookupModel>>();
            builder.RegisterType<AzureAwardingBodyLookupMapper>().As<IMapper<AwardingBodyLookupModel, Models.AwardingBodyLookupModel>>();
            builder.RegisterType<AzureLookupMapper>().As<IMapper<LookUpModel, Models.LookUpModel>>();
            builder.RegisterType<AzureValidityCategoryLookupMapper>().As<IMapper<ValidityCategoryLookupModel, Models.ValidityCategoryLookupModel>>();
            builder.RegisterType<AzureValidityFundingMappingLookupMapper>().As<IMapper<ValidityFundingMappingLookupModel, Models.ValidityFundingMappingLookupModel>>();
        }
    }
}