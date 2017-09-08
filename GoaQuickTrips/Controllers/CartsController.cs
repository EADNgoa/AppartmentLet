using GoaQuickTrips;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GoaQuickTrips.Controllers
{
    [Authorize]
    public class CartsController : EAController
    {   
        // GET: Carts
        public ActionResult Index()
        {
            var UserID = User.Identity.GetUserId();

            //if (Session["id"] != null)
            //{
            //    int id = (int)Session["id"];

            //    var cartItem = db.Apartments.Find(id);


            //    var IN = DateTime.Parse(Session["in"].ToString());
            //    var OUT = DateTime.Parse(Session["out"].ToString());
            //    var item = new Cart { UserID = UserID, ApartmentID = cartItem.ApartmentID, CheckIn = IN, CheckOut = OUT, NoOfGuests = (int)Session["guests"], OrigPrice = (decimal)Session["AptPrice"] };
            //    db.Carts.Add(item);
            //    db.SaveChanges();
            //}
            var carts = db.Carts.Include(c => c.Apartment).Where(u =>u.UserID == UserID);
            return View(carts.ToList());
        }

        //public ActionResult BookItems()
        //{
        //    var UserID = User.Identity.GetUserId();
        //    var bookings = db.Carts.Where(u => u.UserID == UserID);
        //    foreach (var b in bookings.ToList())
        //    {
        //        var AlreadyBook = db.BookingDetails.Where(i => i.CheckIn <= b.CheckIn && i.CheckIn >= b.CheckIn || i.CheckOut <= b.CheckOut && i.CheckOut >= b.CheckIn && i.ApartmentID == b.ApartmentID);
        //        && i.Booking.StatusID != 3
        //        if (AlreadyBook.Count() == 0)
        //        {
        //            var item1 = new Booking { UserID = UserID, BookDate = DateTime.Now, StatusID = null };
        //            db.Bookings.Add(item1);
        //            db.SaveChanges();
        //            foreach (var item in bookings)
        //            {
        //                var item2 = new BookingDetail { BookingID = item1.BookingID, ApartmentID = item.ApartmentID, CheckIn = item.CheckIn, CheckOut = item.CheckOut, NoOfGuests = item.NoOfGuests, Price = null, BlockedReason = null };
        //                db.BookingDetails.Add(item2);
        //            }
        //            db.SaveChanges();
        //            db.Carts.Remove(b);
        //            db.SaveChanges();
        //        }   
        //    }

        //    return RedirectToAction("Index");
        //}

        public ActionResult AddCustomer()
        {
            var UserID = User.Identity.GetUserId();
            var crt = db.Carts.Where(a => a.UserID == UserID).Max(i => i.NoOfGuests);
            Session["MAXguests"] =(int) crt;
            return View(crt);
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
  
        public ActionResult AddBookedCustomer(FormCollection fm, [Bind(Include = "ApartmentID,UploadedFile")] IdPicture image)
        {
            var UserID = User.Identity.GetUserId();
            var cartItem = db.Carts.Where(u => u.UserID == UserID);
            foreach (var c in cartItem.ToList())
            {
                var AlreadyBook = db.BookingDetails.Where(i => c.CheckIn <= i.CheckOut && c.CheckOut >= i.CheckIn && i.Booking.StatusID != 3 && i.ApartmentID==c.ApartmentID);
                
                if (AlreadyBook.Count() == 0)
                {
                    var item1 = new Booking { UserID = UserID, BookDate = DateTime.Now, StatusID = 1 };
                    db.Bookings.Add(item1);

                    db.SaveChanges();
                    Session["bookingid"] = item1.BookingID;
                    foreach (var item in cartItem)
                    {
                        var item2 = new BookingDetail { BookingID = item1.BookingID, ApartmentID = item.ApartmentID, CheckIn = item.CheckIn, CheckOut = item.CheckOut, NoOfGuests = item.NoOfGuests, Price = item.OrigPrice, BlockedReason = null };
                        db.BookingDetails.Add(item2);
                    }
                    db.SaveChanges();
                    var crt = db.Carts.Where(a => a.UserID == UserID).Max(i => i.NoOfGuests);
                    for (int i = 0; i < crt; i++)
                    {
                        var fname = fm["fname" + i];
                        var sname = fm["sname" + i];
                        var email = fm["email" + i];
                        var phone = fm["phone" + i];

                        var item3 = new Customer { FName = fname, SName = sname, Email = email, Phone = phone, IDpicture = null };
                        db.Customers.Add(item3);
                        db.SaveChanges();
                        var item4 = new BookedCustomer { CartID = cartItem.FirstOrDefault().CartID, BookingID = (int)Session["bookingid"], CustomerID = item3.CustomerID };
                        db.BookedCustomers.Add(item4);
                        db.SaveChanges();

                    }

                    db.Carts.Remove(c);
                    db.SaveChanges();
                }
                else
                    throw new System.InvalidOperationException("Oops! someone else has just booked this apartment. Dont worry we have plenty of others nearby. Please search again.");
                    
            }
           
          

            return RedirectToAction("BookedCustomer","Home");
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
