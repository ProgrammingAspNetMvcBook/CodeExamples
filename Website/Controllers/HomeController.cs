using System.Web.Mvc;

namespace Ebuy.Website.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Auctions");
        }

    }
}
