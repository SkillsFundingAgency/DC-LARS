using ESFA.DC.LARS.Web.Extensions;
using Xunit;

namespace ESFA.DC.LARS.Web.Tests
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("1000.0000", "£1000")]
        [InlineData("14500.0000", "£14500")]
        [InlineData("1250.5500", "£1251")]
        [InlineData("0.0000", "£0")]
        public void ToCurrency_Should_Format_AsExpected(string actual, string expected)
        {
            Assert.Equal(expected, StringExtensions.ToCurrency(actual));
        }

        [Theory]
        [InlineData("48.0000", "48")]
        [InlineData("45.5656", "45")]
        [InlineData("0.5500", "0")]
        public void RemovePrecision_Should_removeprecision(string actual, string expected)
        {
            Assert.Equal(expected, StringExtensions.RemovePrecision(actual));
        }
    }
}
