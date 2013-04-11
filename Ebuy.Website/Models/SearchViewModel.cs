using System.Collections.Generic;

namespace Ebuy.Website.Models
{
    public class SearchViewModel
    {
        public string SearchKeyword { get; set; }

        public int CurrentPage { get; set; }
        public int MaxPages { get; set; }

        public int PagingSize { get; set; }
        public IEnumerable<int> PagingSizeList { get; set; }

        public string SortByField { get; set; }
        public IEnumerable<string> SortByFieldList { get; set; }

        public IEnumerable<AuctionViewModel> SearchResult { get; set; }
    }
}