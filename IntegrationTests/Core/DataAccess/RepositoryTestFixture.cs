using System.Data;
using Ebuy;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    public abstract class RepositoryTestFixture<T> where T : Entity
    {
        protected DataContext DataContext { get; private set; }
        protected Repository<T> Repository { get; private set; }

        protected void AssertCanSaveNewEntity()
        {
            var entity = CreateValidEntity();

            Repository.Save(entity);

            Assert.AreNotEqual(default(int), entity.Id);
            Assert.AreEqual(EntityState.Unchanged, DataContext.Entry(entity).State);
        }

        protected void AssertCanUpdateEntity()
        {
            var entity = CreateValidEntity();

            Repository.Save(entity);

            DataContext.Entry(entity).
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DataContext = new DataContext();
            Repository = new Repository<T>(DataContext, false);
        }

        protected abstract T CreateValidEntity();
    }
}
