using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class AuctionRepositoryTests : RepositoryTestFixture
    {
        [TestMethod]
        public void ShouldSaveNewAuction()
        {
            AssertCanSaveNewEntity<Auction>();
        }

        [TestMethod]
        public void ShouldFindAuctionById()
        {
            AssertCanFindById<Auction>();
        }

        [TestMethod]
        public void ShouldPersistBids()
        {
            var auction = CreateAndSaveNewEntity<Auction>();

            var user1 = CreateAndSaveNewEntity<User>();
            var user2 = CreateAndSaveNewEntity<User>();

            auction.PostBid(user1, "$10");
            auction.PostBid(user2, "$20");
            auction.PostBid(user1, "$30");

            DataContext.SaveChanges();

            ExecuteInNewContext(context => {
                var savedAuction = context.Auctions.Find(auction.Id);
                Assert.AreEqual(3, savedAuction.Bids.Count);
                Assert.AreEqual("$30", savedAuction.WinningBid.Price);
            });
        }
    }
}