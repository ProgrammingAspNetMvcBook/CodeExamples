using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Category.Metadata))]
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        protected override string GenerateKey()
        {
            if (string.IsNullOrWhiteSpace(Name))
                // TODO: Localize
                throw new EntityKeyGenerationException(GetType(), "Name is empty");

            return KeyGenerator.Generate(Name);
        }

        public class Metadata
        {
            [Required, StringLength(100)]
            public object Name { get; set; }
        }
    }
}