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
        [Required]
        public string Rules;

        [Display(Name ="Notes:")]
        [Required]
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
        [Required]
        [StringLength(50)]
        public string Email;

        [Display(Name ="Phone:")]
        [Required]
     
        public string Phone;

        [Display(Name ="Location Info:")]
        [Required]
        public string LocationInfo;

        [Display(Name = "Cancellation Policy")]
        [Required]
        [StringLength(100)]
        public string CancellationPolicy;

        [Display(Name = "Lat")]
        [Required]
        [StringLength(100)]
        public string Lat;

        [Display(Name = "Lang")]
        [Required]
        [StringLength(100)]
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
    
        [StringLength(30)]
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
        [Range(0,10)]
        public int NoOfGuests;

        [Display(Name = "Price1")]
        [Range(0.0, Double.MaxValue)]
        public Decimal OrigPrice;

    }

    public class CustomersMetadata
    {

        [Display(Name = "First Name:")]
        [Required]
        [StringLength(50)]
        public string FName;

        [Display(Name = "Sur Name:")]
        [Required]
        [StringLength(50)]
        public string SName;

        [Display(Name = "Email:")]
        [Required]
        [StringLength(60)]
        public string Email;

        [Display(Name = "Phone:")]
        [Required]
      
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
        public DateTime ReveiewDate;

        [Display(Name = "Review:")]
        [Required]
        [StringLength(50)]
        public string Review;

    }

    public class BookingViewMetadata
    {
      

    }





    //public class ConfigMetadata
    //{
    //    [Display(Name = "Bonus Percentage")]
    //    [Required]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal BonusPerc;

    //    [Display(Name = "Warn n days before document expires")]
    //    [Required]
    //    [Range(0, int.MaxValue)]
    //    public int DocExpirePreWarning;

    //    [Display(Name = "Rows per page")]
    //    [Required]
    //    [Range(0, 300)]
    //    public int RowsPerPage;

    //    [Display(Name = "Save Images To (path)")]
    //    [StringLength(150, MinimumLength = 3)]
    //    [Required]
    //    public string ImageSavePath;
    //}

    //public class EmpTypeMetadata
    //{
    //    [Display(Name = "Employee Type")]
    //    public int EmpTypeID;

    //    [Display(Name = "Employee Type")]
    //    [StringLength(50, MinimumLength = 3)]
    //    [Required]
    //    public int EmpType;

    //    [Display(Name = "Has Daily Allowance")]
    //    public bool HasDailyAllowance;
    //}

    //public class EDocTypeMetadata
    //{
    //    [Display(Name = "Employee Document Type")]
    //    public int EDocTypeID;

    //    [Display(Name = "Document Type")]
    //    [StringLength(50, MinimumLength = 3)]
    //    [Required]
    //    public string EDocType;


    //}

    //public class AllowanceTypeMetadata
    //{
    //    [Display(Name = "Allowance Type")]
    //    public int ATID;

    //    [Display(Name = "Allowance Type")]
    //    [StringLength(50, MinimumLength = 3)]
    //    [Required]
    //    public string AllowanceType;
    //}

    //public class EmployeesMetadata
    //{
    //    [Display(Name = "Name")]
    //    [StringLength(250, MinimumLength = 2)]
    //    [Required]
    //    public string Name;

    //    [Display(Name = "Nickname")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string NickName;

    //    [Display(Name = "Job Title")]
    //    [StringLength(150, MinimumLength = 3)]
    //    public string JobTitle;

    //    [Display(Name = "Mobile No.")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string Mobile;

    //    [Display(Name = "Emergency Contact No.")]
    //    [StringLength(50, MinimumLength = 3)]
    //    [Required]
    //    public string EmContactNo;

    //    [Display(Name = "Emergency Contact Name, Address(250 chars)")]
    //    [StringLength(250, MinimumLength = 3)]
    //    public string EmContactName;

    //    [Display(Name = "Relation with Emergency Contact")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string EmContactReln;

    //    [Display(Name = "is HDFC")]
    //    public bool IsHDFC;

    //    [Display(Name = "Category A or B?")]
    //    public string CatAB;


    //    [Display(Name = "HDFC Bank A/c")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string RegBankAc;

    //    [Display(Name = "Non HDFC bank A/c")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string NRegBankAc;

    //    [Display(Name = "Non. HDFC Bank Name")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string NRegBank;

    //    [Display(Name = "Non. HDFC Bank IFSC")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string NRegIFSC;

    //    [Display(Name = "Bonus Pay Month")]
    //    [Required]
    //    [Range(1, 12)]
    //    public int BonusPayMonth;

    //    [Display(Name = "PF No.")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string PFno;

    //    [Display(Name = "ESI No.")]
    //    [StringLength(50, MinimumLength = 3)]
    //    public string ESIno;


    //}

    //public class EmpDocsMetadata
    //{
    //    [Display(Name = "Employee")]
    //    public int EmpID;

    //    [Display(Name = "Document Type")]
    //    public int EDocTypeID;

    //    [Display(Name = "Employee")]
    //    public Employees Employees;

    //    [Display(Name = "Document Type")]
    //    public EDocTypes EDocTypes;

    //    [Display(Name = "Hide")]
    //    public bool Renewed;

    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Expiry Date")]        
    //    public DateTime ExpiryDate;
    //}

    //public class EmploymentHistoryMetadata
    //{
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Join Date")]
    //    [Required]
    //    public DateTime JoinDate;

    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Registration Date")]
    //    public DateTime RegistrationDate;

    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Exit Date")]
    //    public DateTime ExitDate;

    //    [Display(Name = "Reason for leaving")]
    //    public string ExitReason;
    //}

    //public class LoansMetadata
    //{
    //    [Display(Name = "Loan")]
    //    public int LoanID;


    //    [DataType(DataType.Date)]
    //   [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Loan Date")]
    //    public DateTime LoanDate;

    //    [Display(Name = "Loan Amount")]
    //    [Required]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal Amount;

    //    [Display(Name = "Payback period (months)")]
    //    [Required]
    //    [Range(1, 48)]
    //    public int PayMonths;
    //}

    //public class LoanSkipMetadata
    //{
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Apply Date")]
    //    public DateTime PayDate;

    //    [Display(Name = "Amount payable (min 0)")]
    //    [Required]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal Amount;
    //}

    //public class WagesMetadata
    //{
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "With Effect From")]
    //    [Required]
    //    public DateTime WEF;

    //    [Display(Name = "Amount payable (min 0)")]
    //    [Required]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal Amount;
    //}

    //public class AllowanceMetadata
    //{
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "With Effect From")]
    //    [Required]
    //    public DateTime WEF;

    //    [Display(Name = "Percentage of Wage")]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal PercOfWage;

    //    [Display(Name = "Amount")]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal Amount;

    //}

    //public class AdvanceMetadata
    //{
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Advance taken on")]
    //    [Required]
    //    public DateTime AdvDate;

    //    [Display(Name = "Amount")]
    //    [Required]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal Amount;
    //}

    //public class BonusMetadata
    //{
    //    [Display(Name = "System Bonus")]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal SysBonus;

    //    [Display(Name = "Manual Override")]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal UsrBonus;
    //}

    //public class PayrollMetadata
    //{
    //    [Display(Name = "Days Worked")]        
    //    public int DaysWorked;

    //    [Display(Name = "Loan Amt.")]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal LoanAmt;

    //    [Display(Name = "Loan")]
    //    public String LoanCmt;

    //    [Display(Name = "Bank")]
    //    public String BankName;

    //    [Display(Name = "Account")]
    //    public String BankAccount;

    //    [Display(Name = "Adjustment")]
    //    [Range(0.0, Double.MaxValue)]
    //    public decimal AdjAmt;

    //    [Display(Name = "Reason")]
    //    public String AdjRemark;
    //}


}