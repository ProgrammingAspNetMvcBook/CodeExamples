using System;
using System.Collections.Generic;

namespace Ebuy.Website.Models
{
    public class AuctionsViewModel
    {
        public IEnumerable<AuctionViewModel> Auctions { get; set; }
    }

    /// <summary>
    /// View Model for an Auction
    /// </summary>
    public class AuctionViewModel
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
        public Currency WinningBidPrice { get; set; }

        /// <summary>
        /// Friendly Representation in days, hours, minutes
        /// </summary>
        /// <example>05d:12h:30m</example>
        public TimeSpan ClosingIn
        {
            get
            {
                return EndTime.Subtract(DateTime.Now);
            }
        }

        /// <summary>
        /// Closing Date in mm/dd/yyyy format
        /// </summary>
        /// <example>18th Oct, 2012 at 5:00 PM</example>
        public DateTime EndTime { get; set; }

        public string Key { get; set; }

        public AuctionViewModel(Ebuy.Auction auctionDto)
        {
            Product = auctionDto.Product;
            Winner = auctionDto.Owner;
            WinningBidPrice = auctionDto.WinningBid.Price;
            EndTime = auctionDto.EndTime;
        }

        public AuctionViewModel()
        {

        }


    }
}