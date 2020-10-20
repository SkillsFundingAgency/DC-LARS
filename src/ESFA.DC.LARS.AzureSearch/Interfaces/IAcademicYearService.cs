using System.Collections.Generic;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.LARS.AzureSearch.Interfaces
{
    public interface IAcademicYearService
    {
        bool IsCurrentAcademicYear(LarsAcademicYearLookup year);

        IEnumerable<LarsAcademicYearLookup> GetAcademicYears(LarsContext larsContext);

        string FormatDescription(string description);
    }
}