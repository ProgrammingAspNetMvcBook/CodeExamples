using System;
using System.Linq;
using System.Web.Mvc;
using Ebuy.Website.Models;

namespace Ebuy.Website.Controllers
{
    public class AuctionsController : Controller
    {
        [MultipleResponseFormats]
        public ActionResult Index(int page = 0, int size = 25)
        {
            var db = new EbuyDataContext();
            var auctions = db.Auctions.OrderByDescending(x => x.EndTime).Skip(page * 25).Take(size);
            return View("Auctions", auctions);
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

        public ActionResult Details(long id = 0)
        {
            var auction = new Ebuy.Website.Models.Auction
            {
                Id = id,
                Title = "Brand new Widget 2.0",
                Description = "This is a brand new version 2.0 Widget!",
                StartPrice = 1.00m,
                CurrentPrice = 13.40m,
                EndTime = DateTime.Parse("6-23-2012 12:34 PM"),
            };

            return View(auction);
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
                db.Auctions.Add(auction);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = auction.Id });
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
