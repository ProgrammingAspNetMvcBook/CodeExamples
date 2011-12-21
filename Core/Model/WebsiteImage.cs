using System;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(WebsiteImage.Metadata))]
    public class WebsiteImage
    {
        public virtual Guid Id
        {
            get { return _id ?? Guid.NewGuid(); }
            set { _id = value; }
        }
        private Guid? _id;

        public virtual string ImageUrl { get; set; }

        public virtual string ThumbnailUrl { get; set; }


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
            public object ImageUrl { get; set; }

            [StringLength(2000)]
            public object ThumbnailUrl { get; set; }
        }
    }
}