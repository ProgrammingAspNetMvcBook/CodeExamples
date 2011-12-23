using System.Web.Mvc;
using Ebuy.Web.ModelBinding;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(ModelBinding), "Initialize")]

namespace Ebuy.Website.App_Start
{
    public static class ModelBinding
    {
        public static void Initialize()
        {
            ModelBinderProviders.BinderProviders.Add(
                new DependencyResolverModelBinderProvider<UserModelBinder>(type => typeof(User).IsAssignableFrom(type)));
        }
    }
}