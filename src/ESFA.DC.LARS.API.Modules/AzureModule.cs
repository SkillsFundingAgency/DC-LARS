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
                .Register(c => new UnitsClient(AzureSettings))
                .As<IUnitIndexService>()
                .SingleInstance();

            builder
                .Register(c => new LookupClient(AzureSettings))
                .As<ILookupIndexService>()
                .SingleInstance();

            builder
                .Register(c => new FrameworkClient(AzureSettings))
                .As<IFrameworkIndexService>()
                .SingleInstance();

            builder
                .Register(c => new StandardClient(AzureSettings))
                .As<IStandardIndexService>()
                .SingleInstance();

            builder.RegisterType<AzureLearningAimsService>().As<IAzureLearningAimsService>();
            builder.RegisterType<AzureUnitsService>().As<IAzureUnitsService>();
            builder.RegisterType<AzureLookupService>().As<IAzureLookupService>();
            builder.RegisterType<AzureFrameworkService>().As<IAzureFrameworkService>();
            builder.RegisterType<AzureStandardService>().As<IAzureStandardService>();

            builder.RegisterType<ODataQueryService>().As<IODataQueryService>();
            builder.RegisterType<AzureService>().As<IAzureService>();

            builder.RegisterType<SearchCleaningService>().As<ISearchCleaningService>();

            builder.RegisterType<LevelODataFilter>().As<ILearningAimsODataFilter>();
            builder.RegisterType<AwardingBodyODataFilter>().As<ILearningAimsODataFilter>();
            builder.RegisterType<AcademicYearODataFilter>().As<ILearningAimsODataFilter>();
            builder.RegisterType<FundingStreamODataFilter>().As<ILearningAimsODataFilter>();

            builder.RegisterType<FrameworkTypeODataFilter>().As<IFrameworkODataFilter>();
            builder.RegisterType<IssuingAuthorityODataFilter>().As<IFrameworkODataFilter>();

            builder.RegisterType<StandardSectorCodeODataFilter>().As<IStandardODataFilter>();
            builder.RegisterType<StandardLevelODataFilter>().As<IStandardODataFilter>();

            builder.RegisterType<AzureLearningAimsMapper>().As<IMapper<LearningAimModel, Models.LearningAimModel>>();
            builder.RegisterType<AzureLearningAimFrameworkMapper>().As<IMapper<LearningAimFrameworkModel, Models.LearningAimFrameworkModel>>();
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
            builder.RegisterType<AzureFrameworkTypeLookupMapper>().As<IMapper<FrameworkTypeLookupModel, Models.FrameworkTypeLookupModel>>();
            builder.RegisterType<AzureIssuingAuthorityLookupMapper>().As<IMapper<IssuingAuthorityLookupModel, Models.IssuingAuthorityLookupModel>>();
            builder.RegisterType<AzureStandardSectorLookupMapper>().As<IMapper<StandardSectorLookupModel, Models.StandardSectorLookupModel>>();

            builder.RegisterType<AzureFrameworkMapper>().As<IMapper<FrameworkModel, Models.FrameworkModel>>();
            builder.RegisterType<AzureFrameworkAimMapper>().As<IMapper<FrameworkAimModel, Models.FrameworkAimModel>>();

            builder.RegisterType<AzureStandardMapper>().As<IMapper<StandardModel, Models.StandardModel>>();
            builder.RegisterType<AzureCommonComponentMapper>().As<IMapper<CommonComponentModel, Models.CommonComponentModel>>();
        }
    }
}