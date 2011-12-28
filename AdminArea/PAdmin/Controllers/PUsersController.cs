using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ebuy.DataAccess;
using AutoMapper;
using Ebuy.AdminArea.Admin.Models;

namespace Ebuy.AdminArea.Admin.Controllers
{
    public class PUsersController : Controller
    {
        private readonly IRepository _repository;

        public PUsersController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int page = 0, int pageSize = 25)
        {
            var users = _repository.All<User>(page, pageSize);

            var viewModel = users.Select(Mapper.DynamicMap<PUserViewModel>);

            return View("PUsers", viewModel);
        }

        public new ActionResult User(string key)
        {
            var auction = _repository.Single<Auction>(key);

            if (auction == null)
                return View("NotFound");

            var viewModel = Mapper.DynamicMap<PUserViewModel>(auction);
            return View("PUser", viewModel);
        }

    }
}
