using System.Collections.Generic;

namespace Ebuy.Website.Models
{
    public class FeaturedAuctionsViewModel
    {
        public IEnumerable<AuctionViewModel> Auctions { get; set; }
    }
}