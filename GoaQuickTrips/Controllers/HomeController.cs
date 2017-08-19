using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GoaQuickTrips.Models;
using Microsoft.AspNet.Identity;
using PagedList;


namespace GoaQuickTrips.Controllers
{
    public class HomeController : Controller
    {
        private QuickTripsEntities db = new QuickTripsEntities();

        

         public ActionResult Index()
        {

            ViewBag.ReturnAction = "ApartmentsView";
            return View("Index");
        }
        [HttpPost]
        public ActionResult ApartmentsView(FormCollection fn,int? page)
        {
            string check_in = DateTime.Parse(fn["check_in"]).ToString("yyyy/MM/dd");
            string check_out = DateTime.Parse(fn["check_Out"]).ToString("yyyy/MM/dd");
            int guests = int.Parse(fn["guest"]);
        
            if ((guests != 0) && (check_in != null) && (check_out != null))
            {
                Session["in"] = check_in;
                Session["out"] = check_out;
                Session["Guests"] = guests;
            }
          
            var IN = DateTime.Parse(check_in);
            var OUT = DateTime.Parse(check_out);
            var bookings = db.BookingDetails.Where(i => i.CheckIn <= IN &&  i.CheckIn >= IN ||  i.CheckOut <= OUT && i.CheckOut >= IN).Select(id => id.ApartmentID);
            var apartments = db.Apartments.Where(a => a.NoOfGuests >= guests).Where(a => !bookings.Contains(a.ApartmentID)).OrderBy(a =>a.Name);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(apartments.ToPagedList(pageNumber, pageSize));

        }
 

        public ActionResult BookedCustomer(BookingViewModel model)
        {

            var UserID = User.Identity.GetUserId();

            //BookingViewModel bvm = new BookingViewModel();

            var bkings = db.Bookings.Where(b => b.UserID == UserID).ToList();
            //bvm.Bookingdata = bkings;


            //bkings.ForEach(bd => boo)

            //var bookedDetails =db.Bookings.Join(db.BookingDetails,b => b.BookingID,bd=> bd.BookingID,(b,bd) => new {b.BookingID,b.UserID,b.StatusID,bd.ApartmentID,bd.CheckIn,bd.CheckOut,bd.Price,bd.NoOfGuests }).Where(i=>i.UserID ==UserID).ToList();
            return View(bkings);
       
      }


        public ActionResult ApartmentDetail(int? id)
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

            
        public ActionResult AddToCart(int? id)
        {
            Session["id"] = id;
            var cartItem = db.Apartments.Find(id);
            var UserID = User.Identity.GetUserId();

           
            var IN = DateTime.Parse(Session["in"].ToString());
            var OUT = DateTime.Parse(Session["out"].ToString());
            var item = new Cart { UserID = UserID, ApartmentID = cartItem.ApartmentID, CheckIn = IN, CheckOut=OUT, NoOfGuests=(int)Session["guests"],OrigPrice=null };
            db.Carts.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index", "Carts");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}