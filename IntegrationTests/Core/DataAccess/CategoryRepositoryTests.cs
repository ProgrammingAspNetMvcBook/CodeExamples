using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class CategoryRepositoryTests : RepositoryTestFixture
    {
        [TestMethod]
        public void ShouldSaveNewCategory()
        {
            AssertCanSaveNewEntity<Category>();
        }
    }
}