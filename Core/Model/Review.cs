using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Review.Metadata))]
    public class Review : Entity
    {
        public string Description { get; private set; }
        public int Rating { get; private set; }

        public virtual Product Product { get; private set; }
        public virtual User User { get; private set; }


        public class Metadata : EntityMetadata
        {
            [StringLength(1000)]
            public object Description { get; set; }

            [Range(0, 5)] // Five-star rating system
            public object Rating { get; set; }
        }
    }
}