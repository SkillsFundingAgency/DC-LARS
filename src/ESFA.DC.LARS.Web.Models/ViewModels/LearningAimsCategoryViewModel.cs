using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimsCategoryViewModel
    {
        public LearningAimModel LearningAimModel { get; set; }

        public IEnumerable<CategoryModel> CategoryModels { get; set; }
    }
}