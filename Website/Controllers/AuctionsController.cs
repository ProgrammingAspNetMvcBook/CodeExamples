using System.Collections.Generic;
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

        [Route("auctions")]
        public ActionResult Index()
        {
            var auctions = _repository.All<Auction>();
            return ActiveAuctionsResult(auctions);
        }

        [Route("auctions/{key}-{title}")]
        [Route("auctions/{key}")]
        public ActionResult Auction(string key)
        {
            var auction = _repository.Single<Auction>(key);

            if (auction == null)
                return View("NotFound");

            var viewModel = Mapper.DynamicMap<AuctionViewModel>(auction);

            viewModel.SuccessfulBid = TempData["SuccessfulBid"] as BidViewModel;

            return View("Auction", viewModel);
        }

        [Route("auctions/{key}-{title}/bids")]
        [Route("auctions/{key}/bids")]
        public ActionResult Bids(string key)
        {
            var auction = _repository.Single<Auction>(key);
            
            if (auction == null)
                return View("NotFound");

            var bids = 
                _repository
                    .Query<Bid>(x => x.Auction.Key == key, "User", "Auction")
                    .OrderByDescending(x => x.Timestamp)
                    .ToArray();

            var viewModel = new BidsViewModel {
                                    Auction = Mapper.DynamicMap<AuctionViewModel>(auction),
                                    Bids = bids.Select(x => new BidViewModel {
                                                Amount = x.Amount,
                                                Timestamp = x.Timestamp,
                                                UserDisplayName = x.User.DisplayName,
                                            }).ToArray(),
                                };

            if (Request.IsAjaxRequest())
                return PartialView("Bids", viewModel);

            return View("Bids", viewModel);
        }

        [Authorize]
        [Route("auctions/{key}-{title}/bid")]
        [Route("auctions/{key}/bid")]
        public ActionResult PlaceBid(string key, User user, double amount)
        {
            var auction = _repository.Single<Auction>(key);

            if (auction == null)
                return View("NotFound");

            var bid = auction.PostBid(user, amount);

            TempData["SuccessfulBid"] = Mapper.DynamicMap<BidViewModel>(bid);

            return RedirectToAction("Auction", new { key });
        }

        [Route("featured")]
        public ActionResult FeaturedAuctions()
        {
            var featuredAuctions = _repository.All<Auction>().Active().Featured();
            featuredAuctions = this.ApplyPaging(featuredAuctions, 5);

            var viewModel = new FeaturedAuctionsViewModel {
                    Auctions = featuredAuctions.Select(Mapper.DynamicMap<AuctionViewModel>)
                };

            if (Request.IsAjaxRequest() || ControllerContext.IsChildAction)
                return PartialView("FeaturedAuctions", viewModel);

            return View("FeaturedAuctions", viewModel);
            
        }

        [Route("categories/{key}")]
        public ActionResult Category(string key, int pageIndex = 0, int pageSize = 25)
        {
            var auctions = _repository.Query<Auction>(x => x.Categories.Any(cat => cat.Key == key));
            return ActiveAuctionsResult(auctions);
        }

        private ActionResult ActiveAuctionsResult(IEnumerable<Auction> auctions)
        {
            var viewModel = this.ApplyPaging(auctions.Active()).Select(Mapper.DynamicMap<AuctionViewModel>);

            return View("Auctions", viewModel);
        }
    }
}
