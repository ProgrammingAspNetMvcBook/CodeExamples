using Ebuy;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    public abstract class RepositoryTestFixture : DataContextTestFixture
    {
        protected Repository Repository { get; set; }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Repository = new Repository(DataContext, false);
        }

        protected void AssertCanSaveNewEntity<T>() where T : class, IEntity
        {
            var entity = CreateNewEntity<T>();
            Repository.Add(entity);
            AssertSavedEntityExists(entity);
        }

        protected virtual void AssertCanFindById<T>() where T : class, IEntity
        {
            var expectedEntity = CreateAndSaveNewEntity<T>();

            var saved = Repository.Single<T>(x => x.Id == expectedEntity.Id);

            Assert.IsNotNull(saved);
            Assert.AreEqual(expectedEntity.Id, saved.Id);
        }
    }
}
