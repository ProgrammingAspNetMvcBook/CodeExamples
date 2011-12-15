using System;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    public class Bid
    {
        public virtual Guid Id
        {
            get { return _id ?? Guid.NewGuid(); }
            set { _id = value; }
        }
        private Guid? _id;

        public Currency Price { get; set; }
        public DateTime Timestamp { get; set; }

        public bool IsWinningBid
        {
            get { return this == Auction.WinningBid; }
        }

        public virtual Auction Auction { get; set; }
        public virtual User User { get; set; }


        public class Metadata
        {
            [Required]
            public object Price { get; set; }

            [Required]
            public object Timestamp { get; set; }

            [Required]
            public object Auction { get; set; }

            [Required]
            public object User { get; set; }
        }

    }
}