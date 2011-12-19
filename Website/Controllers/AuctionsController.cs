using System.Linq;
using System.Web.Mvc;
using Ebuy.DataAccess;
using Ebuy.Website.Models;

namespace Ebuy.Website.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IRepository<Auction> _repository;

        public AuctionsController(IRepository<Auction> repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int page = 0, int pageSize = 25)
        {
            var auctions = _repository.Query(page, pageSize);

            var auctionViewModel = new AuctionsViewModel {
                    Auctions = auctions.Select(x => new AuctionViewModel {
                                                            Key = x.Key,
                                                            Product = x.Product,
                                                            Winner = x.WinningBid.User,
                                                            WinningBidPrice = x.WinningBid.Price,
                                                            EndTime = x.EndTime,
                                                        })
                };

            return View("Auctions", auctionViewModel);
        }

        public ActionResult Auction(string id)
        {
            var auction = _repository.FindByKey(id);

            if (auction == null)
                return View("NotFound");

            return View("Auction", auction);
        }
    }
}
