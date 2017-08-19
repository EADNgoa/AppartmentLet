using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoaQuickTrips.Models;

namespace GoaQuickTrips.Controllers
{
    public class MasterAmenitiesController : Controller
    {
        private QuickTripsEntities db = new QuickTripsEntities();

        // GET: MasterAmenities
        public ActionResult Index()
        {
            return View(db.MasterAmenities.ToList());
        }
        public ActionResult AutoCompleteAmenity(string term)
        {
            var filteredItems = db.MasterAmenities.Where(c => c.Amenity.Contains(term)).Select(c => new { id = c.MasterID, value = c.Amenity });

            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignedAmenity(AmenityViewModel amenity)
        {
            


            return View();
        }

      
        public ActionResult AddAmenity(int? id)
        {
            ViewBag.ApartmentID = id;

            var apt = db.Apartments.Find(id);

            ViewBag.ReturnAction = "LoadAmenity";
            ViewBag.Amenity = apt.MasterAmenities;

            return View();
        }

        public ActionResult LoadAmenity(FormCollection fm, int? id)
        {
            int amenity =int.Parse( fm["Amenity"]);
            int apartmentid = int.Parse(fm["ApartmentID"]);
            ViewBag.ApartmentID = apartmentid;
            
            var appt= db.Apartments.Find(apartmentid);
            var amty = new MasterAmenity { MasterID = amenity };
            db.MasterAmenities.Attach(amty);
            appt.MasterAmenities.Add(amty);
            
            int res = db.SaveChanges();

            return  RedirectToAction("AddAmenity",new{ id = apartmentid});  
        }




        // GET: MasterAmenities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterAmenity masterAmenity = db.MasterAmenities.Find(id);
            if (masterAmenity == null)
            {
                return HttpNotFound();
            }
            return View(masterAmenity);
        }

        // GET: MasterAmenities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterAmenities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MasterID,Amenity")] MasterAmenity masterAmenity)
        {
            if (ModelState.IsValid)
            {
                db.MasterAmenities.Add(masterAmenity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterAmenity);
        }

        // GET: MasterAmenities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterAmenity masterAmenity = db.MasterAmenities.Find(id);
            if (masterAmenity == null)
            {
                return HttpNotFound();
            }
            return View(masterAmenity);
        }

        // POST: MasterAmenities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MasterID,Amenity")] MasterAmenity masterAmenity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterAmenity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterAmenity);
        }

        // GET: MasterAmenities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterAmenity masterAmenity = db.MasterAmenities.Find(id);
            if (masterAmenity == null)
            {
                return HttpNotFound();
            }
            return View(masterAmenity);
        }

        // POST: MasterAmenities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterAmenity masterAmenity = db.MasterAmenities.Find(id);
            db.MasterAmenities.Remove(masterAmenity);
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
