using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ESFA.DC.LARS.Web.TagHelpers
{
    public class FilterTemplateTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetHtmlContent("<script id='filtersTemplate' type='text/x-template'><div>");
            output.PostContent.SetHtmlContent("<p><a v-on:click='clearFilters()' class='govuk-link govuk-link--no-visited-state' href='#'>Clear filters</a></p></div></script>");
        }
    }
}