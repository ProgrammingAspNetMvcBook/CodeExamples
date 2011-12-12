using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class CategoryRepositoryTests : RepositoryTestFixture<Category>
    {
        [TestMethod]
        public void ShouldSaveNewCategory()
        {
            AssertCanSaveNewEntity();
        }
    }
}