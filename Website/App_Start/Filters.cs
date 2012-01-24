using System.Web.Mvc;
using Ebuy.Website.ActionFilters;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Filters), "Start")]

namespace Ebuy.Website.App_Start
{
    public static class Filters
    {
        public static void Start()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CategoriesActionFilter());
        }
    }
}