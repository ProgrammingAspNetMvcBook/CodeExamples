using System;
using System.Collections.Generic;
using System.Linq;

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

        public Bid WinningBid { get; set; }

        public bool HasWinningBid
        {
            get { return WinningBid != null; }
        }

        public string WinningBidUsername
        {
            get
            {
                if (HasWinningBid)
                    return WinningBid.User.DisplayName;

                return string.Empty;
            }
        }

        public Currency WinningBidPrice
        {
            get
            {
                if (HasWinningBid)
                    return WinningBid.Price;

                return null;
            }
        }

        public WebsiteImage Image
        {
            get
            {
                var images = Images ?? Enumerable.Empty<WebsiteImage>();
                return images.FirstOrDefault();
            }
        }

        public IEnumerable<WebsiteImage> Images { get; set; }

        /// <summary>
        /// Display Representation in days, hours, minutes
        /// </summary>
        /// <example>05d:12h:30m</example>
        public string RemainingTimeDisplay
        {
            get
            {
                if (EndTime == null)
                    return "N/A";

                var time = EndTime.Value.Subtract(DateTime.UtcNow);

                if (time.Days > 0)
                    return string.Format("{0} days, {1} hours",
                                         time.Days, time.Hours);

                if (time.Hours > 0)
                    return string.Format("{0} hours, {1} minutes",
                                         time.Hours, time.Minutes);

                if (time.Minutes > 0)
                    return string.Format("{0} minutes, {1} seconds",
                                         time.Minutes, time.Seconds);

                if (time.Seconds > 0)
                    return string.Format("{0} seconds", time.Seconds);

                return "Closed";
            }
        }

        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Closing Date in mm/dd/yyyy format
        /// </summary>
        /// <example>18th Oct, 2012 at 5:00 PM</example>
        public string EndTimeDisplay
        {
            get
            {
                if (EndTime == null)
                    return string.Empty;

                return EndTime.Value.ToString("MMM d, yyyy 'at' h:mm tt 'GMT'");
            }
        }

        public string Key { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}