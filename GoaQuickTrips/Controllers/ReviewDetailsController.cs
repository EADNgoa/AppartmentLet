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

namespace GoaQuickTrips.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ReviewDetailsController : EAController
    {
        // GET: ReviewDetails
        public ActionResult Index()
        {
            var reviewDetails = db.ReviewDetails; ;
            return View(reviewDetails.ToList());
        }

        // GET: ReviewDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewDetail reviewDetail = db.ReviewDetails.Find(id);
            if (reviewDetail == null)
            {
                return HttpNotFound();
            }
            return View(reviewDetail);
        }

        // GET: ReviewDetails/Create
        public ActionResult Create(int? id)
        {

            ViewBag.ReviewID = id;
            
            return View();
        }

        // POST: ReviewDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewDetailID,ReviewID,UserID,ReviewDate,Reply,ISvisible")] ReviewDetail reviewDetail)
        {
            var UserID = User.Identity.GetUserId();
            
            if (ModelState.IsValid)
            {
                reviewDetail.ReviewID = reviewDetail.ReviewID;
                reviewDetail.UserID = UserID;
                reviewDetail.ReviewDate = DateTime.Now;
                reviewDetail.ISvisible = true;
                db.ReviewDetails.Add(reviewDetail);
                db.SaveChanges();
                return RedirectToAction("Index","Reviews");
            }

            ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "UserID", reviewDetail.ReviewID);
            return View(reviewDetail);
        }

        // GET: ReviewDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewDetail reviewDetail = db.ReviewDetails.Find(id);
            if (reviewDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "UserID", reviewDetail.ReviewID);
            return View(reviewDetail);
        }

        // POST: ReviewDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewDetailID,ReviewID,UserID,ReviewDate,Reply,ISvisible")] ReviewDetail reviewDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "UserID", reviewDetail.ReviewID);
            return View(reviewDetail);
        }

        // GET: ReviewDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewDetail reviewDetail = db.ReviewDetails.Find(id);
            if (reviewDetail == null)
            {
                return HttpNotFound();
            }
            return View(reviewDetail);
        }

        // POST: ReviewDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReviewDetail reviewDetail = db.ReviewDetails.Find(id);
            db.ReviewDetails.Remove(reviewDetail);
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
