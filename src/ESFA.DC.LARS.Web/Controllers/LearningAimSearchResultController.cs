﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Extensions;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("LearningAimSearchResult")]
    public class LearningAimSearchResultController : Controller
    {
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly ILearningAimsApiService _learningAimsApiService;
        private readonly ILookupApiService _lookupApiService;
        private readonly IClientValidationService _clientValidationService;

        public LearningAimSearchResultController(
            ISearchModelFactory searchModelFactory,
            ILearningAimsApiService learningAimsApiService,
            ILookupApiService lookupApiService,
            IClientValidationService clientValidationService)
        {
            _searchModelFactory = searchModelFactory;
            _learningAimsApiService = learningAimsApiService;
            _lookupApiService = lookupApiService;
            _clientValidationService = clientValidationService;
        }

        public async Task<IActionResult> Index(BasicSearchModel basicSearchModel = null)
        {
            var model = await PopulateViewModel(basicSearchModel);

            return View(model);
        }

        [HttpGet("RedirectToDetails")]
        public IActionResult RedirectToDetails(string learnAimRef, string academicYear)
        {
            return RedirectToAction("Index", "LearningAimDetail", new { learnAimRef, academicYear });
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromForm]LearningAimsSearchModel searchModel)
        {
            var model = new LearningAimsSearchResultsViewModel();

            ValidateSearch(searchModel, model);
            if (model.ValidationErrors.Any())
            {
                model.SearchModel = searchModel;
                model.LookUpModel = await _lookupApiService.GetLookups();
                return View("Index", model);
            }

            model = await PopulateViewModel(null, searchModel);

            return View("Index", model);
        }

        [HttpGet("Results")]
        public async Task<IActionResult> Results([FromQuery]LearningAimsSearchModel searchModel)
        {
            var resultsModel = new LearningAimsSearchResultsViewModel
            {
                SearchModel = searchModel,
                LearningAimModels = new List<LearningAimModel>()
            };

            ValidateSearch(searchModel, resultsModel);

            if (!resultsModel.ValidationErrors.Any())
            {
                resultsModel.LearningAimModels = await _learningAimsApiService.GetLearningAims(searchModel);
            }

            var partialViewHtml = await this.RenderViewAsync("_LearningAimsResults", resultsModel, true);

            return Json(new { data= partialViewHtml, count= resultsModel.LearningAimModels.Count(), validationErrors= resultsModel.ValidationErrors });
        }

        [HttpGet("ClearFilters")]
        public async Task<IActionResult> ClearFilters(string searchTerm, string academicYear)
        {
            var model = await PopulateViewModel(null, new LearningAimsSearchModel { SearchTerm = searchTerm, TeachingYears = new List<string> { academicYear } });
            return View("Index", model);
        }

        private void ValidateSearch(LearningAimsSearchModel searchModel, LearningAimsSearchResultsViewModel viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }
        }

        private async Task<LearningAimsSearchResultsViewModel> PopulateViewModel(
            BasicSearchModel basicSearchModel = null,
            LearningAimsSearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = _searchModelFactory.GetLearningAimsSearchModel(basicSearchModel);
            }

            var learningAimsTask = _learningAimsApiService.GetLearningAims(searchModel);
            var lookupsTask = _lookupApiService.GetLookups();

            await Task.WhenAll(learningAimsTask, lookupsTask);

            var learningAims = learningAimsTask.Result;
            var lookups = lookupsTask.Result;

            return new LearningAimsSearchResultsViewModel
            {
                SearchModel = searchModel,
                LearningAimModels = learningAims,
                LookUpModel = lookups
            };
        }
    }
}