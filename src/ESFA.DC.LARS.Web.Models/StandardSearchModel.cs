using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class StandardSearchModel : AbstractSearchModel
    {
        public StandardSearchModel()
        {
            SearchType = LearningType.Standards;
            Levels = new List<string>();
            Sectors = new List<string>();
        }

        public override LearningType? SearchType { get; set; }

        public List<string> Levels { get; set; }

        public List<string> Sectors { get; set; }
    }
}
