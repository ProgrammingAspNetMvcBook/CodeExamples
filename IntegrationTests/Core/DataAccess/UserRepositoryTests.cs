using System.Data.Entity.Infrastructure;
using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class UserRepositoryTests : RepositoryTestFixture
    {
        [TestMethod]
        public void ShouldSaveNewUser()
        {
            AssertCanSaveNewEntity<User>();
        }

        [TestMethod]
        public void ShouldNotAllowDuplicateEmailAddresses()
        {
            var user1 = CreateNewEntity<User>();
            Repository.Add(user1);

            var user2 = CreateNewEntity<User>();
            user2.EmailAddress = user1.EmailAddress;

            AssertException.Throws<DbUpdateException>(() =>
                    Repository.Add(user2),
                    "Expected unique constraint exception but it did not get thrown"
                );
        }
    }
}