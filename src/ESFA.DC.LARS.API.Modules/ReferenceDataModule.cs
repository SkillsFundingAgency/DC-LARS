using Autofac;
using ESFA.DC.ILR.ReferenceDataService.Model.LARS;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.LARS.API.ReferenceData;
using ESFA.DC.LARS.API.ReferenceData.Mappers;

namespace ESFA.DC.LARS.API.Modules
{
    public class ReferenceDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LarsLearningAimMapper>().As<IMapper<LARSLearningDelivery, LearningAimModel>>();
            builder.RegisterType<LarsJsonService>().As<ILarsJsonService>().SingleInstance();
        }
    }
}