using System.Collections.Generic;

namespace Ebuy.Website.Models
{
    public class HomepageViewModel
    {

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public AuctionViewModel FeaturedAuction { get; set; }

    }
}