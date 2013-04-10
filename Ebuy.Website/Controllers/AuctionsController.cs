using System;
using System.Linq;
using System.Web.Mvc;
using Ebuy.DataAccess;

namespace Ebuy.Website.Controllers
{
    public class AuctionsController : Controller
    {
        [MultipleResponseFormats]
        public ActionResult Index(int page = 0, int size = 25)
        {
            var db = new EbuyDataContext();
            var auctions = db.Auctions.OrderByDescending(x => x.EndTime).Skip(page * 25).Take(size);
            return View("Auctions", auctions.ToArray());
        }

        [MultipleResponseFormats]
        public ActionResult Auction(long id)
        {
            var db = new EbuyDataContext();
            var auction = db.Auctions.Find(id);

            // The following moved to MultipleResponseFormatsAttribute:
            /*
            // Respond to AJAX requests
            if (Request.IsAjaxRequest())
                return PartialView("Auction", auction);

            // Respond to JSON requests
            if (Request.IsJsonRequest())
                return Json(auction);
            */

            // Default to a "normal" view with layout
            return View("Auction", auction);
        }

        public ActionResult JsonAuction(long id)
        {
            var db = new EbuyDataContext();
            var auction = db.Auctions.Find(id);
            return Json(auction, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialAuction(long id)
        {
            var db = new EbuyDataContext();
            var auction = db.Auctions.Find(id);
            return PartialView("Auction", auction);
        }

        //
        // GET: /Auctions/Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Auctions/Create

        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            if (ModelState.IsValid)
            {
                var db = new EbuyDataContext();
				auction.CurrentPrice = auction.StartPrice;
                db.Auctions.Add(auction);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = auction.Id });
            }

            return View(auction);
        }


		//
		// GET: /Test/Details/{guid}

		public ActionResult Details(Guid id)
		{
			var db = new EbuyDataContext();
			var auction = db.Auctions.Find(id);
			if (auction == null)
			{
				return HttpNotFound();
			}
			return View(auction);
		}


        //
        // GET: /Auctions/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Auctions/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Auctions/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Auctions/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
