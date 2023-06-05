using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_BAL
{
    public class BAL_Application : DMS_DAL.DAL_Application
    {
        DataTable dt = new DataTable();

        public DataTable usp_Application(int OperationType, int? ApplicationId, string Application_RefNo, string Domicile_No, int? ApplicationTypeId, int? RequestTypeId, int? ApplicationStatusId, string Title, string Applicant_Name, string Father_Name,
                                       string Applicant_Cnic, DateTime? Date_of_Birth, string Place_of_Birth, string Resident_of, string Surname, string Primary_School, string Town_PrimarySchool, string FromDate_PrimarySchool, string ToDate_PrimarySchool, string Middle_School,
                                       string Town_MiddleSchool, string FromDate_MiddleSchool, string ToDate_ModdleSchool, string High_School, string Town_HighSchool, string FromDate_HighSchool, string ToDate_HighSchool, string District_Education, DateTime? Date_Submission, DateTime? Date_Issue,
                                       string ParticularsProperty, string Location, int? TalukaId, string District, string Electoral_Area, string Electoral_Area_Taluka, int? Electoreal_Area_Deh, string Guardian_NIC, int? Guardian_RelationShip, string Applicant_PhoneNo,
                                       string Temporary_Address, string Permanent_Address, DateTime? GuardianDomicile_CertificateDate, int? Guardian_RelationShip2, int? Applicant_Age, string Trade_Occupation, string Mark_of_Identification, DateTime? Date_of_Arrival, string Applicant_Photo_Path, string Address_ForeignCountry,
                                       string Mukhtiarkar_Taluka, string Deputy_District_Officer_Taluka, string Husband_Wife_Name, string Marital_Status, DateTime? FromDate, DateTime? ToDate, DateTime? From_IssuanceDate, DateTime? To_IssuanceDate, DateTime? FromCancellation_Date, DateTime? ToCancellation_Date,
                                       int UserId, string UserIP, DataTable dtchild, string CNIC_Front, string CNIC_Back, string ASSISTANT_COMMISSIONERS_REPORT_Path, string MUKHTIARKAR_REPORT_Path, string PRIMARY_CERTIFICATE_Path, string MATRIC_CERTIFICATE_Path, string RESIDENCE_CERTIFICATE_Path,
                                       string VOTE_CERTIFICATE_Path, string GUARDIANS_DOMICILE_Path, string BANK_CHALLANS_Path, string OTHER_DOCUMENT1_Path, string OTHER_DOCUMENT2_Path, string ObjectionComments, DateTime? DomicileApprovedDate, string FormC_No, string FormD_No, int? ApplicationId_RevisedDuplicate,
                                       string CNICFront_Revise, string CNICBack_Revise, string Approval_AuthorityRevise, string BankChallan_Revise, string CorrectionDoc1_Revise, string CorrectionDoc2_Revise, string OtherDoc_Revise, string CNICFront_Duplicate, string CNICBack_Duplicate, string FirCopy_Duplicate,
                                       string Approval_AuthorityDuplicate, string BankChallan_Duplicate, string Application_Duplicate, string CNICFront_Cancel, string CNICBack_Cancel, string Residence_Cancel, string Vote_Cancel, string Application_Cancel, string Affidevit_Cancel, string Approval_Cancel,
                                       string OthersDoc_Cancel, string OthersDoc1_Cancel ,  bool? IsByBirth, string RejectionComments = null, string DeleteComments = null)
        {
            dt = usp_Crud_Application(OperationType, ApplicationId, Application_RefNo, Domicile_No, ApplicationTypeId, RequestTypeId, ApplicationStatusId, Title, Applicant_Name, Father_Name, Applicant_Cnic, Date_of_Birth, Place_of_Birth, Resident_of, Surname, Primary_School, Town_PrimarySchool, FromDate_PrimarySchool, ToDate_PrimarySchool, Middle_School, Town_MiddleSchool, FromDate_MiddleSchool, ToDate_ModdleSchool, High_School, Town_HighSchool, FromDate_HighSchool, ToDate_HighSchool, District_Education, Date_Submission, Date_Issue, ParticularsProperty, Location, TalukaId, District, Electoral_Area, Electoral_Area_Taluka, Electoreal_Area_Deh, Guardian_NIC, Guardian_RelationShip, Applicant_PhoneNo, Temporary_Address, Permanent_Address, GuardianDomicile_CertificateDate, Guardian_RelationShip2, Applicant_Age, Trade_Occupation, Mark_of_Identification, Date_of_Arrival, Applicant_Photo_Path, Address_ForeignCountry, Mukhtiarkar_Taluka, Deputy_District_Officer_Taluka, Husband_Wife_Name, Marital_Status,
                FromDate, ToDate, From_IssuanceDate, To_IssuanceDate, FromCancellation_Date, ToCancellation_Date, UserId, UserIP, dtchild, CNIC_Front, CNIC_Back, ASSISTANT_COMMISSIONERS_REPORT_Path, MUKHTIARKAR_REPORT_Path, PRIMARY_CERTIFICATE_Path, MATRIC_CERTIFICATE_Path, RESIDENCE_CERTIFICATE_Path, VOTE_CERTIFICATE_Path, GUARDIANS_DOMICILE_Path, BANK_CHALLANS_Path, OTHER_DOCUMENT1_Path, OTHER_DOCUMENT2_Path, ObjectionComments, DomicileApprovedDate, FormC_No, FormD_No,
                ApplicationId_RevisedDuplicate, CNICFront_Revise, CNICBack_Revise, Approval_AuthorityRevise, BankChallan_Revise, CorrectionDoc1_Revise, CorrectionDoc2_Revise, OtherDoc_Revise, CNICFront_Duplicate, CNICBack_Duplicate, FirCopy_Duplicate, Approval_AuthorityDuplicate, BankChallan_Duplicate, Application_Duplicate,
                 CNICFront_Cancel, CNICBack_Cancel, Residence_Cancel, Vote_Cancel, Application_Cancel, Affidevit_Cancel, Approval_Cancel, OthersDoc_Cancel, OthersDoc1_Cancel , IsByBirth , RejectionComments, DeleteComments);
            return dt;
        }


        public DataTable usp_UpdateApplicationDocuments(int OperationType, int? ApplicationId, string CNIC_Front, string CNIC_Back, string ASSISTANT_COMMISSIONERS_REPORT_Path, string MUKHTIARKAR_REPORT_Path, string PRIMARY_CERTIFICATE_Path, string MATRIC_CERTIFICATE_Path, string RESIDENCE_CERTIFICATE_Path, string VOTE_CERTIFICATE_Path, string GUARDIANS_DOMICILE_Path, string BANK_CHALLANS_Path, string OTHER_DOCUMENT1_Path, string OTHER_DOCUMENT2_Path, string OTHER_DOCUMENT3_Path, string OTHER_DOCUMENT4_Path, string OTHER_DOCUMENT5_Path,
                                                         string CNICFront_Revise, string CNICBack_Revise, string Approval_AuthorityRevise, string BankChallan_Revise, string CorrectionDoc1_Revise, string CorrectionDoc2_Revise, string OtherDoc_Revise, string OtherDoc2_Revise,  string OtherDoc3_Revise,  string OtherDoc4_Revise,
                                                         string CNICFront_Duplicate, string CNICBack_Duplicate, string FirCopy_Duplicate, string Approval_AuthorityDuplicate, string BankChallan_Duplicate, string Application_Duplicate, string OtherDoc1_Dup, string OtherDoc2_Dup, string OtherDoc3_Dup,
                                                         string CNICFront_Cancel, string CNICBack_Cancel, string Residence_Cancel, string Vote_Cancel, string Application_Cancel, string Affidevit_Cancel, string Approval_Cancel, string OthersDoc_Cancel, string OthersDoc1_Cancel, string OthersDoc2_Cancel, string OthersDoc3_Cancel, string OthersDoc4_Cancel,
                                                         int UserId)
        {
            dt = usp_UpdateDocs_Application(OperationType, ApplicationId, CNIC_Front, CNIC_Back, ASSISTANT_COMMISSIONERS_REPORT_Path, MUKHTIARKAR_REPORT_Path, PRIMARY_CERTIFICATE_Path, MATRIC_CERTIFICATE_Path, RESIDENCE_CERTIFICATE_Path, VOTE_CERTIFICATE_Path, GUARDIANS_DOMICILE_Path, BANK_CHALLANS_Path, OTHER_DOCUMENT1_Path, OTHER_DOCUMENT2_Path,  OTHER_DOCUMENT3_Path,  OTHER_DOCUMENT4_Path,  OTHER_DOCUMENT5_Path,
                                      CNICFront_Revise, CNICBack_Revise, Approval_AuthorityRevise, BankChallan_Revise, CorrectionDoc1_Revise, CorrectionDoc2_Revise, OtherDoc_Revise, OtherDoc2_Revise , OtherDoc3_Revise , OtherDoc4_Revise,
                                       CNICFront_Duplicate, CNICBack_Duplicate, FirCopy_Duplicate, Approval_AuthorityDuplicate, BankChallan_Duplicate, Application_Duplicate, OtherDoc1_Dup, OtherDoc2_Dup , OtherDoc3_Dup,
                                       CNICFront_Cancel, CNICBack_Cancel, Residence_Cancel, Vote_Cancel, Application_Cancel, Affidevit_Cancel, Approval_Cancel, OthersDoc_Cancel, OthersDoc1_Cancel , OthersDoc2_Cancel, OthersDoc3_Cancel , OthersDoc4_Cancel,
                                       UserId);
            return dt;

        }

    }
}
