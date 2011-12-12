using Ebuy;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    public partial class RepositoryTestFixture<T> : DataContextTestFixture<T>
        where T : Entity
    {
        protected Repository<T> Repository { get; set; }

        protected void AssertCanSaveNewEntity()
        {
            var entity = CreateNewEntity();
            Repository.Save(entity);
            AssertEntitySaved(entity.Id);
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Repository = new Repository<T>(DataContext, false);
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
