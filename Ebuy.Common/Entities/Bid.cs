using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Ebuy
{
    public class Bid : Entity<Guid>, IEquatable<Bid>
    {
        public Currency Amount { get; private set; }

        public virtual Auction Auction { get; private set; }

        public DateTime Timestamp { get; private set; }

        public virtual User User { get; private set; }

        public bool IsWinningBid
        {
            get
            {
                return Auction != null 
                       && Id == Auction.WinningBidId;
            }
        }


        public Bid(User user, Auction auction, Currency price) 
        {
            Contract.Requires(user != null);
            Contract.Requires(auction != null);
            Contract.Requires(price != null);

            User = user;
            Auction = auction;
            Amount = price;
            Timestamp = DateTime.Now;
        }

        public Bid()
        {
        }

        public bool Equals(Bid other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public class Metadata
        {
            [Required]
            public object Auction;

            [Required]
            public object Amount;

            [Required]
            public object Timestamp;

            [Required]
            public object User;
        }
    }
}