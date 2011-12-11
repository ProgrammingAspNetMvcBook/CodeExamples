using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class ProductRepositoryTests : RepositoryTestFixture<Product>
    {
        [TestMethod]
        public void ShouldSaveNewProduct()
        {
            AssertCanSaveNewEntity();
        }

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
}