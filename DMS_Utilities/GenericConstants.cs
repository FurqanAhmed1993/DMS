using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_Utilities
{
    public struct GenericConstants
    {
        public const string ConnectionStringKey = "ConnectionString";
        public static int Sql_CommandTimeout = 36000;
        public const string DefaultDate = "01/01/1900";
        public const string MYDateFormatLong = "dd/MM/yyyy";
        public const string NEWDateFormat = "yyyy/MM/dd";
        public const string DateFormat1_ = "dd-MMM-yyyy";
        public const string DateTimeFormat11_ = "yyyy/MM/dd hh:mm";
        public const string DateTimeFormat1_ = "dd-MMM-yyyy hh:mm:ss tt";
        public const string DateTimeFormat2_ = "dd MMM yyyy";
        public const string DateTimeFormat2 = "dd-MMM-yyyy";
        public const string DateFormat2 = "MM/dd/yyyy";
        public const string DateFormatWithTime = "MM/dd/yyyy ddd HH:mm";
        public const string DateFormat = "MMM dd,yyyy";
        public const string DateFormat1 = "dd-MMM-yyyy ddd";
        public const string DateTimeFormat1 = "dd-MMM-yyyy ddd hh:mm:ss tt";
        public const string IntDateFormat = "yyyyMMdd";
        public const string TimeFormat = "mm:tt";
        public const string TimeFormatHour = "HH:MM";
        public const string DateFormatLong = "dd/MM/yyyy HH:mm:ss";
        public const string TimeFormatLong = "HH:mm:ss";
        public const string TimeFormatAMPM = "hh:mm:ss tt";
        public const string Password = "ppcrm123$";
        public const string ErrorLog = "ExceptionsLogs";
        public const string GetDefaultPage = "/Pages/Dashboard.aspx";
        public const decimal DiscountAlertPercentage = 20;
        public const string AttachmentsFolderName = "Attachments";
        public const string ReceiptAttachmentsFolderPath = "Attachments/OrderReceipt/";
        public const string PrescriptiontAttachmentsFolderPath = "Attachments/Prescription/";
        public const string HCPConsentAttachmentsFolderPath = "Attachments/HCPConsent/";
        public const string OrderSlipPath = "/Pages/Reports/OrderSlip.rpt";
        public const int Doctor_NotAvailableId = 1;
        public const int FollowupCallDays_Subtract = 5;
        public const string EmailLog = "EmailLog";
        public const string SMSLog = "SMSLog";
        public const string StarterKitAttachmentsFolderPath = "Attachments/StarterKit/";
        public const string DateTimeFormat_yMd = "yyyy-MM-dd";
        public const string HasError = "1";
        public const string HasNoError = "0";
        public const string EducationMaterial = "Uploads/EducationMaterial/";
        public const int PasswordLength = 8;
        public const int MobileNoLength = 11;
        public const string MobileNoStartChar = "03";
        public const string ProductDetailAttachmentsFolderPath = "Attachments/ProductDetail/";
        public const bool IsMobileData = true;
        public const bool IsMobileInsertData = true;
        public const string OtSelect = "Select";
        public const string OtInsert = "Insert";
        public const string OtUpdate = "Update";
        public const string OtDelete = "Delete";
        public const string OtIsExist = "IsExist";
        public const string OtIsExistById = "IsExistById";
        public const int PhoneNumberLength = 11;
        public const string RoleSuperAdmin = "Super Admin";
        public const string RoleCreator = "Creator";
        public const string RoleAgent = "Agent";
        public const string Status1 = "New";
        public const string Status2 = "Closed";
        public const string Status3 = "Reopen";
        public const string Status4 = "Cancel";
        public const string Status5 = "Pending";
        public const int StatusId1 = 1;
        public const int StatusId2 = 2;
        public const int StatusId3 = 3;
        public const int StatusId4 = 4;
        public const int StatusId5 = 5;
    }

    public struct UserRole
    {
        public const int SuperAdmin = 1;
        public const int Approver = 2;
        public const int Creator = 3;
        public const int SubAdmin = 1043;
        public const int Agent = 4;
        public const int Distributer = 5;
        public const int CommunicationManager = 1037;
        public const int SupplyChain = 1038;
        public const int DCM = 1039;//Diabities Zonal manager
        public const int Educator = 1044;
        public const int Doctor = 1045;
    }

    public struct Setup_Master
    {
        public const int ContactPerson = 1;
        public const int Country = 2;
        public const int City = 3;
        public const int Area = 4;
        public const int OperationType = 5;
        public const int PatientDroppedReason = 6;
        public const int Province = 7;
        public const int Gender = 8;
        public const int MaritalStatus = 9;
        public const int Occupation = 10;
        public const int Qualification = 11;
        public const int Region = 12;
        public const int Unit = 13;
        public const int Speicalization = 14;
        public const int Strength = 15;
        public const int PhoneType = 16;
        public const int PatientStatus = 17;
        public const int Complain_Inquiry_Status = 18;
        public const int Nature = 19;
        public const int Channel = 20;
        public const int Complain_Inquiry_Type = 21;
        public const int Follow_Up_Product_Remarks_Type = 22;
        public const int Follow_Up_Product_Remarks = 23;
        public const int ReceiptInfo = 24;
        public const int Product_CAP_Type = 25;
        public const int Patient_Program = 26;
        public const int PrescriptionValidated = 30;
        public const int PrescriptionValidationRows = 31;
        public const int Educator_Service = 32;
        public const int GlycemicType = 33;
        public const int FileType = 34;
        public const int OtpExpireTimeInSeconds = 35;
        public const int EducatorStatus = 36;
    }

    public struct Setup_MasterDetail
    {
        public const int Primary = 1;
        public const int Secondary = 2;
        public const int Supervisor = 3;
        public const int Active = 54;
        public const int Inactive = 55;
        public const int Expired = 56;
        public const int PatientDropped = 57;
        public const int Mobile = 52;
        public const int Complain = 1078;
        public const int Inquiry = 1079;
        public const int Product_Follow_Up_Required = 1083;
        public const int Product_Dropped = 1084;
        public const int Product_Ordered = 1082;
        public const int Per_Order_CAP = 38;
        public const int Annual_Order_CAP = 39;
        public const int Home_Visit = 3357;// live 3359;
        public const int Virtual_Call = 3358;// Live 3360;
        public const int OtpExpireTime = 3368;// Live 3360;
        public const int Assigned = 3437;// Educator Request Status ;
        public const int InProgress = 3438;// Educator Request Status;
        public const int Close = 3439;// Educator Request Status;
        public const int UnAssigned = 3506;// Educator Request Status;
    }

    public struct Feature
    {
        public const int Add = 1;
        public const int Update = 2;
        public const int Delete = 3;
        public const int View = 4;
        public const int Submit = 5;
        public const int Create_Complain_Inqyiry = 6;
        public const int UpdateDoctor = 7;
        public const int DownloadPrescription = 8;
        public const int HidePatientColoumnsInReport = 9;
        public const int OrderSlip = 10;
        public const int NewPrescriptionUpload = 11;
        public const int NewStarterKitUpload = 12;
        public const int DownLoadStarterKit = 13;
        public const int FollowUpChecked = 14;//Live 14;
        public const int MobileOrders = 15;
    }

    public struct OrderStatus
    {
        public const int New = 1;
        public const int Received = 2;
        public const int Dispatched = 3;
        public const int Delivered = 4;
        public const int Cenceled = 5;
        public const int NotConfirmed = 6;
        public const int Delayed = 7;
    }
    public struct SurveyTypes
    {
        public const int EducatedSurvey = 1;
    }

    public struct Complain_Inquiry_Status
    {
        public const int New = 1065;
        public const int Inprogress = 1066;
        public const int Closed = 1067;
    }
    public struct InventoryType
    {
        public const int InventoryIn = 3351;
        public const int InventoryOut = 3352;
    }
    public struct PrescriptionValidationStatusId
    {
        public const int Validated = 3353;
        public const int NotValidated = 3354;
    }

    public class ResponseKeys
    {
        public static string Data = "Data";
        public static string Response = "Response";
        public static string ResponseCode = "ResponseCode";
        public static string ErrorMessage = "ErrorMessage";
        public static string ResponseMessage = "ResponseMessage";
        public static string Token = "Token";
    }

    public class ResponseCodes
    {
        public static string Success = "00";
        public static string TokenExpired = "01";
        public static string NotGeneratedAgainstThisUser = "02";
        public static string InvalidToken = "03";
        public static string InvalidCredentials = "04";
        public static string Exception = "05";
        public static string ValidationError = "07";
        public static string Failure = "11";
    }

    public class ResponseMessages
    {
        public static string Success = "Success";
        public static string TokenExpired = "Token Expired";
        public static string NotGeneratedAgainstThisUser = "Token is not valid for this user";
        public static string InvalidToken = "Invalid Token";
        public static string InvalidCredentials = "Invalid Credentials";
        public static string InvalidErrorCode = "Invalid Error Code";
        public static string Failure = "Failure";
        public static string Exception = "An Exception has been occured";
        public static string NoData = "No Data Found";
        public static string ExceptionMessage = "An Exception has occured. Some thing went wrong";
        public static string InvalidPatientId = "Invalid Patient";
        public static string SuccessfullyRegistered = "SuccessfullyRegistered";
        public static string InvalidParameters = "Invalid Parameters";
        public static string SuccessfullyUpdated = "Successfully Updated";
        public static string SuccessfullyAdded = "Successfully Added";
        public static string SuccessfullyDeleted = "Successfully Deleted";
        public static string InvalidOtp = "Invalid OTP";
        public static string SuccessfullyPasswordChanged = "Password has been successfully updated";
        public static string InvalidRequest = "Invalid Request";
        public static string UnableToReOrder = "Unable To Re-Order";

    }
    public struct OperationTypes
    {
        public const string Select = "Select";
        public const string Populate = "Populate";
        public const string Insert = "Insert";
        public const string Update = "Update";
        public const string Delete = "Delete";
        public const string GetEducatorOnCity = "GetEducatorOnCity";
        public const string Patient = "Patient";
        public const string Product = "Product";
        public const string ProductSelect = "ProductSelect";
    }

    public struct OperationTypesID
    {
        public const int Select = 1;
        public const int Insert = 2;
        public const int Update = 3;
        public const int Delete = 4;
        public const int OtIsExist = 5;
        public const int OtIsExistById = 6;
        public const int GetUserTalukaMapping = 7;
        public const int GetTaluka = 8;
        public const int GetDehByTaluka = 9;
        public const int GetLastRecord_ID = 10;
        public const int InsertChild_BulkRecord = 11;
        public const int GetChildByApplicationId = 12;
        public const int DeleteChildByApplicationId = 13;
        public const int UpdateApplicationDocuments = 14;
        public const int UpdateApplicationStatus = 15;
        public const int ReviseApplicationDocs = 16;
        public const int UpdateReviseApplicationDocuments = 17;
        public const int GetRevised_DuplicationApplication = 18;
        public const int UpdateApplication_ObjectionComments = 19;
        public const int UpdateDomicileApprovalDate_ApplicationApprovalDate = 20;
        public const int UpdateDuplicateApplicationDocuments = 21;
        public const int UpdateCancelledApplicationDocuments = 22;
        public const int UpdateApplicationApprovalDate_DomicileCancellationDate = 23;
        public const int GetDomicileHistory = 24;
        public const int UpdateApplicationApprovalDate = 25;
        public const int UpdateApplication_RejectionComments = 26;
        public const int UpdateApplication_DeleteComments = 27;
    }
    public struct StatusId
    {

        public const int InitialDraft = 1;
        public const int Submitted_to_DDO = 2;
        public const int Rejected = 3;
        public const int Objected = 4;
        public const int Approved = 5;
        public const int Disable = 6;
        public const int Cancelled = 7;
        public const int Issued = 8;
    }

    public struct RequestTypeID
    {
        public const int New_Request = 1;
        public const int Duplication = 2;
        public const int Revision = 3;
        public const int Cancellation = 4;
    }

    public struct DocumentType
    {
        public const int Domicile = 1;
        public const int FormC = 2;
        public const int FormD = 3;
    }

    public struct ApplicationTypeValueId
    {
        public const int Major = 1;
        public const int Minor = 2;
    }
    public class OrderMaster
    {
        public int PatientId { get; set; }
        public int PatientAddressId { get; set; }
        public string DeliveryDate { get; set; }
        public bool IsUrgent { get; set; }
        public decimal? HBA1C { get; set; }
        public decimal? FBS { get; set; }
        public bool IsFollowUpRequested { get; set; }
        public int CreatedBy { get; set; }
        public string UserIp { get; set; }
        public List<OrderDetail> lstOrderDetail { get; set; }
    }
    public class OrderDetail
    {
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public int CurrentDosage { get; set; }
        public double TradePricePerItem { get; set; }
        public double TradePrice { get; set; }
        public double RetailPricePerItem { get; set; }
        public double RetailPrice { get; set; }
        public int? DoctorId { get; set; }
        public int? IBKAMProductDetailMappingID { get; set; }
        public double DiscountPercentage { get; set; }
        public double DiscountPrice { get; set; }
        //public bool IsAllowStarterKit { get; set; }

    }

    public class Menu
    {
        public string path { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string component { get; set; }
        public string layout { get; set; }
        public List<SubMenu> SubMenus { get; set; }
    }
    public class SubMenu
    {
        public string path { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string component { get; set; }
        public string layout { get; set; }
    }
    public class FilesUpload
    {
        public string OrignalFileName { get; set; }
        public string FileName { get; set; }
        public string FilePathGuid { get; set; }

    }
    public class OrderList
    {
        public string OrderNumber { get; set; }
        public string PatientName { get; set; }
        public int OrderMasterId { get; set; }
        public string InititatorName { get; set; }
        public int InitiatorPatientId { get; set; }
        public int InitiatorUserId { get; set; }
        public decimal TotalRetailPrice { get; set; }
        public decimal TotalTradePrice { get; set; }
        public decimal TotalFinalPrice { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string OrderStatus { get; set; }
        public int TotalQuantity { get; set; }
        public int OrderStatusId { get; set; }
        public List<Products> productList { get; set; }
    }
    public class Products
    {
        public int OrderMasterId { get; set; }
        public int OrderDetailId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal TradePrice { get; set; }
        public int AnnualCAP { get; set; }
        public int PerOrderCAP { get; set; }
        public int ProductDetailId { get; set; }
        public int ProductId { get; set; }

    }
    public struct OtpMessages
    {
        public const string ForgotPasswordOTP = "Use this DiaBuddy Code <OTP>, do not share it with anyone. Valid for <Time> minute(s).\nE2StheUupsi";
    }
    public struct Distributors
    {
        public const int NoDistributor = 49;
    }
}
