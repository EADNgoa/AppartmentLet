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
    public class ImagesController : Controller
    {
        private QuickTripsEntities db = new QuickTripsEntities();

        // GET: Images
        public ActionResult Index(int? id)
        {
            var images = db.Images.Include(i => i.Apartment).Where(a=>a.ApartmentID == id);
            ViewBag.ApartmentID = id;
            return View(images.ToList());
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Images/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ApartmentID = id;
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageId,ApartmentID,Name,UploadedFile")] AptImg image)
        {
            if (ModelState.IsValid)
            {
                if (image.UploadedFile != null)
                {
                    string fn = image.UploadedFile.FileName.Substring(image.UploadedFile.FileName.LastIndexOf('\\') + 1);
                    fn = image.ApartmentID + "_" + fn;
                    string SavePath = System.IO.Path.Combine(Server.MapPath("~/Images"), fn);
                    image.UploadedFile.SaveAs(SavePath);

                    //System.Drawing.Bitmap upimg = new System.Drawing.Bitmap(image.UploadedFile.InputStream);
                    //System.Drawing.Bitmap svimg = MyExtensions.CropUnwantedBackground(upimg);
                    //svimg.Save(System.IO.Path.Combine(Server.MapPath("~/Images"), fn));

                    Image img = new Image
                    {
                        ApartmentID = image.ApartmentID,
                        Name = image.Name,
                        Path = fn

                    };

                    db.Images.Add(img);
                    db.SaveChanges();
                    return RedirectToAction("Create", new { ApartmentID = image.ApartmentID });
                }
            }

            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", image.ApartmentID);
            return View(image);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", image.ApartmentID);
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageId,ApartmentID,Name,Path")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", image.ApartmentID);
            return View(image);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            int AptId = (int)image.ApartmentID;
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index", new { id =AptId });
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
