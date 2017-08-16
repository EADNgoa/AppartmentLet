using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoaQuickTrips;

namespace GoaQuickTrips.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class BookingDetailsController : Controller
    {
        private QuickTripsEntities db = new QuickTripsEntities();

        // GET: BookingDetails
        public ActionResult Index()
        {
            return View(db.BookingDetails.ToList());
        }
        public ActionResult Confirm(int? id)
        {
            var cancel = db.Bookings.Find(id);
            cancel.StatusID = 2;
            db.Entry(cancel).Property(a => a.StatusID).IsModified = true;

            db.SaveChanges();
            return RedirectToAction("Index");

        }


        public ActionResult BlockList()
        {
            var BlockedList = db.BookingDetails.Where(a => a.BlockedReason != null);
            return View(BlockedList.ToList());
        }


        // GET: BookingDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = db.BookingDetails.Find(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // GET: BookingDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingDetailID,BookingID,ApartmentID,CheckIn,CheckOut,NoOfGuests,Price,BlockedReason")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                db.BookingDetails.Add(bookingDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingDetail);
        }

        // GET: BookingDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = db.BookingDetails.Find(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // POST: BookingDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingDetailID,BookingID,ApartmentID,CheckIn,CheckOut,NoOfGuests,Price,BlockedReason")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingDetail);
        }

        // GET: BookingDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDetail bookingDetail = db.BookingDetails.Find(id);
            if (bookingDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetail);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingDetail bookingDetail = db.BookingDetails.Find(id);
            db.BookingDetails.Remove(bookingDetail);
            db.SaveChanges();
            return RedirectToAction("BlockList");
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
