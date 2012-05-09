using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace Ebuy
{
    public static class ControllerExtensions
    {
        public static IEnumerable<T> ApplyPaging<T>(this Controller controller, IEnumerable<T> source, int defaultPageSize = 25)
        {
            Contract.Requires(controller != null, "Controller cannot be negative");
            Contract.Requires(controller.RouteData != null, "Controller RouteData cannot be negative");
            
            var routeData = controller.RouteData;

            int pageIndex = (routeData.DataTokens["pageIndex"] as int?).GetValueOrDefault(0);
            int pageSize = (routeData.DataTokens["pageSize"] as int?).GetValueOrDefault(defaultPageSize);

            return source.Page(pageIndex, pageSize);
        }
    }
}
