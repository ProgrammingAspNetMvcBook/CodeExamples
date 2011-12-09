using System;

namespace Ebuy
{
    public class Bid
    {
        public Currency Price { get; private set; }
        public DateTime Timestamp { get; private set; }

        public bool IsWinningBid
        {
            get { return this == Auction.WinningBid; }
        }

        public virtual Auction Auction { get; private set; }
        public virtual User User { get; private set; }
    }
}