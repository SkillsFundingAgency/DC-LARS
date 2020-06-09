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

        [Fact]
        public void Sort_ShouldSortAlphanumericallyOnLevel()
        {
            // Act
            var results = _sut.Sort(GetLookupData());

            // Assert
            Assert.Equal("1.5", results.First().NotionalNVQLevelV2);
            Assert.Equal("Z", results.Last().NotionalNVQLevelV2);
        }


        [Theory]
        [InlineData("E")]
        [InlineData("e")]
        public void Sort_ShouldApplyEntryLevelToStartRule(string level)
        {
            // Arrange
            var getLookupData = GetLookupData();
            getLookupData.Add(new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2=level});

            // Act
            var results = _sut.Sort(getLookupData);

            // Assert
            Assert.Equal("E", results.First().NotionalNVQLevelV2.ToUpper());
        }

        public List<NotionalNVQLevel2LookupModel> GetLookupData()
        {
            return new List<NotionalNVQLevel2LookupModel>
            {
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="2"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="4"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="3"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="a"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="Z"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="5"},
                new NotionalNVQLevel2LookupModel{NotionalNVQLevelV2="1.5"},
            };
        }
    }
}
