using System.Web.Mvc;
using System.Web.Routing;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Routes), "Start")]

namespace Ebuy.Website.App_Start
{
    public static class Routes
    {
        public static void Start()
        {
            Routes.RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Auction",
                "Auctions/{id}/{title}",
                new { controller = "Auctions", action = "Auction" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
    }
}