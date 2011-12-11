using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Ebuy;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class ProductRepositoryTests : RepositoryTests<Product> {
        protected override Product CreateValidEntity()
        {
            return new Product()
                       {
                           Name = "Test product",
                           Description = "Test product",
                           ImageUrl = "http://www.test.com/image.png",
                       };
        }
    }

    [TestClass]
    public class UserRepositoryTests : RepositoryTests<User>
    {
        private volatile static int LastUserId = 0;
        
        [TestMethod]
        public void ShouldNotAllowDuplicateEmailAddresses()
        {
            var user1 = CreateValidEntity();
            Repository.Save(user1);

            var user2 = CreateValidEntity();
            user2.EmailAddress = user1.EmailAddress;

            AssertException.Throws<DbUpdateException>(() => 
                Repository.Save(user2),
                "Expected unique constraint exception but it did not get thrown"
            );
        }
        
        protected override User CreateValidEntity()
        {
            var userId = LastUserId++;

            return new User()
                       {
                           EmailAddress = string.Format("user_{0}@email.com", userId),
                           FullName = "Test User #" + userId,
                       };
        }
    }

    [TestClass]
    public class AuctionRepositoryTests : RepositoryTests<Auction> {
        protected override Auction CreateValidEntity()
        {
            return new Auction()
                       {
                           StartTime = DateTime.Now,
                           EndTime = DateTime.Now.AddDays(7),
                       };
        }

        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            Database.SetInitializer(new EbuyDataContext.Initializer());
        }

    }


    public abstract class RepositoryTests<T> where T : Entity, new()
    {
        protected EbuyDataContext DataContext { get; private set; }
        protected Repository<T> Repository { get; private set; }

        [TestMethod]
        public void ShouldSaveNewEntity()
        {
            var entity = CreateValidEntity();

            Repository.Save(entity);

            Assert.AreNotEqual(0, entity.Id);
            Assert.AreEqual(EntityState.Unchanged, DataContext.Entry(entity).State);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DataContext = new EbuyDataContext();
            Repository = new Repository<T>(DataContext, false);
        }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Database.SetInitializer(new EbuyDataContext.Initializer());
        }

        protected abstract T CreateValidEntity();
    }
}
