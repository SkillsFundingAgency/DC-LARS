using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.LARS.Web.Models
{
    public class StandardSearchModel : AbstractSearchModel
    {
        public StandardSearchModel()
        {
            SearchType = LearningType.Standards;
        }

      public override LearningType? SearchType { get; set; }
    }
}
