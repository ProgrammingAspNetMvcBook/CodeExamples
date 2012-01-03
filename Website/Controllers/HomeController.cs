using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ebuy.DataAccess;
using Ebuy.Website.Models;

namespace Ebuy.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var categories = _repository.All<Category>();

            var viewModel = new HomepageViewModel {
                    Categories = categories.Select(Mapper.DynamicMap<CategoryViewModel>),
                };

            return View("Homepage", viewModel);
        }
    }
}
