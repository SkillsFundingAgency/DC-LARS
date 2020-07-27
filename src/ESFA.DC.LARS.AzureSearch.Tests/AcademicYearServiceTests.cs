using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.LARS.AzureSearch.Services;
using Moq;
using Xunit;

namespace ESFA.DC.LARS.AzureSearch.Tests
{
    public class AcademicYearServiceTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
        private readonly AcademicYearService _sut;

        public AcademicYearServiceTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _sut = new AcademicYearService(_mockDateTimeProvider.Object);
        }

        [Theory]
        [InlineData("2019/2020", "2019 to 2020")]
        [InlineData("19/20", "19 to 20")]
        [InlineData("19-20", "19-20")]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void FormatDescription_ShouldFormatDatesWithSlashes(string description, string formatedDescription) 
        {
            // Act
            var result = _sut.FormatDescription(description);

            //Assert
            Assert.Equal(formatedDescription, result);
        }
    }
}
