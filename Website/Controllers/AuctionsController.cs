using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CustomExtensions.Routing;
using Ebuy.DataAccess;
using Ebuy.Website.Models;

namespace Ebuy.Website.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IRepository _repository;

        public AuctionsController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int page = 0, int pageSize = 25)
        {
            var auctions = _repository.All<Auction>(page, pageSize);

            var viewModel = auctions.Select(Mapper.DynamicMap<AuctionViewModel>);

            return View("Auctions", viewModel);
        }

        public ActionResult Auction(string key)
        {
            var auction = _repository.Single<Auction>(key);

            if (auction == null)
                return View("NotFound");

            var viewModel = Mapper.DynamicMap<AuctionViewModel>(auction);
            return View("Auction", viewModel);
        }

        [Route("auctions/{title}/{key}/bids")]
        public ActionResult Bids(string key)
        {
            var auction = _repository.Query<Auction>(x => x.Key == key);

            if (auction == null || auction.Count() == 0)
                return View("NotFound");

            var bids = auction.FirstOrDefault().Bids.AsQueryable();

            var viewModel = new BidsViewModel
                                {
                                    Auction = Mapper.DynamicMap<AuctionViewModel>(auction),
                                    Bids = bids.Select(x => new BidViewModel()
                                    {
                                                Amount = x.Amount.ToString(),
                                                Timestamp = x.Timestamp,
                                                UserDisplayName = x.User.DisplayName,
                                            }).ToArray(),
                                };

            return View("Bids", viewModel);
        }

    }
}
