using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Ebuy.DataAccess;

namespace Ebuy.Website.Api
{
    public class AuctionsDataController : ApiController
    {
        private readonly IRepository _repository;

        public AuctionsDataController(IRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Auction> Get()
        {
            return _repository.All<Auction>().AsQueryable();
        }

        [CustomExceptionFilter]
        public Auction Get(string id)
        {
            var result = _repository.Single<Auction>(id);
            
            if (result == null)
            {
                throw new Exception("Item not Found!");
            }

            return result;
        }
        public void Post(Auction auction)
        {
            _repository.Add<Auction>(auction);
        }

        public void Put(string id, Auction auction)
        {
            var currentAuction = _repository.Single<Auction>(id);
            if (currentAuction != null)
            {
                Mapper.DynamicMap(auction, currentAuction);
            }
        }

        public void Delete(string id)
        {
            _repository.Delete<Auction>(id);
        }
    }
}
