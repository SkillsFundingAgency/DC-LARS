using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.LARS.Web.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ESFA.DC.LARS.Web.Tests
{
    public class RenderIfTagHelperTests
    {
        private readonly RenderIfAttributeTagHelper _sut;

        public RenderIfTagHelperTests()
        {
            _sut = new RenderIfAttributeTagHelper();
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Process_WillSuppressContentBasedOnRenderIfProperty(bool renderIf, bool contentShouldBeEmpty)
        {
            // Arrange
            var output = new TagHelperOutput(
                "li",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(result: null));

            output.Content.AppendHtml("<p>inner content</p>");

            _sut.RenderIf = renderIf;

            // Act
            _sut.Process(new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "uniqueId"), output);

            // Assert
            Assert.Equal(output.Content.IsEmptyOrWhiteSpace, contentShouldBeEmpty);
        }
    }
}
