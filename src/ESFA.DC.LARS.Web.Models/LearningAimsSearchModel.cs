﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ESFA.DC.LARS.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class LearningAimsSearchModel
    {
        public LearningAimsSearchModel()
        {
            AwardingBodies = new List<string>();
            Levels = new List<string>();
            TeachingYears = new List<string>();
            FundingStreams = new List<string>();
        }

        public string SearchTerm { get; set; }

        public List<string> AwardingBodies { get; set; }

        public List<string> Levels { get; set; }

        public List<string> TeachingYears { get; set; }

        public List<string> FundingStreams { get; set; }

        public DateTime? LastDataForNewStarters { get; set; }

        public override string ToString()
        {
            return $" SearchTerm : {SearchTerm} | TeachingYears : {TeachingYears} | AwardingBodies : {AwardingBodies}| Levels : {Levels}";
        }
    }
}