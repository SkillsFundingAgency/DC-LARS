﻿using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.Interfaces.Services;
using ESFA.DC.LARS.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.LARS.Web.Controllers
{
    public abstract class AbstractAimDetailController : Controller
    {
        protected string FormController;
        protected string CategoryController;

        private readonly ILearningAimsApiService _learningAimsApiService;
        private readonly ILookupApiService _lookupApiService;

        protected AbstractAimDetailController(
            ILearningAimsApiService learningAimsApiService,
            ILookupApiService lookupApiService)
        {
            _learningAimsApiService = learningAimsApiService;
            _lookupApiService = lookupApiService;
        }

        [Route("{learnAimRef}")]
        public async Task<IActionResult> Index(string learnAimRef, string academicYear)
        {
            var model = await GetData(learnAimRef, academicYear);

            return View(model);
        }

        public async Task<IActionResult> ChangeAcademicYear([FromForm]string learnAimRef, [FromForm]string academicYear)
        {
            var model = await GetData(learnAimRef, academicYear);

            return View("Index", model);
        }

        private async Task<LearningAimDetailsViewModel> GetData(string learnAimRef, string academicYear = null)
        {
            var lookupsTask = _lookupApiService.GetLookups();
            var learningAimTask = _learningAimsApiService.GetLearningAim(learnAimRef);

            await Task.WhenAll(lookupsTask, learningAimTask);

            var lookups = lookupsTask.Result;
            var learningAim = learningAimTask.Result;

            var selectedYear = academicYear ?? lookups.AcademicYearLookups.FirstOrDefault(l => l.IsCurrentAcademicYear)?.AcademicYear;

            return new LearningAimDetailsViewModel
            {
                AcademicYear = selectedYear,
                LookUpModel = lookups,
                LearningAimModel = learningAim,
                FormController = FormController,
                CategoryController = CategoryController
            };
        }
    }
}