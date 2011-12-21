using System;
using System.Linq;
using Ebuy;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    public abstract class DataContextTestFixture
    {
        protected DataContext DataContext { get; set; }

        protected TestDataGenerator TestDataGenerator
        {
            get { return TestDataGenerator.Current; }
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            DataContext = new DataContext();
        }

        protected void ExecuteInNewContext(Action<DataContext> action)
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

            ExecuteInNewContext(context => {
                var saved = context.Set<TEntity>().Find(id);
                Assert.IsNotNull(saved);
            });
        }


        protected void AssertNoSavedEntitiesMatching<TEntity>(Func<TEntity, bool> predicate) 
            where TEntity : class
        {
            ExecuteInNewContext(context => 
                Assert.IsFalse(context.Set<TEntity>().Any(predicate)));
        }


        protected virtual TEntity CreateNewEntity<TEntity>()
        {
            return TestDataGenerator.GenerateValid<TEntity>();
        }

        protected virtual TEntity CreateAndSaveNewEntity<TEntity>() 
            where TEntity : class, IEntity
        {
            var entity = TestDataGenerator.GenerateValid<TEntity>();

            ExecuteInNewContext(context => {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            });

            // Make sure the Id was updated!
            Assert.AreNotEqual(0, entity.Id);

            return entity;
        }
    }
}