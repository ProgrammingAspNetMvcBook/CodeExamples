using System.Data.Entity;
using Ebuy.DataAccess;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DataAccess), "Start")]

namespace Ebuy.Website.App_Start
{
    public static class DataAccess
    {
        public static void Start() 
        {
            Database.SetInitializer(new DataContext.Initializer());
        }
    }
}
