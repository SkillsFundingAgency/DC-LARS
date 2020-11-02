using System;
using ESFA.DC.LARS.Web.Extensions;
using Xunit;

namespace ESFA.DC.LARS.Web.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void DateTimeExtensions_ToDateStringOrDefault_ShouldReturnDefaultWhenNull()
        {
            // Arrange
            var expected = "default";

            // Act
            var result = DateTimeExtensions.ToDateStringOrDefault(null, expected);

            // ASsert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DateTimeExtensions_ToDateStringOrDefault_ShouldReturnFormattedDateWhenNotNull()
        {
            // Arrange
            var expected = "08 October 2020";

            // Act
            var result = DateTimeExtensions.ToDateStringOrDefault(new DateTime(2020, 10, 8), "default");

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
