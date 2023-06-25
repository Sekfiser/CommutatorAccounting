using CommutatorAccounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CommutatorAccounting.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;
        public PageCommutatorModel? PageModel { get; set; }
        public string PageAction { get; set; } = "";

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new();
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PageModel == null) throw new Exception("PageModel is not set");
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");
            tag.AddCssClass("justify-content-center");

            TagBuilder currentItem = CreateTag(PageModel.CurrentPage, urlHelper);

            if (PageModel.HasPrevious)
            {
                TagBuilder firstItem = CreateTag(1, urlHelper);
                tag.InnerHtml.AppendHtml(firstItem);
                if (PageModel.CurrentPage - 1 != 1) {
                    TagBuilder prevItem = CreateTag(PageModel.CurrentPage - 1, urlHelper);
                    tag.InnerHtml.AppendHtml(prevItem);
                }
            }

            tag.InnerHtml.AppendHtml(currentItem);
            if (PageModel.HasNext)
            {
                TagBuilder nextItem = CreateTag(PageModel.CurrentPage + 1, urlHelper);
                tag.InnerHtml.AppendHtml(nextItem);
                if (PageModel.CurrentPage + 1 != PageModel.TotalPages)
                {
                    TagBuilder lastItem = CreateTag(PageModel.TotalPages, urlHelper);
                    tag.InnerHtml.AppendHtml(lastItem);
                }
            }
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == PageModel?.CurrentPage)
            {
                item.AddCssClass("active");
            }
            else
            {
                PageUrlValues["page"] = pageNumber;
                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");

            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
