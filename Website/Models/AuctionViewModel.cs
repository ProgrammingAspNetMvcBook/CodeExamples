using System;
using System.Collections.Generic;
using System.Linq;

namespace Ebuy.Website.Models
{
    public class AuctionViewModel
    {
        public string CurrencyCode
        {
            get { return CurrentPrice.Code; }
        }

        public string Description { get; set; }

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

        public bool HasWinningBid
        {
            get { return WinningBid != null; }
        }

        public bool HasSuccessfulBid
        {
            get { return SuccessfulBid != null; }
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

        public string Key { get; set; }

        public Currency MinimumBid
        {
            get
            {
                var currentBidAmount = HasWinningBid ? WinningBid.Amount : CurrentPrice;
                return currentBidAmount + 0.1;
            }
        }

        public string MinimumBidValue
        {
            get { return MinimumBid.Value.ToString("N2"); }
        }

        public string ProductKey { get; set; }

        public string ProductName { get; set; }

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

                var time = EndTime.Value.Subtract(Clock.Now);

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

        public Currency CurrentPrice { get; set; }

        public BidViewModel SuccessfulBid { get; set; }

        public string Title { get; set; }

        public Bid WinningBid { get; set; }

        public string WinningBidUsername
        {
            get
            {
                if (HasWinningBid && WinningBid.User != null)
                    return WinningBid.User.DisplayName;

                return string.Empty;
            }
        }
    }
}