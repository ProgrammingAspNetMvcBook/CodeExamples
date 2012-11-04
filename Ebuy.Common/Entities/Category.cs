using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebuy
{
    [MetadataType(typeof(Category.Metadata))]
    public class Category : Entity<long>
    {
        public virtual ICollection<Auction> Auctions { get; set; }

        public bool IsTopLevelCategory
        {
            get { return ParentId == null; }
        }

        public string Name { get; set; }

        public long? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }


        public Category()
        {
            Auctions = new Collection<Auction>();
            SubCategories = new Collection<Category>();
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

            [ForeignKey("Parent")]
            public object ParentId;
        }
    }
}