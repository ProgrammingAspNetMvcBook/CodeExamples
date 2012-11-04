using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ebuy.DataAccess;
using Ebuy.Website.Models;

namespace Ebuy.Website.Controllers
{
    public class SearchController : Controller
    {
        private readonly IRepository _repository;

        public SearchController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(SearchCriteria criteria)
        {
            IQueryable<Auction> auctions;

            // Filter auctions by keyword
            if (!string.IsNullOrEmpty(criteria.SearchKeyword))
                auctions = _repository.Query<Auction>(q => q.Description.Contains(criteria.SearchKeyword));
            else
                auctions = _repository.All<Auction>();

            switch (criteria.GetSortByField())
            {
                case SearchCriteria.SearchFieldType.Price:
                    auctions = auctions.OrderBy(q => q.CurrentPrice.Value);
                    break;

                case SearchCriteria.SearchFieldType.RemainingTime:
                    auctions = auctions.OrderBy(q => q.EndTime);
                    break;

                case SearchCriteria.SearchFieldType.Keyword:
                default:
                    auctions = auctions.OrderBy(q => q.Title);
                    break;
            }

            auctions = PageSearchResult(criteria, auctions);

            // Populate the view model
            var viewModel = new SearchViewModel();

            // Copy values from the criteria object to the view model
            Mapper.DynamicMap(criteria, viewModel);

            // Map the matching Auctions to view models
            viewModel.SearchResult = Mapper.DynamicMap<IEnumerable<AuctionViewModel>>(auctions);

            return View("Search", viewModel);
        }

        private IQueryable<Auction> PageSearchResult(SearchCriteria criteria, IQueryable<Auction> auctionsData)
        {
            IQueryable<Auction> result;
            
            var numberOfItems = auctionsData.Count();

            if (numberOfItems > criteria.GetPageSize())
            {
                var maxNumberOfPages = numberOfItems/criteria.GetPageSize();

                if (criteria.CurrentPage > maxNumberOfPages)
                    criteria.CurrentPage = maxNumberOfPages;

                result = auctionsData.Page(criteria.CurrentPage, criteria.GetPageSize()).AsQueryable();
            }
            else
            {
                result = auctionsData;
            }

            return result;
        }
    }
}
