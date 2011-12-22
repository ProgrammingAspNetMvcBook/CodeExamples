using System;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(WebsiteImage.Metadata))]
    public class WebsiteImage : Entity<Guid>
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string ThumbnailUrl { get; set; }


        public WebsiteImage()
        {
        }

        public WebsiteImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }


        public static implicit operator WebsiteImage(string imageUrl)
        {
            return new WebsiteImage(imageUrl);
        }

        public class Metadata
        {
            [StringLength(2000)]
            public object ImageUrl;

            [StringLength(2000)]
            public object ThumbnailUrl;

            [StringLength(2000)]
            public object Title;
        }
    }
}