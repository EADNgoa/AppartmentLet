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
        
        public ActionResult ApartmentsView(FormCollection fn,int? page)
        {
            DateTime IN, OUT;
            int guests;
            if (fn["check_in"] != null)
            {
                string check_in = DateTime.Parse(fn["check_in"]).ToString("yyyy/MM/dd");
                string check_out = DateTime.Parse(fn["check_Out"]).ToString("yyyy/MM/dd");
                guests = int.Parse(fn["guest"]);

                if ((guests != 0) && (check_in != null) && (check_out != null))
                {
                    Session["in"] = check_in;
                    Session["out"] = check_out;
                    Session["Guests"] = guests;
                }

                IN = DateTime.Parse(check_in);
                OUT = DateTime.Parse(check_out);
            }
            else if (Session["in"] != null)
            {
                IN = DateTime.Parse(Session["in"].ToString());
                OUT = DateTime.Parse(Session["out"].ToString());
                guests = int.Parse(Session["Guests"].ToString());
            }
            else
                return RedirectToAction("Index");

            var bookings = db.BookingDetails.Where(i => i.CheckIn <= OUT &&  i.CheckIn >= IN ||  i.CheckOut <= OUT && i.CheckOut >= IN).Select(id => id.ApartmentID);
            var apartments = db.Apartments.Where(a => a.NoOfGuests >= guests).Where(a => !bookings.Contains(a.ApartmentID)).OrderBy(a =>a.Name);
            ViewBag.ReturnAction = "ApartmentsView";
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            string ens = "";
            var PagedApts = apartments.ToPagedList(pageNumber, pageSize);
            foreach (var item in PagedApts)            
                ens += $"{item.Name}&&&{item.Lat}&&&{item.Lang}****";
            
            ViewBag.encodedString = ens.Substring(0,ens.Length-4);
            
            return View(PagedApts);

        }
 

        public ActionResult BookedCustomer()
        {

            var UserID = User.Identity.GetUserId();

            //BookingViewModel bvm = new BookingViewModel();

            var bkings = db.Bookings.Where(b => b.UserID == UserID).OrderByDescending(b =>b.BookDate).ToList();
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

            if (Session["in"] == null)
                return RedirectToAction("Index");


            Session["AptPrice"] = apartment.Prices.OrderByDescending(p => p.WEF).FirstOrDefault(p => (DateTime)p.WEF <= DateTime.Parse(Session["in"].ToString())).Price1;

            return View(apartment);
        }

            
        public ActionResult AddToCart(int? id)
        {
            Session["id"] = id;
            var cartItem = db.Apartments.Find(id);
            var UserID = User.Identity.GetUserId();

           
            var IN = DateTime.Parse(Session["in"].ToString());
            var OUT = DateTime.Parse(Session["out"].ToString());
            var item = new Cart { UserID = UserID, ApartmentID = cartItem.ApartmentID, CheckIn = IN, CheckOut=OUT, NoOfGuests=(int)Session["guests"],OrigPrice= (decimal)Session["AptPrice"] };
            db.Carts.Add(item);
           
            db.SaveChanges();
            Session["cartid"] = item.CartID;
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