using System;
using System.Collections.Generic;
using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Services.Tests.Factories
{
    public class SearchModelFactory
    {
        public static SearchModel CreateSearchModel()
        {
            return new SearchModel
            {
                Level = "1/2",
                AwardingBody = "test",
                SearchTerm = "maths",
                TeachingYear = "19/20",
                SearchFilters = new SearchFilterModel
                {
                    AwardingBodies = new List<string> { "test", "another" },
                    Levels = new List<string> { "1", "2", "1/2" },
                    FundingStreams = new List<string> { "test1", "test2" },
                    TeachingYears = new List<string> { "18/19", "19/20" },
                    LastDataForNewStarters = new DateTime(2019, 11, 01)
                }
            };
        }
    }
}