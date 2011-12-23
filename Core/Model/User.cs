using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(User.Metadata))]
    public class User : Entity<long>
    {
        public virtual ICollection<Auction> Selling { get; private set; }

//        public virtual ICollection<Bid> Bids { get; private set; }

        public virtual string DisplayName
        {
            get { return _displayName ?? FullName; }
            set { _displayName = value; }
        }
        private string _displayName;

        [Unique]
        public string EmailAddress { get; set; }

        public virtual string FullName { get; set; }

        public virtual ICollection<Payment> Payments { get; private set; }

        public virtual ICollection<Review> Reviews { get; private set; }

        public virtual ICollection<Auction> WatchedAuctions { get; private set; }


        public User()
        {
//            Bids = new Collection<Bid>();
            Payments = new Collection<Payment>();
            Reviews = new Collection<Review>();
            WatchedAuctions = new Collection<Auction>();
        }


        protected override string GenerateKey()
        {
            if(string.IsNullOrWhiteSpace(DisplayName))
                // TODO: Localize
                throw new EntityKeyGenerationException(GetType(), "DisplayName is empty");

            return KeyGenerator.Generate(DisplayName);
        }


        public void Bid(Auction auction, Currency bidAmount)
        {
            Contract.Requires(auction != null);
            Contract.Requires(bidAmount != null);

            auction.PostBid(this, bidAmount);
        }


        public class Metadata
        {
/*
            [InverseProperty("User")]
            public object Bids;
*/

            [StringLength(50)]
            public object DisplayName;

            [StringLength(100, MinimumLength = 5)]
            public object EmailAddress;

            [Required, StringLength(100, MinimumLength = 3)]
            public object FullName;
        }
    }
}