using System.Web.Mvc;
using Ebuy.Website.Models;

namespace Ebuy.Website.Extensions
{

    public static class UrlHelperExtensions
    {

        public static string Category(this UrlHelper helper, CategoryViewModel category)
        {
            return helper.Action("Category", "Auctions", new { key = category.Key });
        }

        public static string Auction(this UrlHelper helper, Auction auction)
        {
            return helper.Action("Auction", "Auctions", new { key = auction.Key, title = auction.Title });
        }

        public static string Auction(this UrlHelper helper, AuctionViewModel auction)
        {
            return helper.Action("Auction", "Auctions", new { key = auction.Key, title = auction.Title });
        }

    }

}