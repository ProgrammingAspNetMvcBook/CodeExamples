using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ebuy.DataAccess;
using AutoMapper;
using Ebuy.Website.Areas.Admin.Models;

namespace Ebuy.Website.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository _repository;

        public UsersController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int page = 0, int pageSize = 25)
        {
            var users = _repository.All<User>(page, pageSize);

            var viewModel = users.Select(Mapper.DynamicMap<UserViewModel>);

            return View("Users", viewModel);
        }

        public new ActionResult User(string key)
        {
            var auction = _repository.Single<Auction>(key);

            if (auction == null)
                return View("NotFound");

            var viewModel = Mapper.DynamicMap<UserViewModel>(auction);
            return View("User", viewModel);
        }

    }
}
