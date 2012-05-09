using System.Data.Entity;
using Ebuy.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.Core.DataAccess
{
    [TestClass]
    public class RepositoryInitializer
    {
        [AssemblyInitialize]
        public static void InitializeDatabase(TestContext context)
        {
            var initializer = new DataContext.DemoDataInitializer(new DropCreateDatabaseAlways<DataContext>());
            Database.SetInitializer(initializer);
        }
    }
}
