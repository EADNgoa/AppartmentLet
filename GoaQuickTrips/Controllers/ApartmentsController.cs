using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoaQuickTrips;
using Microsoft.AspNet.Identity;

namespace QuickTrips.Areas.AdminSection.Controllers
{   [Authorize(Roles = "ADMIN")]
    public class ApartmentsController : Controller
    {
        private QuickTripsEntities db = new QuickTripsEntities();



        // GET: AdminSection/Apartments
        
        public ActionResult Index()
        {
            var apartments = db.Apartments.Include(a => a.Amenity);
            return View(apartments.ToList());
        }

        public ActionResult BlockApartments()
        {

            var apartments = db.Apartments.Include(a => a.Amenity);
            return View(apartments.ToList());
        }

      
        public ActionResult Blocked(int? id)
        {
            var UserID = User.Identity.GetUserId();
            var apartments = db.Apartments.Find(id);

            var item1 = new Booking { UserID = UserID, BookDate = DateTime.Now, StatusID = null };
            db.Bookings.Add(item1);
            db.SaveChanges();
        
            var item2 = new BookingDetail { BookingID = item1.BookingID, ApartmentID = apartments.ApartmentID, CheckIn = DateTime.Parse(Session["in"].ToString()), CheckOut = DateTime.Parse(Session["out"].ToString()), Price = null, BlockedReason = null };
            db.BookingDetails.Add(item2);
            db.SaveChanges();
            return RedirectToAction("BlockApartments");
        }

  

        // GET: AdminSection/Apartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // GET: AdminSection/Apartments/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentID = new SelectList(db.Amenities, "AmenityID", "Amenity1");
            return View();
        }

        // POST: AdminSection/Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentID,Name,Description,Rules,Notes,NoOfGuests,Address,Email,Phone,LocationInfo,CancellationPolicy,Lat,Lang")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Apartments.Add(apartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentID = new SelectList(db.Amenities, "AmenityID", "Amenity1", apartment.ApartmentID);
            return View(apartment);
        }

        // GET: AdminSection/Apartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Amenities, "AmenityID", "Amenity1", apartment.ApartmentID);
            return View(apartment);
        }

        // POST: AdminSection/Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentID,Name,Description,Rules,Notes,NoOfGuests,Address,Email,Phone,LocationInfo,CancellationPolicy,Lat,Lang")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Amenities, "AmenityID", "Amenity1", apartment.ApartmentID);
            return View(apartment);
        }

        // GET: AdminSection/Apartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // POST: AdminSection/Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apartment apartment = db.Apartments.Find(id);
            db.Apartments.Remove(apartment);
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
