using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace Website.Areas.Admin
{
public class AdminAreaRegistration : PortableAreaRegistration
{
    public override string AreaName
    {
        get
        {
            return "Admin";
        }
    }

    public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
    {            
        context.MapRoute(
            "Admin_default",
            "Admin/{controller}/{action}/{id}",
            new { action = "Index", id = UrlParameter.Optional }
        );
    }
}
}
