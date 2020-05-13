﻿using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Models.ViewModels
{
    public class LearningAimsCategoryViewModel
    {
        public LearningAimModel LearningAimModel { get; set; }

        public List<BreadcrumbsModel> Breadcrumbs => new List<BreadcrumbsModel>()
        {
            new BreadcrumbsModel
            {
                Id = "learningAimBreadcrumbs",
                Breadcrumbs = new Dictionary<string, string>()
                {
                    { "homeLink", "Home" },
                    { "searchResultsLink", "Search Results" },
                    { "learningAimDetailLink", LearningAimModel.LearningAimTitle },
                    { "categoryLink", "Category" }
                }
            },
            new BreadcrumbsModel
            {
                Id = "frameworksBreadcrumbs",
                Breadcrumbs = new Dictionary<string, string>()
                {
                    { "homeLink", "Home" },
                    { "frameworksSearchResultsLink", "Search Results" },
                    { "pathwaysLink", "Pathways" },
                    { "learningAimDetailLink", LearningAimModel.LearningAimTitle },
                    { "categoryLink", "Category" }
                }
            },
        };
    }
}