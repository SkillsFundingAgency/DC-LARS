using System.Collections.Generic;

namespace ESFA.DC.LARS.API.Models
{
    public class LearningAimModel
    {
        public string LearnAimRef { get; set; }

        public string LearningAimTitle { get; set; }

        public string Level { get; set; }

        public string Type { get; set; }

        public int GuidedLearningHours { get; set; }

        public string AwardingBody { get; set; }

        public string Level2Category { get; set; }

        public string Level3Category { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }

        public IEnumerable<FundingModel> FundingModels { get; set; }
    }
}