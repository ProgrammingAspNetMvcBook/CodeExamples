using System.Web.Mvc;
using Ebuy.Website.App_Start;
using MvcContrib.PortableAreas;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Ebuy.Website.App_Start.ViewEngines), "Start")]

namespace Ebuy.Website.App_Start
{
    public static class ViewEngines
    {
        public static void Start()
        {
            //PortableAreaRegistration.RegisterEmbeddedViewEngine();
        }
    }
}