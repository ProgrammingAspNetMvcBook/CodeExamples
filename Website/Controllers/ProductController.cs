using System.Data.Objects;
using System.Web.Mvc;
using System.Linq;
using Website.Models;
using System;

namespace Website.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Auctions()
        {
            var dc = new Ebuy.DataAccess.DataContext();
            
            dc.Database.Initialize(true);

            var auctionViewModel = new AuctionViewModel
                                       {
                                           Auctions = dc.Auctions.Select(x => new Auction
                                                                                  {
                                                                                      Product = x.Product,
                                                                                      Winner = x.Owner,
                                                                                      BidPrice = x.WinningBid.Price,
                                                                                      ClosingTime = x.EndTime,
                                                                                      
                                                                                  })
                                       };
            return View(auctionViewModel);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
