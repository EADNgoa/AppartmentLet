using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoaQuickTrips;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Web.UI;

namespace QuickTrips.Controllers
{
    public class CartsController : Controller
    {
        private QuickTripsEntities db = new QuickTripsEntities();

        // GET: Carts
        public ActionResult Index()
        {
            var UserID = User.Identity.GetUserId();
            var carts = db.Carts.Include(c => c.Apartment).Where(u =>u.UserID == UserID);
            return View(carts.ToList());
        }

        public ActionResult BookItems()
        {
            var UserID = User.Identity.GetUserId();
            var bookings = db.Carts.Where(u => u.UserID == UserID);
            foreach (var b in bookings.ToList())
            {
                var AlreadyBook = db.BookingDetails.Where(i => i.CheckIn <= b.CheckIn && i.CheckIn >= b.CheckIn || i.CheckOut <= b.CheckOut && i.CheckOut >= b.CheckIn && i.ApartmentID == b.ApartmentID);

                if (AlreadyBook.Count() == 0)
                {
                    var item1 = new Booking { UserID = UserID, BookDate = DateTime.Now, StatusID = null };
                    db.Bookings.Add(item1);
                    db.SaveChanges();
                    foreach (var item in bookings)
                    {
                        var item2 = new BookingDetail { BookingID = item1.BookingID, ApartmentID = item.ApartmentID, CheckIn = item.CheckIn, CheckOut = item.CheckOut, NoOfGuests = item.NoOfGuests, Price = null, BlockedReason = null };
                        db.BookingDetails.Add(item2);
                    }
                    db.SaveChanges();
                    db.Carts.Remove(b);
                    db.SaveChanges();
                }   
            }
            return RedirectToAction("Index");
        }

        public ActionResult Payment()
        {
            return View();
        }
      

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartID,UserID,ApartmentID,CheckIn,CheckOut,NoOfGuests,OrigPrice")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", cart.ApartmentID);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", cart.ApartmentID);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CartID,UserID,ApartmentID,CheckIn,CheckOut,NoOfGuests,OrigPrice")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", cart.ApartmentID);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
