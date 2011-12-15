using System.Linq;
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
            var product = CreateNewEntity();

            Repository.Save(product);
            AssertSavedEntityExists(product);
        }

        [TestMethod]
        public void ShouldSaveNewProductCategories()
        {
            var product = CreateNewEntity();

            var categories = product.Categories.Select(x => x.Key).ToArray();
            AssertNoSavedEntitiesMatching<Category>(x => categories.Contains(x.Key));

            Repository.Save(product);

            foreach (var category in product.Categories)
            {
                AssertSavedEntityExists(category);
            }
        }
    }
}