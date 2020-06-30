using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ESFA.DC.LARS.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "hide-if")]
    public class HideIfAttributeTagHelper : TagHelper
    {
        [HtmlAttributeName("hide-if")]
        public bool HideIf { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (HideIf)
            {
                output.Attributes.Add("style", "visibility:hidden");
            }
        }
    }
}
