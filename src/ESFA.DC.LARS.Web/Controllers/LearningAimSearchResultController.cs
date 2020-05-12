﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("LearningAimSearchResult")]
    public class LearningAimSearchResultController : BaseResultsController<LearningAimsSearchModel, LearningAimModel>
    {
        private const string ResultsTemplate = "_LearningAimsResults";

        private readonly ISearchModelFactory _searchModelFactory;
        private readonly ILearningAimsApiService _learningAimsApiService;
        private readonly IClientValidationService _clientValidationService;

        public LearningAimSearchResultController(
            ISearchModelFactory searchModelFactory,
            ILearningAimsApiService learningAimsApiService,
            ILookupApiService lookupApiService,
            IClientValidationService clientValidationService)
            : base(lookupApiService, ResultsTemplate, LearningType.Qualifications)
        {
            _searchModelFactory = searchModelFactory;
            _learningAimsApiService = learningAimsApiService;
            _clientValidationService = clientValidationService;
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(string learnAimRef, string academicYear)
        {
            return RedirectToAction("Index", "LearningAimDetail", new { learnAimRef, academicYear });
        }

        [HttpGet("ClearFilters")]
        public async Task<IActionResult> ClearFilters(string searchTerm, string academicYear)
        {
            var model = await PopulateViewModel(null, new LearningAimsSearchModel { SearchTerm = searchTerm, TeachingYears = new List<string> { academicYear } });
            return View("Index", model);
        }

        protected override LearningAimsSearchModel GetSearchModel(BasicSearchModel basicSearchModel)
        {
            var searchModel = _searchModelFactory.GetLearningAimsSearchModel(basicSearchModel);
            searchModel.SearchType = LearningType.Qualifications;
            return searchModel;
        }

        protected override Task<IEnumerable<LearningAimModel>> GetSearchResults(LearningAimsSearchModel searchModel)
        {
            return _learningAimsApiService.GetLearningAims(searchModel);
        }

        protected override void ValidateSearch(LearningAimsSearchModel searchModel, SearchResultsViewModel<LearningAimsSearchModel, LearningAimModel> viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }
        }
    }
}