using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class CategoryPersistenceTests : RepositoryTestFixture
    {
        [TestMethod]
        public void ShouldSaveNewCategory()
        {
            AssertCanSaveNewEntity<Category>();
        }
    }
}