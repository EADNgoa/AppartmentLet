using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using GoaQuickTrips.Models;
using Microsoft.AspNet.Identity;
using PagedList;


namespace GoaQuickTrips.Controllers
{
    public class HomeController : EAController
    {   
         public ActionResult Index()
        {

            ViewBag.ReturnAction = "ApartmentsView";
            var GetApartments = db.Apartments;
            return View("Index",GetApartments);
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

            var bookings = db.BookingDetails.Where(i => IN <= i.CheckOut && OUT >= i.CheckIn && i.Booking.StatusID != 3).Select(id => id.ApartmentID);
            var apartments = db.Apartments.Where(a => a.NoOfGuests >= guests).Where(a => !bookings.Contains(a.ApartmentID)).OrderBy(a =>a.Name);
            ViewBag.ReturnAction = "ApartmentsView";
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var PagedApts = apartments.ToPagedList(pageNumber, pageSize);
            string ens = "";
            foreach (var item in PagedApts)            
                ens += $"{item.Name}&&&{item.Lat}&&&{item.Lang}****";
            
            ViewBag.encodedString = (ens.Length>4) ? ens.Substring(0,ens.Length-4):"";
            
            return View(PagedApts);

        }
 
        [Authorize]
        public ActionResult BookedCustomer(int? page)
        {

            var UserID = User.Identity.GetUserId();

            //show all records for this user (but if admin dont show the blocked records as taht should be seen only in the admin section)
            var bkings = db.Bookings.Where(b => b.UserID == UserID && b.StatusID != 4).OrderByDescending(b =>b.BookDate);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(bkings.ToPagedList(pageNumber, pageSize));
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

            


            Session["AptPrice"] = apartment.Prices.OrderByDescending(p => p.WEF).FirstOrDefault(p => (DateTime)p.WEF <= DateTime.Now).Price1;
            
            return View(apartment);
        }
        public ActionResult CustomerForm(int? id)
        {
            if (id != null)
            {
                ViewBag.AptID = id;
                ViewBag.AptName = db.Apartments.Find(id).Name;
            }
            return View("Customer");
        }
        [HttpPost]
        public ActionResult CustomerForm([Bind(Include = "FName,SName,Email,Address,Phone,ApartmentID,RequestStr")] Customer cust)
        {
            string GetApartmentName = " ";
            if (cust.ApartmentID != null)
            {
                 GetApartmentName = db.Apartments.Find(cust.ApartmentID).Name;
            }
            var Body = "Full Name:"+cust.FName +" "+cust.SName+"\n"+"Email:"+cust.Email+"\n Phone:"+cust.Phone+"\n"+"Apartment Name:"+GetApartmentName + "\n" + cust.RequestStr;
           
            var QuickTripEmail = "reservations@goaquicktrips.com";
           
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            try
            {
                db.Customers.Add(cust);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
                MailAddress fromAddress = new MailAddress(cust.Email, cust.FName+" "+cust.SName);
                
                smtpClient.Port = 25;
                smtpClient.Host = "goaquicktrips.com";
                message.From = fromAddress;
                message.To.Add(QuickTripEmail);
                message.Subject = "Enquiry";
                message.IsBodyHtml = false;
                message.Body = Body;
                smtpClient.UseDefaultCredentials = false;

                smtpClient.Credentials = new NetworkCredential("reservations@goaquicktrips.com", "xxxxxxx");

                smtpClient.EnableSsl = true;
                NEVER_EAT_POISON_Disable_CertificateValidation();
                smtpClient.Send(message);
                return View("QuerySuccessMessage");

            }
            catch (Exception ex)
            {
                throw ex;
            }


            
        }
        
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
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