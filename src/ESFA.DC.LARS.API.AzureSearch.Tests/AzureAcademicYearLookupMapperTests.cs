using ESFA.DC.LARS.API.AzureSearch.Mappers;
using ESFA.DC.LARS.Azure.Models;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureAcademicYearLookupMapperTests
    {
        [Fact]
        public void Map_Returns_Valid_Model()
        {
            var lookup = new AcademicYearLookupModel
            {
                AcademicYear = "testYear",
                AcademicYearDesc = "testDesc",
                IsCurrentAcademicYear = true
            };

            var mapper = new AzureAcademicYearLookupMapper();

            var result = mapper.Map(lookup);

            result.IsCurrentAcademicYear.Should().Be(lookup.IsCurrentAcademicYear);
            result.AcademicYear.Should().Be(lookup.AcademicYear);
            result.AcademicYearDesc.Should().Be(lookup.AcademicYearDesc);
        }
    }
}