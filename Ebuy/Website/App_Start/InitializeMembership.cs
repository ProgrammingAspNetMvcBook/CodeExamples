using System.Data.SqlClient;
using System.Linq;
using System.Web.Management;
using System.Web.Security;
using Ebuy.DataAccess;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(InitializeMembership), "CreateAdminUser")]

namespace Ebuy.Website.App_Start
{
    public static class InitializeMembership
    {
        public static void CreateAdminUser()
        {
            using (var context = new DataContext())
            {
                var admin = new User
                                {
                                    DisplayName = "Administrator",
                                    EmailAddress = "admin@ebuy.com",
                                    Username = "admin",
                                };

                if (!context.Users.Any(x => x.Key == admin.Key))
                {
                    context.Users.Add(admin);
                    context.SaveChanges();

                    var conn = new SqlConnectionStringBuilder(context.Database.Connection.ConnectionString);
                    SqlServices.Install(conn.InitialCatalog, SqlFeatures.Membership | SqlFeatures.RoleManager, conn.ConnectionString);

                    MembershipCreateStatus status;
                    Membership.CreateUser(admin.Username, "Password!", admin.EmailAddress, null, null, true, null, out status);
                }
            }
        }
    }
}