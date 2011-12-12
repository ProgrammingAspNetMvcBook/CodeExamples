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
            Database.SetInitializer(new DataContext.Initializer());
        }
    }
}
