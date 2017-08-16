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

      
        public ActionResult AddAmenity(int id)
        {
            ViewBag.ApartmentID = id;
            ViewBag.ReturnAction = "LoadAmenity";
            return View();
        }

        public ActionResult LoadAmenity(FormCollection fm)
        {
            int amenity =int.Parse( fm["Amenity"]);
            int apartmentid = int.Parse(fm["ApartmentID"]);
           // var amenityid = db.MasterAmenities.Where(a => a.Amenity == amenity).Select(i => i.MasterID).FirstOrDefault();
            var item = new Amenities_Apartments { ApartmentID = apartmentid, AmenityID =amenity };
            db.Amenities_Apartments.Add(item);
            db.SaveChanges();

            return View("AddAmenity");  
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
