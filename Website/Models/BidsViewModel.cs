using System;
using System.Collections.Generic;

namespace Ebuy.Website.Models
{
    public class BidsViewModel
    {
        public AuctionViewModel Auction { get; set; }
        
        public IEnumerable<BidViewModel> Bids { get; set; }

        public string Title
        {
            get { return string.Format("Bid history for {0}", Auction.Title); }
        }
    }

    public class BidViewModel
    {
        public string Amount { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public string TimestampDisplay
        {
            get { return Timestamp.ToString("G"); }
        }
        
        public string UserDisplayName { get; set; }
    }
}