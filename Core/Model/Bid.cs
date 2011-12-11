using System;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    public class Bid
    {
        public Guid Id { get; set; }

        public Currency Price { get; private set; }
        public DateTime Timestamp { get; private set; }

/*
        public bool IsWinningBid
        {
            get { return this == Auction.WinningBid; }
        }
*/

        [ForeignKey("Auction")]
        public virtual int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }
//        public virtual User User { get; set; }
    }
}