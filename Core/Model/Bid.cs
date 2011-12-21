using System;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    public class Bid
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

        public virtual Auction Auction { get; set; }

        public virtual bool IsWinningBid
        {
            get
            {
                return Auction != null 
                       && this == Auction.WinningBid;
            }
        }

        public virtual Currency Price { get; set; }

        public virtual DateTime Timestamp { get; private set; }

        public virtual User User { get; set; }


        public Bid() : this(DateTime.UtcNow)
        {
        }

        protected internal Bid(DateTime timestamp)
        {
            Timestamp = timestamp;
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
            public object User { get; set; }
        }

    }
}