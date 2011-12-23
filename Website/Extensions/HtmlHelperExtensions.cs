using System.Web;
using System.Web.Mvc;

namespace Ebuy.Website.Extensions
{

    public static class HtmlHelperExtensions
    {

        public static IHtmlString Image<T>(this HtmlHelper<T> helper, WebsiteImage image, string title = null)
        {
            var img = new TagBuilder("img");
            img.Attributes.Add("src", helper.RelativeUrl(image.ImageUrl));
            ApplyTitle(img, image, title);

            return new HtmlString(img.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString Thumbnail<T>(this HtmlHelper<T> helper, WebsiteImage image, string title = null)
        {
            var img = new TagBuilder("img");
            img.AddCssClass("thumbnail");
            img.Attributes.Add("data-image", image.ImageUrl);
            ApplyTitle(img, image, title);

            if(string.IsNullOrWhiteSpace(image.ThumbnailUrl))
            {
                img.Attributes.Add("src", helper.RelativeUrl(image.ImageUrl));
                img.Attributes.Add("width", "140px");
                img.Attributes.Add("height", "140px");
            }
            else
            {
                img.Attributes.Add("src", helper.RelativeUrl(image.ThumbnailUrl));
            }

            return new HtmlString(img.ToString(TagRenderMode.SelfClosing));
        }


        private static string RelativeUrl(this HtmlHelper helper, string url)
        {
            return new UrlHelper(helper.ViewContext.RequestContext).Content(url);
        }

        private static void ApplyTitle(TagBuilder img, WebsiteImage image, string title)
        {
            var alt = title ?? image.Title;

            if (!string.IsNullOrWhiteSpace(alt))
                img.Attributes.Add("alt", alt);
        }
    }

}