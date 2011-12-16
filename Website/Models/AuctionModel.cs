using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ebuy;

namespace Website.Models
{
    public class AuctionModel
    {
        public IEnumerable<Auction> Auctions { get; set; }
    }

    /// <summary>
    /// View Model for an Auction
    /// </summary>
    public class Auction
    {
        public Product Product { get; set; }
        public User Winner { get; set; }
        public Currency BidPrice { get; set; }
        public TimeSpan ClosingIn { get; set; }
    }
}