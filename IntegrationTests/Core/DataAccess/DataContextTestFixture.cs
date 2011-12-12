using System;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    public abstract class DataContextTestFixture<T> where T : class
    {
        protected DataContext DataContext { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            DataContext = new DataContext();
        }


        protected void AssertEntitySaved(long id, Func<T, bool> predicate = null)
        {
            AssertEntitySaved<T>(id, predicate);
        }

        protected void AssertEntitySaved<TEntity>(long id, Func<TEntity, bool> predicate = null)
            where TEntity : class
        {
            Assert.AreNotEqual(default(long), id);

            using (var context = new DataContext())
            {
                var saved = context.Set<TEntity>().Find(id);

                Assert.IsNotNull(saved);

                if (predicate != null)
                    Assert.IsTrue(predicate(saved));
            }
        }


        protected virtual T CreateNewEntity()
        {
            return TestDataGenerator.Current.GenerateValid<T>();
        }

        protected virtual T CreateSaveAndRetrieveNewEntity()
        {
            var entity = TestDataGenerator.Current.GenerateValid<T>();

            using (var context = new DataContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }

            return entity;
        }
    }
}