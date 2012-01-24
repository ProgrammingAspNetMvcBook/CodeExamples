using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ebuy.DataAccess;
using Ebuy.Website.Models;

namespace Ebuy.Website.ActionFilters
{
    public class CategoriesActionFilter : IActionFilter
    {
        private readonly Func<IRepository> _repositoryFactory;

        // Default constructor for ease of initialization
        public CategoriesActionFilter()
            : this(() => DependencyResolver.Current.GetService<IRepository>())
        {
        }

        // Constructor injection for testing purposes
        public CategoriesActionFilter(Func<IRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var viewBag = filterContext.Controller.ViewBag;

            // If the Categories have already been loaded, don't load them again
            if (viewBag.Categories != null)
                return;

            // Get all top-level categories
            var categories = _repositoryFactory().Query<Category>(cat => cat.ParentId == null);

            viewBag.Categories = categories.Select(Mapper.DynamicMap<CategoryViewModel>).ToArray();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Nothing
        }
    }
}