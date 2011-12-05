using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using CustomExtensions.Controllers;

namespace CustomExtensions.Routing
{
    public interface IRouteGenerator
    {
        IEnumerable<RouteBase> Generate();
    }

    public class RouteGenerator : IRouteGenerator
    {
        private readonly RouteCollection _routes;
        private readonly RequestContext _requestContext;
        private readonly JavaScriptSerializer _javaScriptSerializer;
        private readonly ControllerActions _controllerActions;

        public RouteGenerator(
                RouteCollection routes, RequestContext requestContext, 
                ControllerActions controllerActions
            )
        {
            Contract.Requires(routes != null);
            Contract.Requires(requestContext != null);
            Contract.Requires(controllerActions != null);

            _routes = routes;
            _controllerActions = controllerActions;
            _requestContext = requestContext;

            _javaScriptSerializer = new JavaScriptSerializer();
        }


        public virtual IEnumerable<RouteBase> Generate()
        {
            IEnumerable<Route> customRoutes =
                from controllerAction in _controllerActions
                from attribute in controllerAction.Attributes.OfType<RouteAttribute>()
                let defaults = GetDefaults(controllerAction, attribute)
                let constraints = GetConstraints(attribute)
                let routeUrl = ResolveRoute(attribute, defaults)
                select new Route(routeUrl, defaults, constraints, new MvcRouteHandler());

            return customRoutes;
        }

        private RouteValueDictionary GetDefaults(ControllerAction controllerAction, RouteAttribute attribute)
        {
            var routeDefaults = new RouteValueDictionary(new {
                controller = controllerAction.ControllerShortName,
                action = controllerAction.Action.Name,
            });

            if (string.IsNullOrWhiteSpace(attribute.Defaults) == false)
            {
                var attributeDefaults =
                    _javaScriptSerializer.Deserialize<IDictionary<string, object>>(attribute.Defaults);

                foreach (var key in attributeDefaults.Keys)
                {
                    routeDefaults[key] = attributeDefaults[key];
                }
            }

            return routeDefaults;
        }

        private RouteValueDictionary GetConstraints(RouteAttribute attribute)
        {
            var constraints = _javaScriptSerializer.Deserialize<IDictionary<string, object>>(attribute.Constraints ?? string.Empty);
            return new RouteValueDictionary(constraints ?? new object());
        }

        private string ResolveRoute(RouteAttribute attribute, RouteValueDictionary defaults)
        {
            // An explict URL trumps everything
            string routeUrl = attribute.Pattern;

            // If one doesn't exist, try to figure it out
            if (string.IsNullOrEmpty(routeUrl))
                routeUrl = _routes.GetVirtualPath(_requestContext, defaults).VirtualPath;

            if ((routeUrl ?? string.Empty).StartsWith("/"))
                routeUrl = routeUrl.Substring(1);

            return routeUrl;
        }

    }
}
