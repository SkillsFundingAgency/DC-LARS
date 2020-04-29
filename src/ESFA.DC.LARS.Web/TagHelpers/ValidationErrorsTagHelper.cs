using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ESFA.DC.LARS.Web.TagHelpers
{
    public class ValidationErrorsTagHelper : TagHelper
    {
        public IEnumerable<string> Errors { get; set; }

        public string Ref { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "govuk-list");

            if (!string.IsNullOrWhiteSpace(Ref))
            {
                output.Attributes.SetAttribute("ref", Ref);
            }

            var errorsHtml = string.Empty;

            foreach (var error in Errors)
            {
                errorsHtml += $"<li class='govuk-error-message'>{error}</li>";
            }

            output.Content.SetHtmlContent(errorsHtml);
        }
    }
}
