using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class AzureSearchTermFormattingServiceTests
    {
        [Theory]
        [InlineData("information", "information")]
        [InlineData("   information    ", "information")]
        [InlineData("info*tion", "info*tion")]
        [InlineData("info*", "info*")]
        [InlineData("info* planning", "info* planning")]
        [InlineData("info* *mation", "info* /.*mation*./")]
        [InlineData("*mation", "/.*mation*./")]
        [InlineData("*mation planning", "/.*mation*./ planning")]
        [InlineData("*mation plan*", "/.*mation*./ plan*")]
        [InlineData("*mation pl*ng", "/.*mation*./ pl*ng")]
        [InlineData(" info*     *mation    ", "info* /.*mation*./")]
        [InlineData(null, "")]
        public void Test(string searchTerm, string formattedSearchTerm)
        {
            // Arrange
            var sut = new AzureSearchTermFormattingService();

            // Act
            var result = sut.FormatSearchTerm(searchTerm);

            // Assert
            Assert.Equal(formattedSearchTerm, result);
        }
    }
}
