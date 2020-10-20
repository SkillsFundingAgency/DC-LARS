using System.Collections.Generic;

namespace ESFA.DC.LARS.API.Models
{
    public class StandardSearchModel
    {
        public string SearchTerm { get; set; }

        public List<string> Sectors { get; set; }

        public List<string> Levels { get; set; }
    }
}