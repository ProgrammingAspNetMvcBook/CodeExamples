using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(User.Metadata))]
    public class User : Entity
    {
        public string DisplayName
        {
            get { return _displayName ?? FullName; }
            set { _displayName = value; }
        }
        private string _displayName;

        [Unique]
        public string EmailAddress { get; set; }

        public string FullName { get; set; }

        public virtual IEnumerable<Bid> Bids { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }
        public virtual IEnumerable<Auction> WatchedAuctions { get; set; }

        protected override string GenerateKey()
        {
            if(string.IsNullOrWhiteSpace(DisplayName))
                // TODO: Localize
                throw new EntityKeyGenerationException(GetType(), "DisplayName is empty");

            return KeyGenerator.Generate(DisplayName);
        }

        public class Metadata
        {
            [StringLength(50)]
            public object DisplayName { get; set; }

            [StringLength(100, MinimumLength = 5)]
            public object EmailAddress { get; set; }

            [Required, StringLength(100, MinimumLength = 3)]
            public object FullName { get; set; }
        }
    }
}