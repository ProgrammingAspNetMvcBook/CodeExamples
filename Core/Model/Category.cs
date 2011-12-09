using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Category.Metadata))]
    public class Category : Entity
    {
        public string Name { get; private set; }

        public virtual IEnumerable<Product> Products { get; private set; }


        protected override string GenerateKey()
        {
            if (string.IsNullOrWhiteSpace(Name))
                // TODO: Localize
                throw new EntityKeyGenerationException(GetType(), "Name is empty");

            return KeyGenerator.Generate(Name);
        }

        public class Metadata : EntityMetadata
        {
            [Required, StringLength(100)]
            public object Name { get; set; }
        }
    }
}