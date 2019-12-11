using Autofac;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.ReferenceDataService.Model.LARS;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.ReferenceData;
using ESFA.DC.LARS.API.ReferenceData.Mappers;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;

namespace ESFA.DC.LARS.API.Modules
{
    public class ReferenceDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileSystemFileService>().As<IFileService>();
            builder.RegisterType<JsonSerializationService>().As<IJsonSerializationService>();

            builder.RegisterType<PathProvider>().As<IPathProvider>();
            builder.RegisterType<LarsLearningAimMapper>().As<IMapper<LARSLearningDelivery, LearningAimModel>>();
            builder.RegisterType<LarsJsonService>().As<ILarsJsonService>().SingleInstance();
            builder.RegisterType<LarsCache>().As<ILarsCache>().SingleInstance();
        }
    }
}