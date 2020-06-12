using System.ComponentModel.DataAnnotations;
using System.Linq;
using ESFA.DC.LARS.Web.Extensions;
using Xunit;

namespace ESFA.DC.LARS.Web.Tests
{
    public class HtmlExtensionsTests
    {
        private const string HasDisplayAttributeText = "HasDisplayAttributeText";

        private enum TestEnum
        {
            [Display(Name = HasDisplayAttributeText)]
            HasDiplayAttribute,
            NoDisplayAttribute
        }

        [Fact]
        public void GetEnumValueSelectList_ShouldUseDisplayAttributeForTextWhenPresent()
        {
            // Act
            var result = HtmlExtensions.GetEnumValueSelectList<TestEnum>(null);

            // Assert
            Assert.Equal(result.Single(a => a.Value == TestEnum.HasDiplayAttribute.ToString()).Text, HasDisplayAttributeText);
        }

        [Fact]
        public void GetEnumValueSelectList_ShouldDefaultToEnumToStringForText()
        {
            // Act
            var result = HtmlExtensions.GetEnumValueSelectList<TestEnum>(null);

            // Assert
            Assert.Equal(result.Single(a => a.Value == TestEnum.NoDisplayAttribute.ToString()).Text, TestEnum.NoDisplayAttribute.ToString());
        }
    }
}
