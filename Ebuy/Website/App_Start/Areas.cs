using System.Web.Mvc;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Areas), "Start")]

namespace Ebuy.Website.App_Start
{
    public static class Areas
    {
        public static void Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}