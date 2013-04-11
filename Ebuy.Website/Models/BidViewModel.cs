using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ebuy.Website.Models
{
	public class BidsViewModel
	{
		public AuctionViewModel Auction { get; set; }

		public IEnumerable<BidViewModel> Bids { get; set; }


		public string Title
		{
			get { return Auction.Title; }
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