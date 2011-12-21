using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(User.Metadata))]
    public class User : Entity
    {
        public virtual ICollection<Bid> Bids { get; private set; }

        public virtual string DisplayName
        {
            get { return _displayName ?? FullName; }
            set { _displayName = value; }
        }
        private string _displayName;

        [Unique]
        public virtual string EmailAddress { get; set; }

        public virtual string FullName { get; set; }

        public virtual ICollection<Payment> Payments { get; private set; }

        public virtual ICollection<Review> Reviews { get; private set; }

        public virtual ICollection<Auction> WatchedAuctions { get; private set; }


        public User()
        {
            Bids = new Collection<Bid>();
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
            Bid(auction, bidAmount, DateTime.Now);
        }

        protected internal virtual void Bid(Auction auction, Currency bidAmount, DateTime timestamp)
        {
            Contract.Requires(auction != null);
            Contract.Requires(bidAmount != null);

            auction.PostBid(this, bidAmount, timestamp);
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