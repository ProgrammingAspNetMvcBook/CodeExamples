using System.Data.Entity.Infrastructure;
using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class UserRepositoryTests : RepositoryTestFixture<User>
    {
        [TestMethod]
        public void ShouldSaveNewUser()
        {
            AssertCanSaveNewEntity();
        }

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


        private volatile static int _lastUserId = 0;

        protected override User CreateValidEntity()
        {
            var userId = _lastUserId++;

            return new User()
                       {
                           EmailAddress = string.Format("user_{0}@email.com", userId),
                           FullName = "Test User #" + userId,
                       };
        }
    }
}