﻿using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimsSearchResultsViewModel : LearningAimsResultsViewModel
    {
        public LearningAimsSearchResultsViewModel()
            : base()
        {
            LearningAimBreadcrumbs = new BreadcrumbsModel()
            {
                Id = "learningAimBreadcrumbs",
                Breadcrumbs = new Dictionary<string, string>()
                {
                    { "homeLink", "Home" },
                    { "searchResultsLink", "Search Results" }
                },
            };
        }
    }
}