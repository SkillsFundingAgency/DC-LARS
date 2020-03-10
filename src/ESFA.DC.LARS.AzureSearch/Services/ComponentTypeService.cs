using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class ComponentTypeService : IComponentTypeService
    {
        public IDictionary<int, string> GetComponentTypes(LarsContext context)
        {
            IDictionary<int, string> componentTypes;

            componentTypes =
                context.LarsApprenticeshipComponentTypeLookups
                    .ToDictionary(
                        ct => ct.ApprenticeshipComponentType,
                        ct => ct.ApprenticeshipComponentTypeDesc);

            return componentTypes;
        }
    }
}