using System.Configuration;
using System.Data.Entity;
using Devtalk.EF.CodeFirst;
using Ebuy.DataAccess;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DataAccess), "InitializeDatabase")]

namespace Ebuy.Website.App_Start
{
    public static class DataAccess
    {
        static bool IsAppHarbor
        {
            get { return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["appharbor.commit_id"]); }
        }

        public static void InitializeDatabase()
        {
            IDatabaseInitializer<DataContext> initializer;

            if (IsAppHarbor)
                initializer = new DontDropDbJustCreateTablesIfModelChanged<DataContext>();
            else
                initializer = new DropCreateDatabaseAlways<DataContext>();

            Database.SetInitializer(new DataContext.DemoDataInitializer(initializer));

            using (var context = new DataContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
