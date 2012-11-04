using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Ebuy.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(User.Metadata))]
    public class User : Entity<long>
    {
        public virtual ICollection<Auction> Selling { get; private set; }

        public virtual string DisplayName
        {
            get { return _displayName ?? Username; }
            set { _displayName = value; }
        }
        private string _displayName;

        [Unique]
        public string EmailAddress { get; set; }

        public virtual ICollection<Payment> Payments { get; private set; }

        [Unique]
        public string Username { get; set; }

        public virtual ICollection<Auction> WatchedAuctions { get; private set; }


        public User()
        {
            Payments = new Collection<Payment>();
            Selling = new Collection<Auction>();
            WatchedAuctions = new Collection<Auction>();
        }


        protected override string GenerateKey()
        {
            if(string.IsNullOrWhiteSpace(Username))
                throw new EntityKeyGenerationException(GetType(), "Username is empty");

            return KeyGenerator.Generate(Username);
        }


        public void Bid(Auction auction, Currency bidAmount)
        {
            Contract.Requires(auction != null);
            Contract.Requires(bidAmount != null);

            auction.PostBid(this, bidAmount);
        }


        public class Metadata
        {
            [Required, StringLength(50)]
            public object DisplayName;

            [Required, StringLength(100, MinimumLength = 5)]
            public object EmailAddress;

            [Required, StringLength(100, MinimumLength = 3)]
            public object Username;
        }
    }
}