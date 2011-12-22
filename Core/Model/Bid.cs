using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Ebuy
{
    public class Bid : IEquatable<Bid>
    {
        public Guid Id
        {
            get
            {
                if (_id == null)
                    _id = Guid.NewGuid();
                
                return _id.Value;
            }
            private set { _id = value; }
        }
        private Guid? _id;

        public virtual Auction Auction { get; private set; }

        public bool IsWinningBid
        {
            get
            {
                return Auction != null 
                    && this == Auction.WinningBid;
            }
        }

        public Currency Amount { get; private set; }

        public DateTime Timestamp { get; private set; }

        public virtual User User { get; private set; }


        public Bid(User user, Auction auction, Currency price) 
        {
            Contract.Requires(user != null);
            Contract.Requires(auction != null);
            Contract.Requires(price != null);

            User = user;
            Auction = auction;
            Amount = price;
            Timestamp = Clock.Now;
        }

        private Bid()
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Bid)) return false;
            return Equals((Bid) obj);
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

        public static bool operator ==(Bid left, Bid right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Bid left, Bid right)
        {
            return !Equals(left, right);
        }


        public class Metadata
        {
            [Required]
            public object Auction;

            [Required]
            public object Price;

            [Required]
            public object Timestamp;

            [Required]
            public object User;
        }
    }
}