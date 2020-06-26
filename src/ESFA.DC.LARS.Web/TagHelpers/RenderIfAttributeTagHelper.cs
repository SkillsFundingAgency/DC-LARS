using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ESFA.DC.LARS.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "render-if")]
    public class RenderIfAttributeTagHelper : TagHelper
    {
        [HtmlAttributeName("render-if")]
        public bool RenderIf { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (!RenderIf)
            {
                output.TagName = null;
                output.SuppressOutput();
            }
        }
    }
}
