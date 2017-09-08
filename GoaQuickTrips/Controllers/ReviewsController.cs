using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace GoaQuickTrips.Controllers
{
    [Authorize]
    public class ReviewsController : EAController
    {
        // GET: Reviews
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index(int? page)
        {
            var reviews = db.Reviews.OrderByDescending(r=>r.ReviewDate);

            int pageSize = 30;
            int pageNumber = (page ?? 1);
            
            return View(reviews.ToPagedList(pageNumber, pageSize));
        }


        // GET: Reviews/Details/5
        [Authorize(Roles = "ADMIN,USER")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ApartmentID = id;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,UserID,ApartmentID,ReviewDate,Review1,IsVisible")] Review review)
        {
            var UserID = User.Identity.GetUserId();

            if (ModelState.IsValid && UserID != null)
            {
                var hasStayed = db.Bookings.Where(bc => bc.UserID == UserID && bc.StatusID == 2);
                var KnowsResort = hasStayed.Where(s => s.BookingDetails.Any(d => d.ApartmentID == review.ApartmentID));
                if (KnowsResort != null)
                {
                    ViewBag.ApartmentID = review.ApartmentID;
                    review.UserID = UserID;
                    review.ReviewDate = DateTime.Now;
                    review.IsVisible = true;
                    db.Reviews.Add(review);
                    db.SaveChanges();
                    return RedirectToAction("BookedCustomer", "Home");
                }
            }

          
            return RedirectToAction("BookedCustomer", "Home");
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", review.ApartmentID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,UserID,ApartmentID,ReviewDate,Review1,IsVisible")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", review.ApartmentID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
