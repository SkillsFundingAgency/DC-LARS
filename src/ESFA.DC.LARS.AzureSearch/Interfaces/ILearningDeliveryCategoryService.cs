using System.Collections.Generic;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface ILearningDeliveryCategoryService
    {
        Dictionary<string, List<CategoryModel>> GetLearningDeliveryCategories(LarsContext context);
    }
}