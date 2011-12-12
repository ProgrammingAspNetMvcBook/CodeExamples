using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Category.Metadata))]
    public class Product : KeyedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; }
        
        [IsNotEmpty]
        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }


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