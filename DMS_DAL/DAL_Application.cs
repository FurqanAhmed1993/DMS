using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DMS_DAL
{
    public class DAL_Application : DAL
    {
        public DataTable usp_Crud_Application(int OperationType, int? ApplicationId, string Application_RefNo, string Domicile_No, int? ApplicationTypeId, int? RequestTypeId, int? ApplicationStatusId, string Title, string Applicant_Name, string Father_Name,
                                             string Applicant_Cnic, DateTime? Date_of_Birth, string Place_of_Birth, string Resident_of, string Surname, string Primary_School, string Town_PrimarySchool, string FromDate_PrimarySchool, string ToDate_PrimarySchool, string Middle_School,
                                             string Town_MiddleSchool, string FromDate_MiddleSchool, string ToDate_ModdleSchool, string High_School, string Town_HighSchool, string FromDate_HighSchool, string ToDate_HighSchool, string District_Education, DateTime? Date_Submission, DateTime? Date_Issue,
                                             string ParticularsProperty, string Location, int? TalukaId, string District, string Electoral_Area, string Electoral_Area_Taluka, int? Electoreal_Area_Deh, string Guardian_NIC, int? Guardian_RelationShip, string Applicant_PhoneNo,
                                             string Temporary_Address, string Permanent_Address, DateTime? GuardianDomicile_CertificateDate, int? Guardian_RelationShip2, int? Applicant_Age, string Trade_Occupation, string Mark_of_Identification, DateTime? Date_of_Arrival, string Applicant_Photo_Path, string Address_ForeignCountry,
                                             string Mukhtiarkar_Taluka, string Deputy_District_Officer_Taluka, string Husband_Wife_Name, string Marital_Status, DateTime? FromDate, DateTime? ToDate, DateTime? From_IssuanceDate, DateTime? To_IssuanceDate, DateTime? FromCancellation_Date, DateTime? ToCancellation_Date,
                                             int UserId, string UserIP, DataTable dtchild, string CNIC_Front, string CNIC_Back, string ASSISTANT_COMMISSIONERS_REPORT_Path, string MUKHTIARKAR_REPORT_Path, string PRIMARY_CERTIFICATE_Path, string MATRIC_CERTIFICATE_Path, string RESIDENCE_CERTIFICATE_Path,
                                             string VOTE_CERTIFICATE_Path, string GUARDIANS_DOMICILE_Path, string BANK_CHALLANS_Path, string OTHER_DOCUMENT1_Path, string OTHER_DOCUMENT2_Path, string ObjectionComments, DateTime? DomicileApprovedDate, string FormC_No, string FormD_No, int? ApplicationId_RevisedDuplicate,
                                             string CNICFront_Revise, string CNICBack_Revise, string Approval_AuthorityRevise, string BankChallan_Revise, string CorrectionDoc1_Revise, string CorrectionDoc2_Revise, string OtherDoc_Revise, string CNICFront_Duplicate, string CNICBack_Duplicate, string FirCopy_Duplicate,
                                             string Approval_AuthorityDuplicate, string BankChallan_Duplicate, string Application_Duplicate, string CNICFront_Cancel, string CNICBack_Cancel, string Residence_Cancel, string Vote_Cancel, string Application_Cancel, string Affidevit_Cancel, string Approval_Cancel,
                                             string OthersDoc_Cancel, string OthersDoc1_Cancel, bool? IsByBirth , string RejectionComments , string DeleteComments)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Application_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 200;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@ApplicationId", SqlDbType.Int).Value = ApplicationId;
                    cmd.Parameters.Add("@Application_RefNo", SqlDbType.VarChar).Value = Application_RefNo;
                    cmd.Parameters.Add("@Domicile_No", SqlDbType.VarChar).Value = Domicile_No;
                    cmd.Parameters.Add("@ApplicationTypeId", SqlDbType.Int).Value = ApplicationTypeId;
                    cmd.Parameters.Add("@RequestTypeId", SqlDbType.Int).Value = RequestTypeId;
                    cmd.Parameters.Add("@ApplicationStatusId", SqlDbType.Int).Value = ApplicationStatusId;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title;
                    cmd.Parameters.Add("@Applicant_Name", SqlDbType.VarChar).Value = Applicant_Name;
                    cmd.Parameters.Add("@Father_Name", SqlDbType.VarChar).Value = Father_Name;
                    cmd.Parameters.Add("@Applicant_Cnic", SqlDbType.VarChar).Value = Applicant_Cnic;
                    cmd.Parameters.Add("@Date_of_Birth", SqlDbType.Date).Value = Date_of_Birth;
                    cmd.Parameters.Add("@Place_of_Birth", SqlDbType.VarChar).Value = Place_of_Birth;
                    cmd.Parameters.Add("@Resident_of", SqlDbType.VarChar).Value = Resident_of;
                    cmd.Parameters.Add("@Surname", SqlDbType.VarChar).Value = Surname;
                    cmd.Parameters.Add("@Primary_School", SqlDbType.VarChar).Value = Primary_School;
                    cmd.Parameters.Add("@Town_PrimarySchool", SqlDbType.VarChar).Value = Town_PrimarySchool;
                    cmd.Parameters.Add("@FromDate_PrimarySchool", SqlDbType.VarChar).Value = FromDate_PrimarySchool;
                    cmd.Parameters.Add("@ToDate_PrimarySchool", SqlDbType.VarChar).Value = ToDate_PrimarySchool;
                    cmd.Parameters.Add("@Middle_School", SqlDbType.VarChar).Value = Middle_School;
                    cmd.Parameters.Add("@Town_MiddleSchool", SqlDbType.VarChar).Value = Town_MiddleSchool;
                    cmd.Parameters.Add("@FromDate_MiddleSchool", SqlDbType.VarChar).Value = FromDate_MiddleSchool;
                    cmd.Parameters.Add("@ToDate_ModdleSchool", SqlDbType.VarChar).Value = ToDate_ModdleSchool;
                    cmd.Parameters.Add("@High_School", SqlDbType.VarChar).Value = High_School;
                    cmd.Parameters.Add("@Town_HighSchool", SqlDbType.VarChar).Value = Town_HighSchool;
                    cmd.Parameters.Add("@FromDate_HighSchool", SqlDbType.VarChar).Value = FromDate_HighSchool;
                    cmd.Parameters.Add("@ToDate_HighSchool", SqlDbType.VarChar).Value = ToDate_HighSchool;
                    cmd.Parameters.Add("@District_Education", SqlDbType.VarChar).Value = District_Education;
                    cmd.Parameters.Add("@Date_Submission", SqlDbType.Date).Value = Date_Submission;
                    cmd.Parameters.Add("@Date_Issue", SqlDbType.Date).Value = Date_Issue;
                    cmd.Parameters.Add("@ParticularsProperty", SqlDbType.VarChar).Value = ParticularsProperty;
                    cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location;
                    cmd.Parameters.Add("@TalukaId", SqlDbType.Int).Value = TalukaId;
                    cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = District;
                    cmd.Parameters.Add("@Electoral_Area", SqlDbType.VarChar).Value = Electoral_Area;
                    cmd.Parameters.Add("@Electoral_Area_Taluka", SqlDbType.VarChar).Value = Electoral_Area_Taluka;
                    cmd.Parameters.Add("@Electoreal_Area_Deh", SqlDbType.VarChar).Value = Electoreal_Area_Deh;
                    cmd.Parameters.Add("@Guardian_NIC", SqlDbType.VarChar).Value = Guardian_NIC;
                    cmd.Parameters.Add("@Guardian_RelationShip", SqlDbType.Int).Value = Guardian_RelationShip;
                    cmd.Parameters.Add("@Applicant_PhoneNo", SqlDbType.VarChar).Value = Applicant_PhoneNo;
                    cmd.Parameters.Add("@Temporary_Address", SqlDbType.VarChar).Value = Temporary_Address;
                    cmd.Parameters.Add("@Permanent_Address", SqlDbType.VarChar).Value = Permanent_Address;
                    cmd.Parameters.Add("@GuardianDomicile_CertificateDate", SqlDbType.Date).Value = GuardianDomicile_CertificateDate;
                    cmd.Parameters.Add("@Guardian_RelationShip2", SqlDbType.Int).Value = Guardian_RelationShip2;
                    cmd.Parameters.Add("@Applicant_Age", SqlDbType.Int).Value = Applicant_Age;
                    cmd.Parameters.Add("@Trade_Occupation", SqlDbType.VarChar).Value = Trade_Occupation;
                    cmd.Parameters.Add("@Mark_of_Identification", SqlDbType.VarChar).Value = Mark_of_Identification;
                    cmd.Parameters.Add("@Date_of_Arrival", SqlDbType.Date).Value = Date_of_Arrival;
                    cmd.Parameters.Add("@Applicant_Photo_Path", SqlDbType.VarChar).Value = Applicant_Photo_Path;
                    cmd.Parameters.Add("@Address_ForeignCountry", SqlDbType.VarChar).Value = Address_ForeignCountry;
                    cmd.Parameters.Add("@Mukhtiarkar_Taluka", SqlDbType.VarChar).Value = Mukhtiarkar_Taluka;
                    cmd.Parameters.Add("@Deputy_District_Officer_Taluka", SqlDbType.VarChar).Value = Deputy_District_Officer_Taluka;
                    cmd.Parameters.Add("@Husband_Wife_Name", SqlDbType.VarChar).Value = Husband_Wife_Name;
                    cmd.Parameters.Add("@Marital_Status", SqlDbType.VarChar).Value = Marital_Status;
                    cmd.Parameters.Add("@From_Date", SqlDbType.DateTime).Value = FromDate;
                    cmd.Parameters.Add("@To_Date", SqlDbType.DateTime).Value = ToDate;
                    cmd.Parameters.Add("@From_IssuanceDate", SqlDbType.DateTime).Value = From_IssuanceDate;
                    cmd.Parameters.Add("@To_IssuanceDate", SqlDbType.DateTime).Value = To_IssuanceDate;
                    cmd.Parameters.Add("@FromCancellation_Date", SqlDbType.DateTime).Value = FromCancellation_Date;
                    cmd.Parameters.Add("@ToCancellation_Date", SqlDbType.DateTime).Value = ToCancellation_Date;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;
                    cmd.Parameters.Add("@ChildDetails", SqlDbType.Structured).Value = dtchild;

                    cmd.Parameters.Add("@CNIC_Front_Path", SqlDbType.VarChar).Value = CNIC_Front;
                    cmd.Parameters.Add("@CNIC_Back_Path", SqlDbType.VarChar).Value = CNIC_Back;
                    cmd.Parameters.Add("@ASSISTANT_COMMISSIONERS_REPORT_Path", SqlDbType.VarChar).Value = ASSISTANT_COMMISSIONERS_REPORT_Path;
                    cmd.Parameters.Add("@MUKHTIARKAR_REPORT_Path", SqlDbType.VarChar).Value = MUKHTIARKAR_REPORT_Path;
                    cmd.Parameters.Add("@PRIMARY_CERTIFICATE_Path", SqlDbType.VarChar).Value = PRIMARY_CERTIFICATE_Path;
                    cmd.Parameters.Add("@MATRIC_CERTIFICATE_Path", SqlDbType.VarChar).Value = MATRIC_CERTIFICATE_Path;
                    cmd.Parameters.Add("@RESIDENCE_CERTIFICATE_Path", SqlDbType.VarChar).Value = RESIDENCE_CERTIFICATE_Path;
                    cmd.Parameters.Add("@VOTE_CERTIFICATE_Path", SqlDbType.VarChar).Value = VOTE_CERTIFICATE_Path;
                    cmd.Parameters.Add("@GUARDIANS_DOMICILE_Path", SqlDbType.VarChar).Value = GUARDIANS_DOMICILE_Path;
                    cmd.Parameters.Add("@BANK_CHALLANS_Path", SqlDbType.VarChar).Value = BANK_CHALLANS_Path;
                    cmd.Parameters.Add("@OTHER_DOCUMENT1_Path", SqlDbType.VarChar).Value = OTHER_DOCUMENT1_Path;
                    cmd.Parameters.Add("@OTHER_DOCUMENT2_Path", SqlDbType.VarChar).Value = OTHER_DOCUMENT2_Path;

                    cmd.Parameters.Add("@ObjectionComments", SqlDbType.VarChar).Value = ObjectionComments;
                    cmd.Parameters.Add("@DomicileApprovedDate", SqlDbType.DateTime).Value = DomicileApprovedDate;
                    cmd.Parameters.Add("@FormC_No", SqlDbType.VarChar).Value = FormC_No;
                    cmd.Parameters.Add("@FormD_No", SqlDbType.VarChar).Value = FormD_No;

                    cmd.Parameters.Add("@ApplicationId_RevisedDuplicate", SqlDbType.Int).Value = ApplicationId_RevisedDuplicate;
                    cmd.Parameters.Add("@CNICFront_Revise", SqlDbType.VarChar).Value = CNICFront_Revise;
                    cmd.Parameters.Add("@CNICBack_Revise", SqlDbType.VarChar).Value = CNICBack_Revise;
                    cmd.Parameters.Add("@Approval_Authority_Revise", SqlDbType.VarChar).Value = Approval_AuthorityRevise;
                    cmd.Parameters.Add("@BankChallan_Revise", SqlDbType.VarChar).Value = BankChallan_Revise;
                    cmd.Parameters.Add("@CorrectionDoc1_Revise", SqlDbType.VarChar).Value = CorrectionDoc1_Revise;
                    cmd.Parameters.Add("@CorrectionDoc2_Revise", SqlDbType.VarChar).Value = CorrectionDoc2_Revise;
                    cmd.Parameters.Add("@OtherDoc_Revise", SqlDbType.VarChar).Value = OtherDoc_Revise;


                    cmd.Parameters.Add("@CNICFront_Duplicate", SqlDbType.VarChar).Value = CNICFront_Duplicate;
                    cmd.Parameters.Add("@CNICBack_Duplicate", SqlDbType.VarChar).Value = CNICBack_Duplicate;
                    cmd.Parameters.Add("@FirCopy_Duplicate", SqlDbType.VarChar).Value = FirCopy_Duplicate;
                    cmd.Parameters.Add("@Approval_Authority_Duplicate", SqlDbType.VarChar).Value = Approval_AuthorityDuplicate;
                    cmd.Parameters.Add("@Bank_Challan_Duplicate", SqlDbType.VarChar).Value = BankChallan_Duplicate;
                    cmd.Parameters.Add("@Application_Duplicate", SqlDbType.VarChar).Value = Application_Duplicate;


                    cmd.Parameters.Add("@CNICFront_Cancel", SqlDbType.VarChar).Value = CNICFront_Cancel;
                    cmd.Parameters.Add("@CNICBack_Cancel", SqlDbType.VarChar).Value = CNICBack_Cancel;
                    cmd.Parameters.Add("@Residence_Cancel", SqlDbType.VarChar).Value = Residence_Cancel;
                    cmd.Parameters.Add("@Vote_Cancel", SqlDbType.VarChar).Value = Vote_Cancel;
                    cmd.Parameters.Add("@Application_Cancel", SqlDbType.VarChar).Value = Application_Cancel;
                    cmd.Parameters.Add("@Affidevit_Cancel", SqlDbType.VarChar).Value = Affidevit_Cancel;
                    cmd.Parameters.Add("@Approval_Cancel", SqlDbType.VarChar).Value = Approval_Cancel;
                    cmd.Parameters.Add("@OthersDoc_Cancel", SqlDbType.VarChar).Value = OthersDoc_Cancel;
                    cmd.Parameters.Add("@OthersDoc1_Cancel", SqlDbType.VarChar).Value = OthersDoc1_Cancel;
                    cmd.Parameters.Add("@IsByBirth", SqlDbType.Bit).Value = IsByBirth;
                    cmd.Parameters.Add("@RejectionComments", SqlDbType.VarChar).Value = RejectionComments;
                    cmd.Parameters.Add("@DeleteComments", SqlDbType.VarChar).Value = DeleteComments;

                    dt = GetData(cmd);

                    CloseConnection();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during sp_Application_Crud : {0}", ex.Message), ex);
            }

            return dt;
        }



        public DataTable usp_UpdateDocs_Application(int OperationType, int? ApplicationId, string CNIC_Front, string CNIC_Back, string ASSISTANT_COMMISSIONERS_REPORT_Path, string MUKHTIARKAR_REPORT_Path, string PRIMARY_CERTIFICATE_Path, string MATRIC_CERTIFICATE_Path, string RESIDENCE_CERTIFICATE_Path, string VOTE_CERTIFICATE_Path, string GUARDIANS_DOMICILE_Path, string BANK_CHALLANS_Path, string OTHER_DOCUMENT1_Path, string OTHER_DOCUMENT2_Path,  string OTHER_DOCUMENT3_Path, string OTHER_DOCUMENT4_Path, string OTHER_DOCUMENT5_Path,
                                                         string CNICFront_Revise, string CNICBack_Revise, string Approval_AuthorityRevise, string BankChallan_Revise, string CorrectionDoc1_Revise, string CorrectionDoc2_Revise, string OtherDoc_Revise, string OtherDoc2_Revise, string OtherDoc3_Revise, string OtherDoc4_Revise,
                                                         string CNICFront_Duplicate, string CNICBack_Duplicate, string FirCopy_Duplicate, string Approval_AuthorityDuplicate, string BankChallan_Duplicate, string Application_Duplicate, string OtherDoc1_Dup, string OtherDoc2_Dup, string OtherDoc3_Dup,
                                                         string CNICFront_Cancel, string CNICBack_Cancel, string Residence_Cancel, string Vote_Cancel, string Application_Cancel, string Affidevit_Cancel, string Approval_Cancel, string OthersDoc_Cancel, string OthersDoc1_Cancel, string OthersDoc2_Cancel, string OthersDoc3_Cancel, string OthersDoc4_Cancel,
                                                         int UserId)

        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand("Update_ApplicationDocuments"))
                {
                    OpenConnection(true);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 200;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@ApplicationId", SqlDbType.Int).Value = ApplicationId;

                    cmd.Parameters.Add("@CNIC_Front_Path", SqlDbType.VarChar).Value = CNIC_Front;
                    cmd.Parameters.Add("@CNIC_Back_Path", SqlDbType.VarChar).Value = CNIC_Back;
                    cmd.Parameters.Add("@ASSISTANT_COMMISSIONERS_REPORT_Path", SqlDbType.VarChar).Value = ASSISTANT_COMMISSIONERS_REPORT_Path;
                    cmd.Parameters.Add("@MUKHTIARKAR_REPORT_Path", SqlDbType.VarChar).Value = MUKHTIARKAR_REPORT_Path;
                    cmd.Parameters.Add("@PRIMARY_CERTIFICATE_Path", SqlDbType.VarChar).Value = PRIMARY_CERTIFICATE_Path;
                    cmd.Parameters.Add("@MATRIC_CERTIFICATE_Path", SqlDbType.VarChar).Value = MATRIC_CERTIFICATE_Path;
                    cmd.Parameters.Add("@RESIDENCE_CERTIFICATE_Path", SqlDbType.VarChar).Value = RESIDENCE_CERTIFICATE_Path;
                    cmd.Parameters.Add("@VOTE_CERTIFICATE_Path", SqlDbType.VarChar).Value = VOTE_CERTIFICATE_Path;
                    cmd.Parameters.Add("@GUARDIANS_DOMICILE_Path", SqlDbType.VarChar).Value = GUARDIANS_DOMICILE_Path;
                    cmd.Parameters.Add("@BANK_CHALLANS_Path", SqlDbType.VarChar).Value = BANK_CHALLANS_Path;
                    cmd.Parameters.Add("@OTHER_DOCUMENT1_Path", SqlDbType.VarChar).Value = OTHER_DOCUMENT1_Path;
                    cmd.Parameters.Add("@OTHER_DOCUMENT2_Path", SqlDbType.VarChar).Value = OTHER_DOCUMENT2_Path;
                    cmd.Parameters.Add("@OTHER_DOCUMENT3_Path", SqlDbType.VarChar).Value = OTHER_DOCUMENT3_Path;
                    cmd.Parameters.Add("@OTHER_DOCUMENT4_Path", SqlDbType.VarChar).Value = OTHER_DOCUMENT4_Path;
                    cmd.Parameters.Add("@OTHER_DOCUMENT5_Path", SqlDbType.VarChar).Value = OTHER_DOCUMENT5_Path;

                    cmd.Parameters.Add("@CNICFront_Revise", SqlDbType.VarChar).Value = CNICFront_Revise;
                    cmd.Parameters.Add("@CNICBack_Revise", SqlDbType.VarChar).Value = CNICBack_Revise;
                    cmd.Parameters.Add("@Approval_Authority_Revise", SqlDbType.VarChar).Value = Approval_AuthorityRevise;
                    cmd.Parameters.Add("@BankChallan_Revise", SqlDbType.VarChar).Value = BankChallan_Revise;
                    cmd.Parameters.Add("@CorrectionDoc1_Revise", SqlDbType.VarChar).Value = CorrectionDoc1_Revise;
                    cmd.Parameters.Add("@CorrectionDoc2_Revise", SqlDbType.VarChar).Value = CorrectionDoc2_Revise;
                    cmd.Parameters.Add("@OtherDoc_Revise", SqlDbType.VarChar).Value = OtherDoc_Revise;
                    cmd.Parameters.Add("@OtherDoc2_Revise", SqlDbType.VarChar).Value = OtherDoc2_Revise;
                    cmd.Parameters.Add("@OtherDoc3_Revise", SqlDbType.VarChar).Value = OtherDoc3_Revise;
                    cmd.Parameters.Add("@OtherDoc4_Revise", SqlDbType.VarChar).Value = OtherDoc4_Revise;

                    cmd.Parameters.Add("@CNICFront_Duplicate", SqlDbType.VarChar).Value = CNICFront_Duplicate;
                    cmd.Parameters.Add("@CNICBack_Duplicate", SqlDbType.VarChar).Value = CNICBack_Duplicate;
                    cmd.Parameters.Add("@FirCopy_Duplicate", SqlDbType.VarChar).Value = FirCopy_Duplicate;
                    cmd.Parameters.Add("@Approval_Authority_Duplicate", SqlDbType.VarChar).Value = Approval_AuthorityDuplicate;
                    cmd.Parameters.Add("@Bank_Challan_Duplicate", SqlDbType.VarChar).Value = BankChallan_Duplicate;
                    cmd.Parameters.Add("@Application_Duplicate", SqlDbType.VarChar).Value = Application_Duplicate;
                    cmd.Parameters.Add("@OtherDoc1_Dup", SqlDbType.VarChar).Value = OtherDoc1_Dup;
                    cmd.Parameters.Add("@OtherDoc2_Dup", SqlDbType.VarChar).Value = OtherDoc2_Dup;
                    cmd.Parameters.Add("@OtherDoc3_Dup", SqlDbType.VarChar).Value = OtherDoc3_Dup;

                    cmd.Parameters.Add("@CNICFront_Cancel", SqlDbType.VarChar).Value = CNICFront_Cancel;
                    cmd.Parameters.Add("@CNICBack_Cancel", SqlDbType.VarChar).Value = CNICBack_Cancel;
                    cmd.Parameters.Add("@Residence_Cancel", SqlDbType.VarChar).Value = Residence_Cancel;
                    cmd.Parameters.Add("@Vote_Cancel", SqlDbType.VarChar).Value = Vote_Cancel;
                    cmd.Parameters.Add("@Application_Cancel", SqlDbType.VarChar).Value = Application_Cancel;
                    cmd.Parameters.Add("@Affidevit_Cancel", SqlDbType.VarChar).Value = Affidevit_Cancel;
                    cmd.Parameters.Add("@Approval_Cancel", SqlDbType.VarChar).Value = Approval_Cancel;
                    cmd.Parameters.Add("@OthersDoc_Cancel", SqlDbType.VarChar).Value = OthersDoc_Cancel;
                    cmd.Parameters.Add("@OthersDoc1_Cancel", SqlDbType.VarChar).Value = OthersDoc1_Cancel;
                    cmd.Parameters.Add("@OthersDoc2_Cancel", SqlDbType.VarChar).Value = OthersDoc2_Cancel;
                    cmd.Parameters.Add("@OthersDoc3_Cancel", SqlDbType.VarChar).Value = OthersDoc3_Cancel;
                    cmd.Parameters.Add("@OthersDoc4_Cancel", SqlDbType.VarChar).Value = OthersDoc4_Cancel;

                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                    dt = GetData(cmd);
                    CloseConnection();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during sp_Application_Crud : {0}", ex.Message), ex);
            }

            return dt;
        }
    }
}
