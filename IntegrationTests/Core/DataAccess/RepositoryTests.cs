using Ebuy;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    public abstract class RepositoryTestFixture<T> : DataContextTestFixture<T>
        where T : Entity, IKeyedEntity
    {
        protected Repository<T> Repository { get; set; }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Repository = new Repository<T>(DataContext, false);
        }

        protected void AssertCanSaveNewEntity()
        {
            var entity = CreateNewEntity();
            Repository.Save(entity);
            AssertSavedEntityExists(entity);
        }

        protected virtual void AssertCanFindById()
        {
            var expectedEntity = CreateSaveAndRetrieveNewEntity();

            var saved = Repository.FindById(expectedEntity.Id);

            Assert.IsNotNull(saved);
            Assert.AreEqual(expectedEntity.Id, saved.Id);
        }
    }
}
