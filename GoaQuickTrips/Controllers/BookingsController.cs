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
using PagedList;

namespace GoaQuickTrips.Controllers
{
    [Authorize]
    public class BookingsController : EAController
    {   // GET: Bookings
        public ActionResult Index(int? page, int BkRefID =0)
        {   
            var bookings= db.Bookings.OrderByDescending(u=>u.BookDate).ThenByDescending(u => u.BookingID);

            if (BkRefID > 0)
                bookings = bookings.Where(b => b.BookingID == BkRefID).OrderByDescending(u => u.BookDate).ThenByDescending(u => u.BookingID);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            
            return View(bookings.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Confirm(int? id)
        {
            var cancel = db.Bookings.Find(id);
            cancel.StatusID = 2;
            db.Entry(cancel).Property(a=>a.StatusID).IsModified =true;
           
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public ActionResult Cancel(int? id)
        {
            var cancel = db.Bookings.Find(id);
            cancel.StatusID = 3;
            db.Entry(cancel).Property(a => a.StatusID).IsModified = true;

            db.SaveChanges();
            return RedirectToAction("Index");

        }


        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Status1");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,UserID,BookDate,StatusID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Status1", booking.StatusID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Status1", booking.StatusID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,UserID,BookDate,StatusID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Status1", booking.StatusID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
