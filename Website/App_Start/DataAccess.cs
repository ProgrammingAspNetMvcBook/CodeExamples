using System.Configuration;
using System.Data.Entity;
using Devtalk.EF.CodeFirst;
using Ebuy.DataAccess;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DataAccess), "Start")]

namespace Ebuy.Website.App_Start
{
    public static class DataAccess
    {
        static  bool IsAppHarbor
        {
            get { return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["appharbor.commit_id"]); }
        }

        public static void Start()
        {
            IDatabaseInitializer<DataContext> initializer;

            if (IsAppHarbor)
                initializer = new DontDropDbJustCreateTablesIfModelChanged<DataContext>();
            else
//                    databaseInitializer = new DropCreateDatabaseIfModelChanges<DataContext>();
                initializer = new DropCreateDatabaseAlways<DataContext>();

            Database.SetInitializer(new DataContext.DemoDataInitializer(initializer));
        }
    }
}
