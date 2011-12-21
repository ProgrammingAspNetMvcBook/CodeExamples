using System;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    public class Bid : IEquatable<Bid>
    {
        public virtual Guid Id
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

        public virtual bool IsWinningBid
        {
            get
            {
                return Auction != null 
                       && this == Auction.WinningBid;
            }
        }

        public virtual Currency Price { get; private set; }

        public virtual DateTime Timestamp { get; private set; }

        public virtual User User { get; private set; }


        public Bid(User user, Auction auction, Currency price) 
            : this(user, auction, price, DateTime.UtcNow)
        {
        }

        protected internal Bid(User user, Auction auction, Currency price, DateTime timestamp)
        {
            User = user;
            Auction = auction;
            Price = price;
            Timestamp = timestamp;
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
            [InverseProperty("Bids")]
            public object Auction { get; set; }

            [Required]
            public object Price { get; set; }

            [Required]
            public object Timestamp { get; set; }

            [Required]
            [InverseProperty("Bids")]
            public object User { get; set; }
        }
    }
}