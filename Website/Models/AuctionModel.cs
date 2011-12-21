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
        public string ProductKey { get; set; }
        public string ProductName { get; set; }

        public string WinningBidUserDisplayName { get; set; }
        public Currency WinningBidPrice { get; set; }

        public IEnumerable<WebsiteImage> Images { get; set; }

        /// <summary>
        /// Friendly Representation in days, hours, minutes
        /// </summary>
        /// <example>05d:12h:30m</example>
        public string ClosingIn
        {
            get
            {
                if (EndTime == null)
                    return "Unknown";

                var span = EndTime.Value.Subtract(DateTime.Now);

                return span.ToString();
            }
        }

        /// <summary>
        /// Closing Date in mm/dd/yyyy format
        /// </summary>
        /// <example>18th Oct, 2012 at 5:00 PM</example>
        public DateTime? EndTime { get; set; }

        public string EndTimeDisplay
        {
            get;
            set;
        }

        public string Key { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}