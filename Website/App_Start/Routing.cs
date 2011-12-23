using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using CustomExtensions.Routing;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Routing), "InitializeRouting")]

namespace Ebuy.Website.App_Start
{
    public static class Routing
    {
        public static void InitializeRouting()
        {
            RegisterRoutes(RouteTable.Routes);
            RegisterRouteAttributes();
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        private static void RegisterRouteAttributes()
        {
            var routeGenerators = DependencyResolver.Current.GetServices<IRouteGenerator>();
            var routes = routeGenerators.SelectMany(x => x.Generate()).ToArray();

            foreach (var route in routes)
            {
                RouteTable.Routes.Insert(0, route);
            }
        }
    }
}