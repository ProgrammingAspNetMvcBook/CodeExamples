using System;
using System.Data.Entity;
using Ebuy;
using Ebuy.DataAccess;
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

        protected override Auction CreateValidEntity()
        {
            return new Auction()
                       {
                           StartTime = DateTime.Now,
                           EndTime = DateTime.Now.AddDays(7),
                       };
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Database.SetInitializer(new DataContext.Initializer());
        }
    }
}