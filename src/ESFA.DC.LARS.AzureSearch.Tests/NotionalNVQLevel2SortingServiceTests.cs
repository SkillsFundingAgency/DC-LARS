using ESFA.DC.LARS.Azure.Models;
using ESFA.DC.LARS.AzureSearch.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ESFA.DC.LARS.AzureSearch.Tests
{
    public class NotionalNVQLevel2SortingServiceTests
    {
        private readonly NotionalNVQLevel2SortingService _sut;

        public NotionalNVQLevel2SortingServiceTests()
        {
            _sut = new NotionalNVQLevel2SortingService();
        }

        [Theory]
        [InlineData("E")]
        [InlineData("e")]
        public void Sort_ShouldStartWithEntryLevel(string level)
        {
            // Arrange
            var getLookupData = GetLookupData();
            getLookupData.Add(new NotionalNVQLevel2LookupModel { NotionalNVQLevelV2 = level });

            // Act
            var results = _sut.Sort(getLookupData);

            // Assert
            Assert.Equal("E", results.First().NotionalNVQLevelV2.ToUpper());
        }

        [Fact]
        public void Sort_ShouldPutUnknownItemsAtEndAlphabetically()
        {
            // Arrange
            var lastUnknown = "unknownC";
            var getLookupData = GetLookupData();

            getLookupData.Add(new NotionalNVQLevel2LookupModel { NotionalNVQLevelV2 = "unknownA" });
            getLookupData.Add(new NotionalNVQLevel2LookupModel { NotionalNVQLevelV2 = "unknownB" });
            getLookupData.Add(new NotionalNVQLevel2LookupModel { NotionalNVQLevelV2 = lastUnknown });

            // Act
            var results = _sut.Sort(getLookupData);

            // Assert
            Assert.Equal(lastUnknown, results.Last().NotionalNVQLevelV2);
        }

        public List<NotionalNVQLevel2LookupModel> GetLookupData()
        {
            return new List<NotionalNVQLevel2LookupModel>
            {
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="1"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="1.5"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="2"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="3"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="4"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="5"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="6"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="7"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="8"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="H"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="M"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="X"}
            };
        }
    }
}
