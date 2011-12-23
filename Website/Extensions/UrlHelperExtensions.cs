using System.Web.Mvc;
using Ebuy.Website.Models;

namespace Ebuy.Website.Extensions
{

    public static class UrlHelperExtensions
    {

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