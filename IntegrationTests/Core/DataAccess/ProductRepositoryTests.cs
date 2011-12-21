using System.Linq;
using Ebuy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class ProductRepositoryTests : RepositoryTestFixture
    {
        [TestMethod]
        public void ShouldSaveNewProduct()
        {
            var product = CreateNewEntity<Product>();

            Repository.Add(product);
            AssertSavedEntityExists(product);
        }

        [TestMethod]
        public void ShouldSaveNewProductCategories()
        {
            var product = CreateNewEntity<Product>();

            var categories = product.Categories.Select(x => x.Key).ToArray();
            AssertNoSavedEntitiesMatching<Category>(x => categories.Contains(x.Key));

            Repository.Add(product);

            foreach (var category in product.Categories)
            {
                AssertSavedEntityExists(category);
            }
        }
    }
}