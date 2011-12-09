using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(User.Metadata))]
    public class User : Entity
    {
        public string DisplayName { get; private set; }
        public string EmailAddress { get; private set; }
        public string FullName { get; private set; }

        public virtual IEnumerable<Bid> Bids { get; private set; }
        public virtual IEnumerable<Payment> Payments { get; private set; }
        public virtual IEnumerable<Review> Reviews { get; private set; }
        public virtual IEnumerable<Auction> WatchedAuctions { get; private set; }

        protected override string GenerateKey()
        {
            if(string.IsNullOrWhiteSpace(DisplayName))
                // TODO: Localize
                throw new EntityKeyGenerationException(GetType(), "DisplayName is empty");

            return KeyGenerator.Generate(DisplayName);
        }

        public class Metadata : EntityMetadata
        {
            [Required, StringLength(50, MinimumLength = 3)]
            public object DisplayName { get; set; }

            [Required, StringLength(100, MinimumLength = 5)]
            public object EmailAddress { get; set; }

            [Required, StringLength(100, MinimumLength = 3)]
            public object FullName { get; set; }
        }
    }
}