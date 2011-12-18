using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ebuy;

namespace Website.Models
{
    public class AuctionViewModel
    {
        public IEnumerable<Auction> Auctions { get; set; }
    }

    /// <summary>
    /// View Model for an Auction
    /// </summary>
    public class Auction
    {
        /// <summary>
        /// Product that is being auctioned
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Current Bid Winner. Will keep changing till the Auction closes
        /// </summary>
        public User Winner { get; set; }

        /// <summary>
        /// Price for next bid
        /// </summary>
        public Currency BidPrice { get; set; }

        /// <summary>
        /// Friendly Representation in days, hours, minutes
        /// </summary>
        /// <example>05d:12h:30m</example>
        public TimeSpan ClosingIn
        {
            get
            {
                return ClosingTime.Subtract(DateTime.Now);
            }
        }

        /// <summary>
        /// Closing Date in mm/dd/yyyy format
        /// </summary>
        /// <example>18th Oct, 2012 at 5:00 PM</example>
        public DateTime ClosingTime { get; set; }

        public Auction(Ebuy.Auction auctionDto)
        {
            Product = auctionDto.Product;
            Winner = auctionDto.Owner;
            BidPrice = auctionDto.WinningBid.Price;
            ClosingTime = auctionDto.EndTime;
        }

        public Auction()
        {

        }


    }
}