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
            AssertSavedEntityExists<TEntity>(entity.Key);
        }

        protected void AssertSavedEntityExists<TEntity>(string key)
            where TEntity : class, IEntity
        {
            Assert.IsNotNull(key);
            Assert.AreNotEqual(string.Empty, key);

            ExecuteInNewContext(context => {
                var saved = context.Set<TEntity>().FirstOrDefault(x => x.Key == key);
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
            string key = null;

            ExecuteInNewContext(context => {
                var entity = TestDataGenerator.GenerateValid<TEntity>();
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();

                key = entity.Key;
            });

            Assert.IsNotNull(key);

            return DataContext.Set<TEntity>().FirstOrDefault(x => x.Key == key);
        }
    }
}