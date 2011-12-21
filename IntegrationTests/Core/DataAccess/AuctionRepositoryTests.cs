using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class AuctionRepositoryTests : RepositoryTestFixture<Auction>
    {
        [TestMethod]
        public void ShouldSaveNewAuction()
        {
            AssertCanSaveNewEntity();
        }

        [TestMethod]
        public void ShouldFindAuctionById()
        {
            AssertCanFindById();
        }

        [TestMethod]
        public void ShouldPersistBids()
        {
            var auction = CreateAndSaveNewEntity<Auction>();

            var user1 = CreateAndSaveNewEntity<User>();
            var user2 = CreateAndSaveNewEntity<User>();

            auction.PostBid(new Bid { Price = "$10", User = user1 });
            auction.PostBid(new Bid { Price = "$20", User = user2 });
            auction.PostBid(new Bid { Price = "$30", User = user1 });

            DataContext.SaveChanges();

            ExecuteInNewContext(context => {
                var savedAuction = context.Auctions.Find(auction.Id);
                Assert.IsNotNull(savedAuction.WinningBid);
                Assert.AreEqual(30, savedAuction.WinningBid.Price.Amount);
                Assert.AreEqual(3, savedAuction.Bids.Count);
            });
        }
    }
}