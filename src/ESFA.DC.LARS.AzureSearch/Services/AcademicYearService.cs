using System.Collections.Generic;
using System.Linq;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.LARS.AzureSearch.Interfaces;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Services
{
    public class AcademicYearService : IAcademicYearService
    {
        private const int FirstYear = 1516;
        private const string YearSeperator = " to ";

        private readonly IDateTimeProvider _dateTimeProvider;

        public AcademicYearService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<LarsAcademicYearLookup> GetAcademicYears(LarsContext larsContext)
        {
            var academicYears = larsContext.LarsAcademicYearLookups.ToList();

            var nextYear = _dateTimeProvider.GetNowUtc().AddYears(1).Date;
            var nextAcademicYear = academicYears.Single(ay => ay.StartDate.Date <= nextYear && ay.EndDate.Date >= nextYear);

            return academicYears
                .Where(ay =>
                {
                    if (ay.AcademicYear == null)
                    {
                        return false;
                    }

                    if (int.TryParse(ay.AcademicYear, out var year))
                    {
                        return year >= FirstYear && ay.EndDate <= nextAcademicYear.EndDate;
                    }

                    return false;
                })
                .OrderByDescending(ay => ay.AcademicYear)
                .ToList();
        }

        public bool IsCurrentAcademicYear(LarsAcademicYearLookup year)
        {
            var currentDate = _dateTimeProvider.GetNowUtc().Date;

            return currentDate >= year.StartDate.Date && currentDate <= year.EndDate.Date;
        }

        public string FormatDescription(string description)
        {
            if (!string.IsNullOrWhiteSpace(description)
                && description.Contains("/"))
            {
                return description.Replace("/", YearSeperator);
            }

            return description;
        }
   }
}