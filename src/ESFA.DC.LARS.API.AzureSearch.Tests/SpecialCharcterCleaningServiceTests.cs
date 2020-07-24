using FluentAssertions;
using Xunit;

namespace ESFA.DC.LARS.API.AzureSearch.Tests
{
    public class SpecialCharcterCleaningServiceTests
    {
        [Theory]
        [InlineData('+')]
        [InlineData('-')]
        [InlineData('&')]
        [InlineData('|')]
        [InlineData('!')]
        [InlineData('(')]
        [InlineData(')')]
        [InlineData('{')]
        [InlineData('}')]
        [InlineData('[')]
        [InlineData(']')]
        [InlineData('^')]
        [InlineData('"')]
        [InlineData('~')]
        [InlineData('?')]
        [InlineData(':')]
        [InlineData(';')]
        [InlineData('/')]
        [InlineData('`')]
        [InlineData('<')]
        [InlineData('>')]
        [InlineData('#')]
        [InlineData('%')]
        [InlineData('@')]
        [InlineData('=')]
        [InlineData('\\')]
        public void EscapeSpecialCharacters_Returns_Sanitised_String(char replaceChar)
        {
            var escapeChar = @"\";
            var service = new SearchCleaningService();

            var result = service.EscapeSearchSpecialCharacters(replaceChar.ToString());

            result.Should().Be(escapeChar + replaceChar);
        }

        [Fact]
        public void EscapeSpecialCharacters_Ignores_Normal_Chars()
        {
            const string normalChars = "1234567890qwertyuiopasdfghjklzxcvbnm";

            var service = new SearchCleaningService();

            var result = service.EscapeSearchSpecialCharacters(normalChars);

            result.Should().Be(normalChars);
        }
    }
}