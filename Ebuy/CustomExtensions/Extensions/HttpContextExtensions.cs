using System.Web;

namespace Ebuy.Web.Extensions
{
    public static class HttpContextExtensions
    {

        public static User CurrentUser(this HttpContextBase context)
        {
            return context.Items["CurrentUser"] as User;
        }

        public static void CurrentUser(this HttpContextBase context, User user)
        {
            context.Items["CurrentUser"] = user;
        }

    }
}
