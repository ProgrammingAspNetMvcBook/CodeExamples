using System;
using System.Linq;
using Ebuy;
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

        protected void ImmediatelyExecute(Action<DataContext> action)
        {
            using (var context = new DataContext())
                action(context);
        }


        protected void AssertSavedEntityExists<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            AssertSavedEntityExists<TEntity>(entity.Id);
        }

        protected void AssertSavedEntityExists<TEntity>(long id)
            where TEntity : class
        {
            Assert.AreNotEqual(default(long), id);

            ImmediatelyExecute(context => {
                var saved = context.Set<TEntity>().Find(id);
                Assert.IsNotNull(saved);
            });
        }


        protected void AssertNoSavedEntitiesMatching<TEntity>(Func<TEntity, bool> predicate) 
            where TEntity : class
        {
            ImmediatelyExecute(context => 
                Assert.IsFalse(context.Set<TEntity>().Any(predicate)));
        }


        protected virtual T CreateNewEntity()
        {
            return TestDataGenerator.Current.GenerateValid<T>();
        }

        protected virtual T CreateSaveAndRetrieveNewEntity()
        {
            var entity = TestDataGenerator.Current.GenerateValid<T>();

            ImmediatelyExecute(context => {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            });

            return entity;
        }
    }
}