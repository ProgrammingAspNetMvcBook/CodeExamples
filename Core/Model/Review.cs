using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Review.Metadata))]
    public class Review : Entity
    {
        public string Description { get; set; }
        public double Rating { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }


        public class Metadata
        {
            [StringLength(1000)]
            public object Description { get; set; }

            [Range(0, 5)] // Five-star rating system
            public object Rating { get; set; }
        }
    }
}