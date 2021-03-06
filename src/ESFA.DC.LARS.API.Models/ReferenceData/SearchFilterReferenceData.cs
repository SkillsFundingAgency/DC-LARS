﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.API.Models.ReferenceData
{
    [ExcludeFromCodeCoverage]
    public class SearchFilterReferenceData
    {
        public List<string> AwardingBodies { get; set; }

        public List<string> Levels { get; set; }

        public List<string> TeachingYears { get; set; }

        public List<string> FundingStreams { get; set; }
    }
}