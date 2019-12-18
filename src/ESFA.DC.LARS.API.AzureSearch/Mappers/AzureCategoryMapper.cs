using ESFA.DC.LARS.API.Interfaces.ReferenceData;
using ESFA.DC.LARS.Azure.Models;

namespace ESFA.DC.LARS.API.AzureSearch.Mappers
{
    public class AzureCategoryMapper : IMapper<CategoryModel, Models.CategoryModel>
    {
        public Models.CategoryModel Map(CategoryModel input)
        {
            return new Models.CategoryModel
            {
                Title = input.Title,
                Reference = input.Reference,
                Description = input.Description,
                ParentReference = input.ParentReference,
                ParentDescription = input.ParentDescription,
                EffectiveFrom = input.EffectiveFrom,
                EffectiveTo = input.EffectiveTo
            };
        }
    }
}