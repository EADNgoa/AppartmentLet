using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GoaQuickTrips
{

    public class ConfigMetadata
    {
        [Display(Name = "Rows Per Page")]
        [Required]
        public int RowsPerPage;
    }
    public class ApartmentMetadata
    {
        [Display(Name = "Apartment Name:")]
        [Required]
        [StringLength(50)]
        public string Name;

        [Display(Name = "Description:")]
        [Required]
        public string Description;

        [Display(Name ="Rules:")]        
        public string Rules;

        [Display(Name ="Notes:")]        
        public string Notes;

        [Display(Name = "No of Guests:")]
        [Required]
        [Range(1, 10)]
        public int NoOfGuests;

        [Display(Name = "Address:")]
        [Required]
        [StringLength(250)]
        public string Address;

        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Please enter a valid email address")]
        [StringLength(50)]
        public string Email;

        [Display(Name ="Phone:")]
        [Required]        
        public string Phone;

        [Display(Name ="Location Info:")]        
        [DataType(DataType.MultilineText)]
        public string LocationInfo;

        [Display(Name = "Cancellation Policy")]                
        public string CancellationPolicy;

        [Display(Name = "Lat")]
        [Required]
        [StringLength(20)]
        public string Lat;

        [Display(Name = "Lang")]
        [Required]
        [StringLength(20)]
        public string Lang;
    }
    public class BookedCustomerMetadata
    {
        [Display(Name = "Cart")]
        public int CartID;

        [Display(Name = "Booking No")]
        public int BookingID;

        [Display(Name = "Customer Name")]
        public int CustomerID;

    }

    public class BookingDetailsMetadata
    {
        [Display(Name = "Booking No:")]
        [Required]
        public int BookingID;

        [Display(Name = "Apartment Name:")]
     
        public int ApartmentID;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Check IN:")]
       
        public DateTime CheckIn;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Check Out:")]
        [Required]
        public DateTime CheckOut;

        [Display(Name = "No Of Guests:")]
       
        public int NoOfGuests;

        [Display(Name = "Price:")]
       
        [Range(0.0, Double.MaxValue)]
        public Decimal Price;

        [Display(Name = "Blocked Reason:")]
    
        [StringLength(100)]
        public string BlockedReason;
    }

    public class BookingsMetadata
    {
        [Display(Name = "User")]
        [Required]
        public int UserID;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Booking Date")]
        public DateTime BookDate;

        [Display(Name = "Status")]
        public int StatusID;
    }

    public class CartMetadata
    {
        [Display(Name = "User")]
        public int UserID;

        [Display(Name = "Apartment Name")]
        public int ApartmentID;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Check IN")]
        public DateTime CheckIn;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Check OUT")]
        public DateTime CheckOut;

        [Display(Name = "No Of Guests")]
        [Range(0, 10)]
        public int NoOfGuests;

        [Display(Name = "Price1")]
        [Range(0.0, Double.MaxValue)]
        public Decimal OrigPrice;
        
    }

    public class CustomersMetadata
    {

        [Display(Name = "First Name:")]        
        [StringLength(50)]
        public string FName;

        [Display(Name = "Sur Name:")]        
        [StringLength(50)]
        public string SName;

        [Display(Name = "Email:")]        
        [StringLength(60)]
        public string Email;

        [Display(Name = "Phone:")]              
        public string Phone;

        [Display(Name ="ID Proof:")]
        public string IDpicture;

    }

    public class ImagesMetadata
    {
        [Display(Name = "Apartment Name:")]
        public int ApartmentID;

        [Display(Name = "Image Name:")]
        [Required]
        [StringLength(50)]
        public string Name;

        [Display(Name = "Upload:")]
        [Required]
        [StringLength(50)]
        public HttpPostedFileBase Path;
    }

    public class PricesMetadata
    {
        [Display(Name = "Apartment Name:")]
        public int ApartmentID;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "With Effect From:")]
        public DateTime WEF;

        [Display(Name = "Price:")]
        [Range(0.0, Double.MaxValue)]
        public Decimal Price1;
    }

    public class ReviewsMetadata
    {
        [Display(Name = "User:")]
        public int UserID;
        [Display(Name = "Apartment Name:")]
        public int ApartmentID;


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Review Date")]
        public DateTime ReviewDate;

        [Display(Name = "Review:")]
        [Required]        
        public string Review1;

        [Display(Name = "IS Visible:")]
        public bool IsVisible;
    }
    public class ReviewDetailMetadata
    {
        [Display(Name = "User:")]
        public int UserID;
     

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Review Date")]
        public DateTime ReviewDate;

        [Display(Name = "Review:")]
        [Required]        
        public string Reply;

        [Display(Name = "IS Visible:")]
        public bool ISvisible;
    }

    public class BookingViewMetadata
    {
      

    }
    

}