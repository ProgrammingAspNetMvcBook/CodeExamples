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
    }
}