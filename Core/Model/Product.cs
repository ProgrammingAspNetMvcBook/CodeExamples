using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Category.Metadata))]
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual IEnumerable<Auction> Auctions { get; set; }
        public virtual IEnumerable<Category> Categories { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }


        protected override string GenerateKey()
        {
            if (string.IsNullOrWhiteSpace(Name))
                // TODO: Localize
                throw new EntityKeyGenerationException(GetType(), "Name is empty");

            return KeyGenerator.Generate(Name);
        }

        public class Metadata
        {
            [Required]
            public object Categories { get; set; }

            [Required]
            public object Description { get; set; }

            [Required, StringLength(2000)]
            public object ImageUrl { get; set; }

            [Required, StringLength(500)]
            public object Name { get; set; }
        }
    }
}