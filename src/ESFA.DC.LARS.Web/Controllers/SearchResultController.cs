﻿using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    [Route("SearchResult")]
    public class SearchResultController : Controller
    {
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly ILearningAimsApiService _learningAimsApiService;
        private readonly ILookupApiService _lookupApiService;
        private readonly IClientValidationService _clientValidationService;

        public SearchResultController(
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
        public async Task<IActionResult> Search([FromForm]SearchModel searchModel)
        {
            var model = new SearchResultsViewModel();

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

        [HttpGet("ClearFilters")]
        public async Task<IActionResult> ClearFilters(string searchTerm)
        {
            var model = await PopulateViewModel(null, new SearchModel { SearchTerm = searchTerm });
            return View("Index", model);
        }

        private void ValidateSearch(SearchModel searchModel, SearchResultsViewModel viewModel)
        {
            var searchTermError = _clientValidationService.SearchTermLengthValid(searchModel.SearchTerm);
            if (!string.IsNullOrEmpty(searchTermError))
            {
                viewModel.ValidationErrors.Add(searchTermError);
            }
        }

        private async Task<SearchResultsViewModel> PopulateViewModel(
            BasicSearchModel basicSearchModel = null,
            SearchModel searchModel = null)
        {
            if (searchModel == null)
            {
                searchModel = _searchModelFactory.GetSearchModel(basicSearchModel);
            }

            var learningAimsTask = _learningAimsApiService.GetLearningAims(searchModel);
            var lookupsTask = _lookupApiService.GetLookups();

            await Task.WhenAll(learningAimsTask, lookupsTask);

            var learningAims = learningAimsTask.Result;
            var lookups = lookupsTask.Result;

            return new SearchResultsViewModel
            {
                SearchModel = searchModel,
                LearningAimModels = learningAims,
                LookUpModel = lookups
            };
        }
    }
}