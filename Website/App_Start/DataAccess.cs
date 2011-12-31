using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using Devtalk.EF.CodeFirst;
using Ebuy.DataAccess;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DataAccess), "InitializeDatabase")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(DataAccess), "CreateAdminUser")]

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
                //                    databaseInitializer = new DropCreateDatabaseIfModelChanges<DataContext>();
                initializer = new DropCreateDatabaseAlways<DataContext>();

            Database.SetInitializer(new DataContext.DemoDataInitializer(initializer));

            using (var context = new DataContext())
            {
                context.Database.Initialize(false);
            }
        }

        public static void CreateAdminUser()
        {
            using (var context = new DataContext())
            {
                if (!context.Users.Any(x => x.EmailAddress == "admin@ebuy.com"))
                {
                    var admin = new User { DisplayName = "Administrator", EmailAddress = "admin@ebuy.com", Username = "admin" };
                    context.Users.Add(admin);
                    context.SaveChanges();

                    MembershipCreateStatus status;
                    Membership.CreateUser(admin.Username, "Password!", admin.EmailAddress, null, null, true, null, out status);
                }
            }
        }
    }
}
