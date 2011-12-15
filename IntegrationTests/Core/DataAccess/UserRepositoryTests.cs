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
            var user1 = CreateNewEntity();
            Repository.Save(user1);

            var user2 = CreateNewEntity();
            user2.EmailAddress = user1.EmailAddress;

            AssertException.Throws<DbUpdateException>(() =>
                    Repository.Save(user2),
                    "Expected unique constraint exception but it did not get thrown"
                );
        }
    }
}