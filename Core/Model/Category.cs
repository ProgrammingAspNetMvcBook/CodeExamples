using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Category.Metadata))]
    public class Category : Entity<long>
    {
        public string Name { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        public Category()
        {
        }

        public Category(string name)
        {
            Name = name;
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
            [Required, StringLength(100)]
            public object Name;
        }
    }
}