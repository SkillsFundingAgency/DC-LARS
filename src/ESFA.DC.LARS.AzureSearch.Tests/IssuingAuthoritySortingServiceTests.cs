using System.Collections.Generic;
using System.Linq;
using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Services;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.AzureSearch.Tests
{
    public class IssuingAuthoritySortingServiceTests
    {
        [Fact]
        public void Sort_Should_SortByDescription_Unknown_NotApplicable_Are_Placed_AtTheEnd()
        {
            List<IssuingAuthorityLookupModel> authorityLookupModels = new List<IssuingAuthorityLookupModel>()
            {
                new IssuingAuthorityLookupModel()
                {
                    IssuingAuthority = "-2",
                    IssuingAuthorityDesc = "Not Applicable"
                },
                new IssuingAuthorityLookupModel()
                {
                    IssuingAuthority = "-1",
                    IssuingAuthorityDesc = "Unknown"
                },
                new IssuingAuthorityLookupModel()
                {
                    IssuingAuthority = "101",
                    IssuingAuthorityDesc = "Asset Skills"
                },
                new IssuingAuthorityLookupModel()
                {
                    IssuingAuthority = "102",
                    IssuingAuthorityDesc = "Instructus"
                },
                new IssuingAuthorityLookupModel()
                {
                    IssuingAuthority = "104",
                    IssuingAuthorityDesc = "Creative and Cultural"
                },
                new IssuingAuthorityLookupModel()
                {
                    IssuingAuthority = "105",
                    IssuingAuthorityDesc = "The Tech Partnership"
                },
            };

            var sortedModels = new IssuingAuthoritySortingService().Sort(authorityLookupModels).ToList();
            sortedModels.ElementAt(0).IssuingAuthority.Should().Be("101");
            sortedModels.ElementAt(1).IssuingAuthority.Should().Be("104");
            sortedModels.ElementAt(2).IssuingAuthority.Should().Be("102");
            sortedModels.ElementAt(3).IssuingAuthority.Should().Be("105");
            sortedModels.ElementAt(4).IssuingAuthority.Should().Be("-2");
            sortedModels.ElementAt(5).IssuingAuthority.Should().Be("-1");
            sortedModels.ElementAt(4).IssuingAuthorityDesc.Should().Be("Not Applicable");
            sortedModels.ElementAt(5).IssuingAuthorityDesc.Should().Be("Unknown");
        }
    }
}
