using System;
using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models
{
    public class SearchFilterModel
    {
        public List<string> AwardingBodies { get; set; }

        public List<string> Levels { get; set; }

        public List<string> TeachingYears { get; set; }

        public List<string> FundingStreams { get; set; }

        public DateTime? LastDataForNewStarters { get; set; }
    }
}