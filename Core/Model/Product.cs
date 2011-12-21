using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Category.Metadata))]
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public double Rating
        {
            get { return _rating.Value; }
        }
        private readonly Lazy<double> _rating;

        public virtual ICollection<Auction> Auctions { get; set; }
        
        [IsNotEmpty]
        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<WebsiteImage> Images { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public Product()
        {
            _rating = new Lazy<double>(() => Reviews.Average(x => x.Rating));
        }

        protected override string GenerateKey()
        {
            if (string.IsNullOrWhiteSpace(Name))
                // TODO: Localize
                throw new EntityKeyGenerationException(GetType(), "Name is empty");

            return KeyGenerator.Generate(Name);
        }

        public class Metadata
        {
            [Required, IsNotEmpty]
            public object Categories;

            [Required]
            public object Description;

            [Required, StringLength(2000)]
            public object ImageUrl;

            [Required, StringLength(500)]
            public object Name;
        }
    }
}