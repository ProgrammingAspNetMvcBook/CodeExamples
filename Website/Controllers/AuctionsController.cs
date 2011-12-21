using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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

        public ActionResult Auction(string id)
        {
            var auction = _repository.Single<Auction>(id);

            if (auction == null)
                return View("NotFound");

            var viewModel = Mapper.DynamicMap<AuctionViewModel>(auction);
            return View("Auction", viewModel);
        }
    }
}
