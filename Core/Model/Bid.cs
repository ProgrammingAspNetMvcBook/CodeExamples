using System;
using System.ComponentModel.DataAnnotations;

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

        public Auction Auction { get; private set; }

        public bool IsWinningBid
        {
            get
            {
                return Auction != null 
                    && this == Auction.WinningBid;
            }
        }

        public Currency Price { get; private set; }

        public DateTime Timestamp { get; private set; }

        public User User { get; private set; }


        public Bid(User user, Auction auction, Currency price) 
        {
            User = user;
            Auction = auction;
            Price = price;
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
            [InverseProperty("Bids")]
            public object Auction;

            [Required]
            public object Price;

            [Required]
            public object Timestamp;

            [Required]
            [InverseProperty("Bids")]
            public object User;
        }
    }
}