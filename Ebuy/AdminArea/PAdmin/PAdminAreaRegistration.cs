using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace Ebuy.AdminArea.Admin
{
    public class PAdminAreaRegistration : PortableAreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            this.RegisterAreaEmbeddedResources();

            //context.MapRoute(
            //    "resources",
            //    "PAdmin/Resource/{resourceName}",
            //    new { Controller = "EmbeddedResource", action = "Index" },
            //    new string[] { "MvcContrib.PortableAreas" });

            //base.RegisterArea(context, bus);

            context.MapRoute(
                "PAdmin_default",
                "PAdmin/{controller}/{action}/{id}",
                new { controller = "PUsers", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
