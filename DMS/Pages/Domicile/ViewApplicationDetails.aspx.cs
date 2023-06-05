using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;
using System.IO;
using System.Web;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Conversion;


using SautinSoft.Document;
using RTE.Convertor.PDF;
using System.Collections.Generic;
using Spire.Pdf.General.Find;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;


public partial class Pages_Domicile_ViewApplicationDetails : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["Success"] != null)
            {
                if (Session["Success"].ToString() != "")
                {
                    divSuccess.Visible = true;
                    lblSuccessDiv.Text = Session["Success"].ToString();
                    Session.Remove("Success");
                }
            }

            SetFeature();
            if (Request.QueryString["ApplicationId"] != null && Request.QueryString["ApplicationId"].ToString() != "" &&
                Request.QueryString["RequestTypeId"] != null && Request.QueryString["RequestTypeId"].ToString() != "" &&
                Request.QueryString["StatusId"] != null && Request.QueryString["StatusId"].ToString() != "")
            {
                hdnApplicationID.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["ApplicationId"].ToString()));
                hdnRequestTypeId.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["RequestTypeId"].ToString()));
                hdnStatusId.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["StatusId"].ToString()));

                GetApplicationByID(Convert.ToInt32(hdnApplicationID.Value));
                Show_Hide_Controls();
                DisableControls();

            }
        }

    }



    protected void Upload(object sender, EventArgs e)
    {
        try
        {
            // New Application CASE

            UploadDocuments(FileUploadCNICFront, lblMessageCNICFront, lnkCNICFront, rfvCNICFront, "CNIC_Front");
            UploadDocuments(FileUploadCNICBack, lblMessageCNICBack, lblCNICBack, rfvCNICBack, "CNIC_Back");
            UploadDocuments(FileUpload_AsstComm, lblMsgCommReport, lblCommReport, rfvAsstComm, "Comm_Report");
            UploadDocuments(FileUpload4, Label2, lblRefMUKHTIARKAR, RequiredFieldValidator3, "MUKHTIARKAR’S_REPORT");
            UploadDocuments(FileUpload5, Label3, lblRefPrimaryCertificate, null, "PRIMARY_CERTIFICATE");
            UploadDocuments(FileUpload6, Label4, lblRefMatricCertificate, null, "MATRIC_CERTIFICATE");
            UploadDocuments(FileUpload7, Label5, lblRefRESIDENCECERTIFICATE, RequiredFieldValidator4, "RESIDENCE_CERTIFICATE");
            UploadDocuments(FileUpload8, Label6, lblVote, RequiredFieldValidator5, "VOTE_CERTIFICATE");
            UploadDocuments(FileUpload9, Label7, lblGUARDIANDomicile, null, "GUARDIANS_DOMICILE");
            UploadDocuments(FileUpload10, Label8, lblBankChallan, RequiredFieldValidator6, "BANK CHALLANS");
            UploadDocuments(FileUpload11, Label9, lblOtherDoc1, null, "Other_Document_1");
            UploadDocuments(FileUpload12, Label10, lblOtherDoc2, null, "Other_Document_2");
            UploadDocuments(FileOtherDoc3, lblOtherDoc3, ImgOtherDoc3, null, "Other_Document_3");
            UploadDocuments(FileOtherDoc4, lblOtherDoc4, ImgOtherDoc4, null, "Other_Document_4");
            UploadDocuments(FileOtherDoc5, lblOtherDoc5, ImgOtherDoc5, null, "Other_Document_5");





            // Duplicate Application CASE

            UploadDocuments(FileUploadCNICFront_Dup, lblMsgCNICFront, ImgCNICFront, RequiredFieldValidator11, "CNIC_Front");
            UploadDocuments(FileUploadCNICBack_Dup, lblMsgCNICBack, ImgCNICBack, RequiredFieldValidator14, "CNIC_Back");
            UploadDocuments(FileUploadFIR_Dup, lblMsgFir, ImgFir, RequiredFieldValidator15, "NC/ FIR COPY");
            UploadDocuments(FileUploadAuthority_Dup, lblApproval, ImgApproval, RequiredFieldValidator16, "APPROVAL OF AUTHORITY");
            UploadDocuments(FileUploadChallan_Dup, lblBanlChallan, ImgBankChallan, RequiredFieldValidator17, "BANK CHALLAN");
            UploadDocuments(FileUploadApp_Dup, lblApp, ImgApp, null, "Application");

            UploadDocuments(FileDupOther1, lblDupOther1, ImgDupOther1, null, "Other Doc 1");
            UploadDocuments(FileDupOther2, lblDupOther2, ImgDupOther2, null, "Other Doc 2");
            UploadDocuments(FileDupOther3, lblDupOther3, ImgDupOther3, null, "Other Doc 3");

            // Duplicate Application CASE



            // Revision Application CASE

            UploadDocuments(FileUpload13, Label12, Label11, RequiredFieldValidator7, "CNIC_Front");
            UploadDocuments(FileUpload14, Label14, Label13, RequiredFieldValidator8, "CNIC_Back");
            UploadDocuments(FileUpload15, Label16, Label15, RequiredFieldValidator9, "APPROVAL_AUTHORITY");
            UploadDocuments(FileUpload16, Label18, Label17, RequiredFieldValidator10, "BANK_CHALLAN");
            UploadDocuments(FileUpload17, Label20, Label19, RequiredFieldValidator12, "CORRECTION_DOCUMENT1");
            UploadDocuments(FileUpload18, Label22, Label21, RequiredFieldValidator13, "CORRECTION_DOCUMENT2");
            UploadDocuments(FileUpload19, Label24, Label23, null, "OTHERS");


            UploadDocuments(FileOtherRev2, lblOtherRev2, ImgOtherRev2, null, "Other Doc 1");
            UploadDocuments(FileOtherRev3, lblOtherRev3, ImgOtherRev3, null, "Other Doc 2");
            UploadDocuments(FileOtherRev4, lblOtherRev4, ImgOtherRev4, null, "Other Doc 3");

            // Revision Application CASE


            // Cancellation Application CASE

            UploadDocuments(FileCNICFront_Cancel, Label38, Label37, RequiredFieldValidator19, "CNIC_Front");
            UploadDocuments(FileCNICBack_Cancel, Label40, Label39, RequiredFieldValidator20, "CNIC_Back");
            UploadDocuments(FileResidence_Cancel, Label42, Label41, null, "Residence");
            UploadDocuments(FileVote_Cancel, Label44, Label43, null, "Vote");
            UploadDocuments(FileApplication_Cancel, Label46, Label45, null, "Application");
            UploadDocuments(FileAffidevit_Cancel, Label48, Label47, null, "Affidevit");
            UploadDocuments(FileApproval_Cancel, Label50, Label49, RequiredFieldValidator25, "Approval");
            UploadDocuments(FileOtherDoc1_Cancel, Label52, Label51, null, "Other Doc 1");
            UploadDocuments(FileOtherDoc2_Cancel, Label54, Label53, null, "Other Doc 2");

            UploadDocuments(FileCancelOther3, lblCancelOther3, ImgCancelOther3, null, "Other Doc 3");
            UploadDocuments(FileCancelOther4, lblCancelOther4, ImgCancelOther4, null, "Other Doc 4");
            UploadDocuments(FileCancelOther5, lblCancelOther5, ImgCancelOther5, null, "Other Doc 5");

            // Cancellation Application CASE

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewApplicationDetails.aspx", "Upload", ex.Message);
        }

    }

    public void UploadDocuments(FileUpload file, Label Message, ImageButton Img, RequiredFieldValidator Rfv, string FileType)
    {
        if (file.HasFile)
        {
            if (CheckFileType(file))
            {
                int maxFileSize = 10;
                long FileSize = file.FileContent.Length / 1024;
                if (FileSize > (maxFileSize * 1024))
                {
                    Message.Visible = true;
                    Message.Text = "Please Upload Image up to 10MB.";

                    file.Focus();
                }

                else
                {
                    // string folderPath = Server.MapPath("~/Applicant_Documents/");

                    string ApplicationId = hdnApplicationID.Value;
                    string folderPath = Server.MapPath("~/Applicant_Documents/" + ApplicationId + "/");

                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists Create it.
                        Directory.CreateDirectory(folderPath);
                    }
                    // string ApplicationId = ViewState["ApplicationId_PopUp"].ToString();
                    string FileName = DateTime.Now.ToString("dd-MMMM-yyyy") + "_" + FileType + "_" + ApplicationId + "_" + file.FileName;
                    //Save the File to the Directory (Folder).

                    if (Rfv != null)
                        Rfv.Enabled = false;

                    file.SaveAs(folderPath + Path.GetFileName(FileName));
                    //Display the Picture in Image control.
                    Img.Visible = true;
                    ViewState["ApplicantPhoto_Path"] = "~/Applicant_Documents/" + ApplicationId + "/" + Path.GetFileName(FileName);
                    Img.ImageUrl = "~/Applicant_Documents/" + ApplicationId + "/" + Path.GetFileName(FileName);
                    Message.Visible = false;



                    file.Focus();
                }
            }
            else
            {
                Message.Visible = true;
                Message.Text = "You can upload ony jpg , jpeg , png files.";
                Img.ImageUrl = "";


                file.Focus();
            }
        }

    }


    public void LoadDocumens(ImageButton Img, RequiredFieldValidator Rfv, string URL)
    {

        if (Rfv != null)
            Rfv.Enabled = false;

        if (URL != "")
            Img.Visible = true;

        Img.ImageUrl = URL;
    }

    protected void btnImageView_Click(object sender, EventArgs e)

    {
        ImageButton button = (ImageButton)sender;
        string buttonId = button.ID;

        if (buttonId == "lnkCNICFront")
        {
            hdnImageURL.Value = lnkCNICFront.ImageUrl;
        }
        if (buttonId == "lblCNICBack")
        {
            hdnImageURL.Value = lblCNICBack.ImageUrl;
        }
        if (buttonId == "lblCommReport")
        {
            hdnImageURL.Value = lblCommReport.ImageUrl;
        }
        if (buttonId == "lblRefMUKHTIARKAR")
        {
            hdnImageURL.Value = lblRefMUKHTIARKAR.ImageUrl;
        }
        if (buttonId == "lblRefPrimaryCertificate")
        {
            hdnImageURL.Value = lblRefPrimaryCertificate.ImageUrl;
        }
        if (buttonId == "lblRefMatricCertificate")
        {
            hdnImageURL.Value = lblRefMatricCertificate.ImageUrl;
        }
        if (buttonId == "lblRefRESIDENCECERTIFICATE")
        {
            hdnImageURL.Value = lblRefRESIDENCECERTIFICATE.ImageUrl;
        }
        if (buttonId == "lblVote")
        {
            hdnImageURL.Value = lblVote.ImageUrl;
        }
        if (buttonId == "lblGUARDIANDomicile")
        {
            hdnImageURL.Value = lblGUARDIANDomicile.ImageUrl;
        }
        if (buttonId == "lblBankChallan")
        {
            hdnImageURL.Value = lblBankChallan.ImageUrl;
        }
        if (buttonId == "lblOtherDoc1")
        {
            hdnImageURL.Value = lblOtherDoc1.ImageUrl;
        }
        if (buttonId == "lblOtherDoc2")
        {
            hdnImageURL.Value = lblOtherDoc2.ImageUrl;
        }
        if (buttonId == "ImgOtherDoc3")
        {
            hdnImageURL.Value = ImgOtherDoc3.ImageUrl;
        }
        if (buttonId == "ImgOtherDoc4")
        {
            hdnImageURL.Value = ImgOtherDoc4.ImageUrl;
        }
        if (buttonId == "ImgOtherDoc5")
        {
            hdnImageURL.Value = ImgOtherDoc5.ImageUrl;
        }



        // Revise Docs View

        if (buttonId == "Label11")
        {
            hdnImageURL.Value = Label11.ImageUrl;
        }
        if (buttonId == "Label13")
        {
            hdnImageURL.Value = Label13.ImageUrl;
        }
        if (buttonId == "Label15")
        {
            hdnImageURL.Value = Label15.ImageUrl;
        }
        if (buttonId == "Label17")
        {
            hdnImageURL.Value = Label17.ImageUrl;
        }
        if (buttonId == "Label19")
        {
            hdnImageURL.Value = Label19.ImageUrl;
        }
        if (buttonId == "Label21")
        {
            hdnImageURL.Value = Label21.ImageUrl;
        }
        if (buttonId == "Label23")
        {
            hdnImageURL.Value = Label23.ImageUrl;
        }

        if (buttonId == "ImgOtherRev2")
        {
            hdnImageURL.Value = ImgOtherRev2.ImageUrl;
        }

        if (buttonId == "ImgOtherRev3")
        {
            hdnImageURL.Value = ImgOtherRev3.ImageUrl;
        }

        if (buttonId == "ImgOtherRev4")
        {
            hdnImageURL.Value = ImgOtherRev4.ImageUrl;
        }

        // Revise Docs View



        // Duplicate Docs View

        if (buttonId == "ImgCNICFront")
        {
            hdnImageURL.Value = ImgCNICFront.ImageUrl;
        }
        if (buttonId == "ImgCNICBack")
        {
            hdnImageURL.Value = ImgCNICBack.ImageUrl;
        }

        if (buttonId == "ImgFir")
        {
            hdnImageURL.Value = ImgFir.ImageUrl;
        }
        if (buttonId == "ImgApproval")
        {
            hdnImageURL.Value = ImgApproval.ImageUrl;
        }

        if (buttonId == "ImgBankChallan")
        {
            hdnImageURL.Value = ImgBankChallan.ImageUrl;
        }
        if (buttonId == "ImgApp")
        {
            hdnImageURL.Value = ImgApp.ImageUrl;
        }

        if (buttonId == "ImgDupOther1")
        {
            hdnImageURL.Value = ImgDupOther1.ImageUrl;
        }

        if (buttonId == "ImgDupOther2")
        {
            hdnImageURL.Value = ImgDupOther2.ImageUrl;
        }

        if (buttonId == "ImgDupOther3")
        {
            hdnImageURL.Value = ImgDupOther3.ImageUrl;
        }

        // Duplicate Docs View



        // Cancellation Docs View

        if (buttonId == "Label37")
        {
            hdnImageURL.Value = Label37.ImageUrl;
        }
        if (buttonId == "Label39")
        {
            hdnImageURL.Value = Label39.ImageUrl;
        }

        if (buttonId == "Label41")
        {
            hdnImageURL.Value = Label41.ImageUrl;
        }
        if (buttonId == "Label43")
        {
            hdnImageURL.Value = Label43.ImageUrl;
        }

        if (buttonId == "Label45")
        {
            hdnImageURL.Value = Label45.ImageUrl;
        }
        if (buttonId == "Label47")
        {
            hdnImageURL.Value = Label47.ImageUrl;
        }

        if (buttonId == "Label49")
        {
            hdnImageURL.Value = Label49.ImageUrl;
        }

        if (buttonId == "Label51")
        {
            hdnImageURL.Value = Label51.ImageUrl;
        }

        if (buttonId == "Label53")
        {
            hdnImageURL.Value = Label53.ImageUrl;
        }



        if (buttonId == "ImgCancelOther3")
        {
            hdnImageURL.Value = ImgCancelOther3.ImageUrl;
        }


        if (buttonId == "ImgCancelOther4")
        {
            hdnImageURL.Value = ImgCancelOther4.ImageUrl;
        }


        if (buttonId == "ImgCancelOther5")
        {
            hdnImageURL.Value = ImgCancelOther5.ImageUrl;
        }

        // Cancellation Docs View


        ImgDoc.ImageUrl = hdnImageURL.Value;
        OpenPopupImage();
    }

    public void OpenPopupImage()
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "OpenImageModel()", "OpenImageModel();", true);
    }

    private bool CheckFileType(FileUpload file)
    {
        string filePath = file.PostedFile.FileName;
        string filename = Path.GetFileName(filePath);
        string ext = Path.GetExtension(filename);
        string contenttype = String.Empty;
        Stream checkStream = file.PostedFile.InputStream;
        BinaryReader chkBinary = new BinaryReader(checkStream);
        Byte[] chkbytes = chkBinary.ReadBytes(0x10);

        string data_as_hex = BitConverter.ToString(chkbytes);
        string magicCheck = data_as_hex.Substring(0, 11);

        //Set the contenttype based on File Extension
        switch (magicCheck)
        {
            case "FF-D8-FF-E1":
                contenttype = "image/jpg";
                break;
            case "FF-D8-FF-E0":
                contenttype = "image/jpeg";
                break;
            case "89-50-4E-47":
                contenttype = "image/png";
                break;
        }

        if (contenttype != String.Empty)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public void GetApplicationByID(int ApplicationId)
    {
        try
        {

            DataTable dt = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, ApplicationId, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    null, null, null, null, null, null, null, null, null, null,
                                                                                    null, null, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ApplicationTypeId"].ToString() == ApplicationTypeValueId.Minor.ToString())
                {
                    ViewState["IsMinor"] = "true";
                }
                lblARNNumber.Text = dt.Rows[0]["Application_RefNo"].ToString();
                ViewState["ARN_Number"] = dt.Rows[0]["Application_RefNo"].ToString();
                lblTitle.Text = dt.Rows[0]["Title"].ToString();
                lblName.Text = dt.Rows[0]["Applicant_Name"].ToString();
                lblFatherName.Text = dt.Rows[0]["Father_Name"].ToString();

                lblCNIC.Text = dt.Rows[0]["Applicant_Cnic"].ToString();
                lblDoB.Text = Convert.ToDateTime(dt.Rows[0]["Date_of_Birth"].ToString()).ToString("D", CultureInfo.CreateSpecificCulture("en-US"));

                DateTime Dob = Convert.ToDateTime(lblDoB.Text).Date;
                //int age = 0;
                //age = DateTime.Now.Subtract(Dob).Days;
                //age = age / 365;

                var today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (Dob.Year * 100 + Dob.Month) * 100 + Dob.Day;

                int age = (a - b) / 10000;

                lblAge.Text = age.ToString();

                lblPlaceofBirth.Text = dt.Rows[0]["Place_of_Birth"].ToString();
                lblResidentof.Text = dt.Rows[0]["Resident_of"].ToString();

                lblCast.Text = dt.Rows[0]["Surname"].ToString();
                Imgphoto.ImageUrl = dt.Rows[0]["Applicant_Photo_Path"].ToString();
                ViewState["ProfileImage"] = dt.Rows[0]["Applicant_Photo_Path"].ToString();
                lblDateDelivery.Text = CalculateDeliveryDate(dt.Rows[0]["CreatedDate"].ToString());

                lblPrimarySchool.Text = dt.Rows[0]["Primary_School"].ToString() == "" ? "N/A" : dt.Rows[0]["Primary_School"].ToString();
                lblTownVillage.Text = dt.Rows[0]["Town_PrimarySchool"].ToString() == "" ? "N/A" : dt.Rows[0]["Town_PrimarySchool"].ToString();
                lblFromDatePrimary.Text = dt.Rows[0]["FromDate_PrimarySchool"].ToString() == "" ? "N/A" : dt.Rows[0]["FromDate_PrimarySchool"].ToString();
                lblToDatePrimary.Text = dt.Rows[0]["ToDate_PrimarySchool"].ToString() == "" ? "N/A" : dt.Rows[0]["ToDate_PrimarySchool"].ToString();


                lblMiddleSchool.Text = dt.Rows[0]["Middle_School"].ToString() == "" ? "N/A" : dt.Rows[0]["Middle_School"].ToString();
                lblMiddleTownVillage.Text = dt.Rows[0]["Town_MiddleSchool"].ToString() == "" ? "N/A" : dt.Rows[0]["Town_MiddleSchool"].ToString();
                lblFromDateMiddle.Text = dt.Rows[0]["FromDate_MiddleSchool"].ToString() == "" ? "N/A" : dt.Rows[0]["FromDate_MiddleSchool"].ToString();
                lblToDateMiddle.Text = dt.Rows[0]["ToDate_MiddleSchool"].ToString() == "" ? "N/A" : dt.Rows[0]["ToDate_MiddleSchool"].ToString();


                lblHighSchool.Text = dt.Rows[0]["High_School"].ToString() == "" ? "N/A" : dt.Rows[0]["High_School"].ToString();
                lblHighTownVillage.Text = dt.Rows[0]["Town_HighSchool"].ToString() == "" ? "N/A" : dt.Rows[0]["Town_HighSchool"].ToString();
                lblFromDateHigh.Text = dt.Rows[0]["FromDate_HighSchool"].ToString() == "" ? "N/A" : dt.Rows[0]["FromDate_HighSchool"].ToString();
                lblToDateHigh.Text = dt.Rows[0]["ToDate_HighSchool"].ToString() == "" ? "N/A" : dt.Rows[0]["ToDate_HighSchool"].ToString();

                lblDistrictEducation.Text = dt.Rows[0]["District_Education"].ToString() == "" ? "N/A" : dt.Rows[0]["District_Education"].ToString();

                DateTime DateSubmission = Convert.ToDateTime(dt.Rows[0]["Date_Submission"].ToString()).Date;
                lblDateSubmission.Text = DateSubmission.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));

                DateTime Date_Issue = Convert.ToDateTime(dt.Rows[0]["Date_Issue"].ToString()).Date;
                lblDateIssue.Text = Date_Issue.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));

                lblParticualrsPropoerty.Text = dt.Rows[0]["ParticularsProperty"].ToString() == "" ? "N/A" : dt.Rows[0]["ParticularsProperty"].ToString();

                lblDistrict.Text = dt.Rows[0]["District"].ToString() == "" ? "N/A" : dt.Rows[0]["District"].ToString();
                lblElectoralArea.Text = dt.Rows[0]["Electoral_Area"].ToString() == "" ? "N/A" : dt.Rows[0]["Electoral_Area"].ToString();
                lblLocation.Text = dt.Rows[0]["Location"].ToString() == "" ? "N/A" : dt.Rows[0]["Location"].ToString();

                int TalukaId = Convert.ToInt32(dt.Rows[0]["TalukaId"].ToString());
                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, TalukaId, null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }

                lblTaluka.Text = TalukaName;


                lblElectoralTaluka.Text = dt.Rows[0]["Electoral_Area_Taluka"].ToString() == "" ? "N/A" : dt.Rows[0]["Electoral_Area_Taluka"].ToString();
                lblGuardianCNIC.Text = dt.Rows[0]["Guardian_NIC"].ToString() == "" ? "N/A" : dt.Rows[0]["Guardian_NIC"].ToString();

                string GuardianRelation = "N/A";

                int GuardianRelationID = Convert.ToInt32(dt.Rows[0]["Guardian_RelationShip"].ToString());

                if (GuardianRelationID != 0)
                {
                    DataTable dt_Guarduan = new BAL_Guardian().usp_tblGuardian(OperationTypesID.Select, GuardianRelationID);
                    if (dt_Guarduan != null && dt_Guarduan.Rows.Count > 0)
                    {
                        GuardianRelation = dt_Guarduan.Rows[0]["GuardianRelationName"].ToString();
                    }
                }

                lblGuardianRelationShip.Text = GuardianRelation;


                int DehID = Convert.ToInt32(dt.Rows[0]["Electoreal_Area_Deh"].ToString());
                DataTable dt_Deh = new BAL_Deh().usp_Setup_Deh(OperationTypesID.Select, DehID, null, null, UserId, UserIP);
                string DehName = "";
                if (dt_Deh != null && dt_Deh.Rows.Count > 0)
                {
                    DehName = dt_Deh.Rows[0]["DehName"].ToString();
                }
                lblElectoralAreaDeh.Text = DehName == "" ? "N/A" : DehName;

                lblApplicantPhone.Text = dt.Rows[0]["Applicant_PhoneNo"].ToString() == "" ? "N/A" : dt.Rows[0]["Applicant_PhoneNo"].ToString();
                lblTempAddress.Text = dt.Rows[0]["Temporary_Address"].ToString() == "" ? "N/A" : dt.Rows[0]["Temporary_Address"].ToString();
                lblPermanentAddress.Text = dt.Rows[0]["Permanent_Address"].ToString() == "" ? "N/A" : dt.Rows[0]["Permanent_Address"].ToString();
                lblGuardianDomicileDate.Text = dt.Rows[0]["GuardianDomicile_CertificateDate"].ToString() == "1/1/0001 12:00:00 AM" ? "N/A" : Convert.ToDateTime(dt.Rows[0]["GuardianDomicile_CertificateDate"].ToString()).ToString("dd-MM-yyyy");

                string GuardianRelation2 = "N/A";
                int GuardianRelationID2 = Convert.ToInt32(dt.Rows[0]["Guardian_RelationShip2"].ToString());
                if (GuardianRelationID2 != 0)
                {
                    DataTable dt_Guarduan2 = new BAL_Guardian().usp_tblGuardian(OperationTypesID.Select, GuardianRelationID2);
                    if (dt_Guarduan2 != null && dt_Guarduan2.Rows.Count > 0)
                    {
                        GuardianRelation2 = dt_Guarduan2.Rows[0]["GuardianRelationName"].ToString();
                    }
                }
                lblGuardianRelationShip2.Text = GuardianRelation2;
                //      lblAge.Text = dt.Rows[0]["Applicant_Age"].ToString();
                lblTrade.Text = dt.Rows[0]["Trade_Occupation"].ToString() == "" ? "N/A" : dt.Rows[0]["Trade_Occupation"].ToString();

                lblMarkIdentification.Text = dt.Rows[0]["Mark_of_Identification"].ToString() == "" ? "N/A" : dt.Rows[0]["Mark_of_Identification"].ToString();
                lblDateArrival.Text = dt.Rows[0]["Date_of_Arrival"].ToString() == "1/1/0001 12:00:00 AM" ? "N/A" : Convert.ToDateTime(dt.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd-MM-yyyy");


                if (dt.Rows[0]["Is_ByBirth"].ToString().ToString() == "True")
                    lblByBirth.Text = "Yes";

                else
                    lblByBirth.Text = "No";

                lblAddressForeign.Text = dt.Rows[0]["Address_ForeignCountry"].ToString() == "" ? "N/A" : dt.Rows[0]["Address_ForeignCountry"].ToString();
                lblMukhtiarkar.Text = dt.Rows[0]["Mukhtiarkar_Taluka"].ToString() == "" ? "N/A" : dt.Rows[0]["Mukhtiarkar_Taluka"].ToString();
                lblDeputyDistrict.Text = dt.Rows[0]["Deputy_District_Officer_Taluka"].ToString() == "" ? "N/A" : dt.Rows[0]["Deputy_District_Officer_Taluka"].ToString();
                lblObjectionComments.Text = dt.Rows[0]["Approver_ObjectionComments"].ToString() == "" ? "N/A" : dt.Rows[0]["Approver_ObjectionComments"].ToString();
                lblRejectionComments.Text = dt.Rows[0]["RejectionComments"].ToString() == "" ? "N/A" : dt.Rows[0]["RejectionComments"].ToString();

                string MaritalStatus = dt.Rows[0]["Marital_Status"].ToString();
                string Marital = "";
                if (MaritalStatus == "1")
                    Marital = "Single";
                else if (MaritalStatus == "2")
                    Marital = "Married";
                else if (MaritalStatus == "3")
                    Marital = "Widow";
                else if (MaritalStatus == "4")
                    Marital = "Divorced";

                lblMaritalStatus.Text = Marital;
                lblHusbandName.Text = dt.Rows[0]["Husband_Wife_Name"].ToString() == "" ? "N/A" : dt.Rows[0]["Husband_Wife_Name"].ToString();

                DataTable dtChild = new BAL_Application().usp_Application(OperationTypesID.GetChildByApplicationId, ApplicationId, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               null, null, null, null, null, null, null, null, null, null,
                                                                               null, null, null);

                if (dtChild != null && dtChild.Rows.Count > 0)
                {
                    divChild.Visible = true;
                    rptChild.DataSource = dtChild;
                    rptChild.DataBind();
                }


                if (hdnStatusId.Value != StatusId.InitialDraft.ToString())
                {

                    // Load New Application Docs

                    LoadDocumens(lnkCNICFront, rfvCNICFront, dt.Rows[0]["CNIC_Front"].ToString());
                    LoadDocumens(lblCNICBack, rfvCNICBack, dt.Rows[0]["CNIC_Back"].ToString());
                    LoadDocumens(lblCommReport, rfvAsstComm, dt.Rows[0]["ASSISTANT_COMMISSIONERS_REPORT_Path"].ToString());
                    LoadDocumens(lblRefMUKHTIARKAR, RequiredFieldValidator3, dt.Rows[0]["MUKHTIARKAR_REPORT_Path"].ToString());
                    LoadDocumens(lblRefPrimaryCertificate, null, dt.Rows[0]["PRIMARY_CERTIFICATE_Path"].ToString());
                    LoadDocumens(lblRefMatricCertificate, null, dt.Rows[0]["MATRIC_CERTIFICATE_Path"].ToString());
                    LoadDocumens(lblRefRESIDENCECERTIFICATE, RequiredFieldValidator4, dt.Rows[0]["RESIDENCE_CERTIFICATE_Path"].ToString());
                    LoadDocumens(lblVote, RequiredFieldValidator5, dt.Rows[0]["VOTE_CERTIFICATE_Path"].ToString());
                    LoadDocumens(lblGUARDIANDomicile, rfvCNICFront, dt.Rows[0]["GUARDIANS_DOMICILE_Path"].ToString());
                    LoadDocumens(lblBankChallan, RequiredFieldValidator6, dt.Rows[0]["BANK_CHALLANS_Path"].ToString());
                    LoadDocumens(lblOtherDoc1, null, dt.Rows[0]["OTHER_DOCUMENT1_Path"].ToString());
                    LoadDocumens(lblOtherDoc2, null, dt.Rows[0]["OTHER_DOCUMENT2_Path"].ToString());
                    LoadDocumens(ImgOtherDoc3, null, dt.Rows[0]["OTHER_DOCUMENT3_Path"].ToString());
                    LoadDocumens(ImgOtherDoc4, null, dt.Rows[0]["OTHER_DOCUMENT4_Path"].ToString());
                    LoadDocumens(ImgOtherDoc5, null, dt.Rows[0]["OTHER_DOCUMENT5_Path"].ToString());
                    // Load New Application Docs


                    // Load Duplicate Application Docs

                    LoadDocumens(ImgCNICFront, RequiredFieldValidator11, dt.Rows[0]["CNICFront_Duplicate"].ToString());
                    LoadDocumens(ImgCNICBack, RequiredFieldValidator14, dt.Rows[0]["CNICBack_Duplicate"].ToString());
                    LoadDocumens(ImgFir, RequiredFieldValidator15, dt.Rows[0]["FirCopy_Duplicate"].ToString());
                    LoadDocumens(ImgApproval, RequiredFieldValidator16, dt.Rows[0]["Approval_Authority_Duplicate"].ToString());
                    LoadDocumens(ImgBankChallan, RequiredFieldValidator17, dt.Rows[0]["Bank_Challan_Duplicate"].ToString());
                    LoadDocumens(ImgApp, null, dt.Rows[0]["Application_Duplicate"].ToString());

                    LoadDocumens(ImgDupOther1, null, dt.Rows[0]["OtherDoc1_Duplicate"].ToString());
                    LoadDocumens(ImgDupOther2, null, dt.Rows[0]["OtherDoc2_Duplicate"].ToString());
                    LoadDocumens(ImgDupOther3, null, dt.Rows[0]["OtherDoc3_Duplicate"].ToString());

                    // Load Duplicate Application Docs


                    // Load Revise Application Docs

                    LoadDocumens(Label11, RequiredFieldValidator7, dt.Rows[0]["CNICFront_Revise"].ToString());
                    LoadDocumens(Label13, RequiredFieldValidator8, dt.Rows[0]["CNICBack_Revise"].ToString());
                    LoadDocumens(Label15, RequiredFieldValidator9, dt.Rows[0]["Approval_Authority_Revise"].ToString());
                    LoadDocumens(Label17, RequiredFieldValidator10, dt.Rows[0]["Bank_Challan_Revise"].ToString());
                    LoadDocumens(Label19, RequiredFieldValidator12, dt.Rows[0]["Correction_Doc1_Revise"].ToString());
                    LoadDocumens(Label21, RequiredFieldValidator13, dt.Rows[0]["Correction_Doc2_Revise"].ToString());
                    LoadDocumens(Label23, null, dt.Rows[0]["OthersDoc_Revise"].ToString());

                    LoadDocumens(ImgOtherRev2, null, dt.Rows[0]["OthersDoc2_Revise"].ToString());
                    LoadDocumens(ImgOtherRev3, null, dt.Rows[0]["OthersDoc3_Revise"].ToString());
                    LoadDocumens(ImgOtherRev4, null, dt.Rows[0]["OthersDoc4_Revise"].ToString());

                    // Load Revise Application Docs


                    // Load Cancel Application Docs

                    LoadDocumens(Label37, RequiredFieldValidator19, dt.Rows[0]["CNICFront_Cancel"].ToString());
                    LoadDocumens(Label39, RequiredFieldValidator20, dt.Rows[0]["CNICBack_Cancel"].ToString());
                    LoadDocumens(Label41, null, dt.Rows[0]["Residence_Cancel"].ToString());
                    LoadDocumens(Label43, null, dt.Rows[0]["Vote_Cancel"].ToString());
                    LoadDocumens(Label45, null, dt.Rows[0]["Application_Cancel"].ToString());
                    LoadDocumens(Label47, RequiredFieldValidator25, dt.Rows[0]["Affidevit_Cancel"].ToString());
                    LoadDocumens(Label49, null, dt.Rows[0]["Approval_Cancel"].ToString());
                    LoadDocumens(Label51, null, dt.Rows[0]["OthersDoc_Cancel"].ToString());
                    LoadDocumens(Label53, null, dt.Rows[0]["OthersDoc1_Cancel"].ToString());


                    LoadDocumens(ImgCancelOther3, null, dt.Rows[0]["OthersDoc2_Cancel"].ToString());
                    LoadDocumens(ImgCancelOther4, null, dt.Rows[0]["OthersDoc3_Cancel"].ToString());
                    LoadDocumens(ImgCancelOther5, null, dt.Rows[0]["OthersDoc4_Cancel"].ToString());

                    // Load Cancel Application Docs


                }

                if (hdnStatusId.Value == StatusId.Issued.ToString() || hdnStatusId.Value == StatusId.Disable.ToString())
                {
                    GetApplicationReceiverDetail();
                }


                GetApplicationDomicileHistory();
            }

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewApplicationDetails.aspx", "GetApplicationByID", ex.Message);
        }

    }


    private string CalculateDeliveryDate(string CreatedDate)
    {

        DataTable dtDeliveryDay = new BAL_DeliveryDateDays().usp_Setup_DeliveryDateDays(OperationTypesID.Select, null, null, null, null);
        int TotalDeliveryDay = Convert.ToInt32(dtDeliveryDay.Rows[0]["DeliveryDateDays"].ToString());
        DateTime DeliveryDay = Convert.ToDateTime(CreatedDate).AddDays(TotalDeliveryDay);
        string Day = DeliveryDay.DayOfWeek.ToString();

        if (Day == "Saturday")
            DeliveryDay = DeliveryDay.AddDays(2);

        if (Day == "Sunday")
            DeliveryDay = DeliveryDay.AddDays(1);

        return DeliveryDay.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
    }

    protected void btnIssueDomicile_Click(object sender, EventArgs e)
    {
        if (IsView.Value == "1")
        {
            divDomicilePrint.Visible = true;
            btnDomicile.Visible = true;
            btnDomicileExcel.Visible = true;

            IssuedDocumentsRecord(DocumentType.Domicile);
            if (hdnRequestTypeId.Value == RequestTypeID.Duplication.ToString())
                DuplicateDocumentsRecord(DocumentType.Domicile);

            btnIssueDomicile.Visible = false;
        }
    }


    protected void btnIssueFormC_Click(object sender, EventArgs e)
    {
        if (IsView.Value == "1")
        {
            divDomicilePrint.Visible = true;
            btnFormC.Visible = true;
            btnFormCExcel.Visible = true;

            IssuedDocumentsRecord(DocumentType.FormC);
            if (hdnRequestTypeId.Value == RequestTypeID.Duplication.ToString())
                DuplicateDocumentsRecord(DocumentType.FormC);

            btnIssueFormC.Visible = false;
        }
    }

    protected void btnIssueFormD_Click(object sender, EventArgs e)
    {
        if (IsView.Value == "1")
        {
            divDomicilePrint.Visible = true;
            btnFormD.Visible = true;
            btnFormDExcel.Visible = true;

            IssuedDocumentsRecord(DocumentType.FormD);
            if (hdnRequestTypeId.Value == RequestTypeID.Duplication.ToString())
                DuplicateDocumentsRecord(DocumentType.FormD);

            btnIssueFormD.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int StatusID = 0;

            if (ddlStatus.SelectedValue == StatusId.Rejected.ToString())
            {
                if (Rejection())
                {
                    Session["Success"] = "ARN Number " + ViewState["ARN_Number"] + " Rejected Successfully";
                    StatusID = StatusId.Rejected;
                }
            }
            else if (ddlStatus.SelectedValue == StatusId.Objected.ToString())
            {
                if (Objection())
                {
                    Session["Success"] = "ARN Number " + ViewState["ARN_Number"] + " Objected Successfully";
                    StatusID = StatusId.Objected;
                }
            }
            else if (ddlStatus.SelectedValue == StatusId.Approved.ToString())
            {
                if (ViewState["ProfileImage"] != null && ViewState["ProfileImage"].ToString() == "")
                {
                    Error("This Application Can not be Approved . Applicant Profile Picture is missing");
                    return;
                }

                if (Approve())
                {
                    Session["Success"] = "ARN Number " + ViewState["ARN_Number"] + " Approved Successfully";
                    StatusID = StatusId.Approved;
                }
            }

            DataTable dtStatus = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, StatusID, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            UserId, UserIP, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, DateTime.Now, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null);

            if (StatusID == StatusId.Approved)
            {
                Response.Redirect("ViewApplicationDetails.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationID.Value)) +
                    "&StatusId=" + Server.UrlEncode(Encryption.Encrypt(StatusId.Approved.ToString())) + "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestTypeId.Value)));
            }
            else
                Response.Redirect("ViewApplicationSaved.aspx?ApplicationStatusId=" + StatusID);

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnSubmit_Click", ex.Message);
        }

    }

    private bool Objection()
    {

        DataTable dt = new BAL_Application().usp_Application(OperationTypesID.UpdateApplication_ObjectionComments, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                UserId, UserIP, null, null, null, null, null, null, null, null,
                null, null, null, null, null, txtObjection.Text, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null);


        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["HasError"].ToString() == "1")
            {
                return false;
            }
            else if (dt.Rows[0]["HasError"].ToString() == "0")
            {
                return true;
            }
        }

        return true;

    }


    private bool Rejection()
    {

        DataTable dt = new BAL_Application().usp_Application(OperationTypesID.UpdateApplication_RejectionComments, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                UserId, UserIP, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, txtObjection.Text);


        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["HasError"].ToString() == "1")
            {
                return false;
            }
            else if (dt.Rows[0]["HasError"].ToString() == "0")
            {
                return true;
            }
        }

        return true;

    }

    private bool Approve()
    {
        if (hdnRequestTypeId.Value == RequestTypeID.Duplication.ToString())
        {
            DataTable dt = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationApprovalDate, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                UserId, UserIP, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, DateTime.Now, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HasError"].ToString() == "1")
                {
                    return false;
                }
                else if (dt.Rows[0]["HasError"].ToString() == "0")
                {
                    return true;
                }
            }
        }

        else if (hdnRequestTypeId.Value == RequestTypeID.Cancellation.ToString())
        {
            DataTable dt = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationApprovalDate_DomicileCancellationDate, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                UserId, UserIP, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, DateTime.Now, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null);


            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HasError"].ToString() == "1")
                {
                    return false;
                }
                else if (dt.Rows[0]["HasError"].ToString() == "0")
                {
                    return true;
                }
            }

        }

        else
        {
            DataTable dt = new BAL_Application().usp_Application(OperationTypesID.UpdateDomicileApprovalDate_ApplicationApprovalDate, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                UserId, UserIP, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, DateTime.Now, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null);


            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HasError"].ToString() == "1")
                {
                    return false;
                }
                else if (dt.Rows[0]["HasError"].ToString() == "0")
                {
                    return true;
                }
            }
        }

        return true;
    }




    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Button button = (Button)sender;
            string buttonId = button.ID;

            if (buttonId == "btnAdd")
            {
                string CNIC_Front = lnkCNICFront.ImageUrl;
                string CNIC_Back = lblCNICBack.ImageUrl;
                string Comm_Report = lblCommReport.ImageUrl;
                string Ref_MUKHTIARKAR = lblRefMUKHTIARKAR.ImageUrl;
                string Ref_PrimaryCertificate = lblRefPrimaryCertificate.ImageUrl;
                string Ref_MatricCertificate = lblRefMatricCertificate.ImageUrl;
                string Ref_RESIDENCECERTIFICATE = lblRefRESIDENCECERTIFICATE.ImageUrl;
                string Ref_Vote = lblVote.ImageUrl;
                string href_GUARDIANDomicile = lblGUARDIANDomicile.ImageUrl;
                string href_BankChalan = lblBankChallan.ImageUrl;
                string href_OtherDoc1 = lblOtherDoc1.ImageUrl;
                string href_OtherDoc2 = lblOtherDoc2.ImageUrl;
                string href_OtherDoc3 = ImgOtherDoc3.ImageUrl;
                string href_OtherDoc4 = ImgOtherDoc4.ImageUrl;
                string href_OtherDoc5 = ImgOtherDoc5.ImageUrl;
                int ApplicationId = Convert.ToInt32(hdnApplicationID.Value);

                DataTable dt = new BAL_Application().usp_UpdateApplicationDocuments(OperationTypesID.UpdateApplicationDocuments, ApplicationId, CNIC_Front, CNIC_Back, Comm_Report, Ref_MUKHTIARKAR, Ref_PrimaryCertificate, Ref_MatricCertificate, Ref_RESIDENCECERTIFICATE, Ref_Vote, href_GUARDIANDomicile, href_BankChalan, href_OtherDoc1, href_OtherDoc2, href_OtherDoc3, href_OtherDoc4, href_OtherDoc5,
                                                         null, null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null, null, null, null,
                                                         UserId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {
                        DataTable dtStatus = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, ApplicationId, null, null, null, null, StatusId.Submitted_to_DDO, null, null, null,
                        null, null, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null, null, null,
                        UserId, UserIP, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, DateTime.Now, null, null, null,
                        null, null, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null, null, null,
                        null, null, null);

                        Session["Success"] = "ARN Number " + ViewState["ARN_Number"] + " documents Uploaded Successfully . Application Submitted to DDO";
                        Response.Redirect("ViewApplicationSaved.aspx");
                    }

                }

            }

            else if (buttonId == "btnApproveDuplicate")
            {
                string CNIC_Front = ImgCNICFront.ImageUrl;
                string CNIC_Back = ImgCNICBack.ImageUrl;
                string FIR_COPY = ImgFir.ImageUrl;
                string APPROVAL_AUTHORITY = ImgApproval.ImageUrl;
                string BANK_CHALLAN = ImgBankChallan.ImageUrl;
                string Application = ImgApp.ImageUrl;
                string OtherDup_Doc1 = ImgDupOther1.ImageUrl;
                string OtherDup_Doc2 = ImgDupOther2.ImageUrl;
                string OtherDup_Doc3 = ImgDupOther3.ImageUrl;


                int ApplicationId = Convert.ToInt32(hdnApplicationID.Value);


                DataTable dt = new BAL_Application().usp_UpdateApplicationDocuments(OperationTypesID.UpdateDuplicateApplicationDocuments, ApplicationId, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                          null, null, null, null, null, null, null, null, null, null,
                                          CNIC_Front, CNIC_Back, FIR_COPY, APPROVAL_AUTHORITY, BANK_CHALLAN, Application, OtherDup_Doc1, OtherDup_Doc2, OtherDup_Doc3,
                                          null, null, null, null, null, null, null, null, null, null, null, null,
                                          UserId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {
                        DataTable dtStatus = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, ApplicationId, null, null, null, null, StatusId.Submitted_to_DDO, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, UserId, UserIP, null
                            , null, null, null, null, null, null, null, null, null, null, null, null, null, DateTime.Now, null, null
                            , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                        Session["Success"] = "ARN Number " + ViewState["ARN_Number"] + " documents Uploaded Successfully . Application Submitted to DDO";
                        Response.Redirect("ViewApplicationSaved.aspx");
                    }

                }
            }


            else if (buttonId == "btnApproveRev")
            {
                string CNIC_Front = Label11.ImageUrl;
                string CNIC_Back = Label13.ImageUrl;
                string APPROVAL_AUTHORITY = Label15.ImageUrl;
                string BANK_CHALLAN = Label17.ImageUrl;
                string CORRECTION_DOCUMENT_1 = Label19.ImageUrl;
                string CORRECTION_DOCUMENT_2 = Label21.ImageUrl;
                string OTHERS = Label23.ImageUrl;
                string OTHERS2 = ImgOtherRev2.ImageUrl;
                string OTHERS3 = ImgOtherRev3.ImageUrl;
                string OTHERS4 = ImgOtherRev4.ImageUrl;

                int ApplicationId = Convert.ToInt32(hdnApplicationID.Value);

                DataTable dt = new BAL_Application().usp_UpdateApplicationDocuments(OperationTypesID.UpdateReviseApplicationDocuments, ApplicationId, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                         CNIC_Front, CNIC_Back, APPROVAL_AUTHORITY, BANK_CHALLAN, CORRECTION_DOCUMENT_1, CORRECTION_DOCUMENT_2, OTHERS, OTHERS2, OTHERS3, OTHERS4,
                                         null, null, null, null, null, null, null, null, null,
                                         null, null, null, null, null, null, null, null, null, null, null, null,
                                         UserId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {
                        DataTable dtStatus = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, ApplicationId, null, null, null, null, StatusId.Submitted_to_DDO, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, UserId, UserIP, null
                            , null, null, null, null, null, null, null, null, null, null, null, null, null, DateTime.Now, null, null
                            , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);


                        Session["Success"] = "ARN Number " + ViewState["ARN_Number"] + " documents Uploaded Successfully . Application Submitted to DDO";
                        Response.Redirect("ViewApplicationSaved.aspx");
                    }

                }
            }

            else if (buttonId == "btnApproveCancel")
            {
                string CNIC_Front = Label37.ImageUrl;
                string CNIC_Back = Label39.ImageUrl;
                string Residence = Label41.ImageUrl;
                string Vote = Label43.ImageUrl;
                string Application = Label45.ImageUrl;
                string Affidevit = Label47.ImageUrl;
                string Approval = Label49.ImageUrl;
                string OtherDoc1 = Label51.ImageUrl;
                string OtherDoc2 = Label53.ImageUrl;
                string OtherDoc3 = ImgCancelOther3.ImageUrl;
                string OtherDoc4 = ImgCancelOther4.ImageUrl;
                string OtherDoc5 = ImgCancelOther5.ImageUrl;


                int ApplicationId = Convert.ToInt32(hdnApplicationID.Value);


                DataTable dt = new BAL_Application().usp_UpdateApplicationDocuments(OperationTypesID.UpdateCancelledApplicationDocuments, ApplicationId, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                      null, null, null, null, null, null, null, null, null, null,
                                      null, null, null, null, null, null, null, null, null,
                                      CNIC_Front, CNIC_Back, Residence, Vote, Application, Affidevit, Approval, OtherDoc1, OtherDoc2, OtherDoc3, OtherDoc4, OtherDoc5,
                                      UserId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {
                        DataTable dtStatus = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, ApplicationId, null, null, null, null, StatusId.Submitted_to_DDO, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, UserId, UserIP, null
                            , null, null, null, null, null, null, null, null, null, null, null, null, null, DateTime.Now, null, null
                            , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                        Session["Success"] = "ARN Number " + ViewState["ARN_Number"] + " documents Uploaded Successfully . Application Submitted to DDO";
                        Response.Redirect("ViewApplicationSaved.aspx");
                    }
                }

            }
        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/ViewAplicationDetails.aspx", "btnAdd_Click", ex.Message);
        }
    }




    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null);

            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptConfirmationReceipt.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                string ApplicationType = "";
                if (dtReport.Rows[0]["ApplicationTypeId"].ToString() == "1")
                    ApplicationType = "Major";

                if (dtReport.Rows[0]["ApplicationTypeId"].ToString() == "2")
                    ApplicationType = "Minor";

                DataTable dtDeliveryDay = new BAL_DeliveryDateDays().usp_Setup_DeliveryDateDays(OperationTypesID.Select, null, null, null, null);
                int TotalDeliveryDay = Convert.ToInt32(dtDeliveryDay.Rows[0]["DeliveryDateDays"].ToString());
                DateTime DeliveryDay = Convert.ToDateTime(dtReport.Rows[0]["CreatedDate"].ToString()).AddDays(TotalDeliveryDay);
                string Day = DeliveryDay.DayOfWeek.ToString();

                if (Day == "Saturday")
                    DeliveryDay = DeliveryDay.AddDays(2);

                if (Day == "Sunday")
                    DeliveryDay = DeliveryDay.AddDays(1);

                dtReport.Columns.Add("DeliveryDate", typeof(DateTime));
                dtReport.Rows[0]["DeliveryDate"] = DeliveryDay;


                ReportParameter parameters1 = new ReportParameter("ApplicationType", ApplicationType);
                viewerDetail.LocalReport.SetParameters(parameters1);

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtReport));
                viewerDetail.LocalReport.Refresh();

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }


                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = UniqueTemporaryFileName_Receipt("Receipt", ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileName + "" + "');", true);

                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                divReceipt.Visible = true;
                divApplciationFormPDF.Visible = true;
                divApplciationFormExcel.Visible = true;
                //   OpenSuccessModal();



            }

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/ViewAplicationDetails.aspx", "btnReceipt_Click", ex.Message);
        }

    }

    protected void btnApplicationForm_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null);
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptApplicationForm.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                string ApplicationRefNo = dtReport.Rows[0]["Application_RefNo"].ToString();
                ReportParameter parameters1 = new ReportParameter("ApplicationNo", ApplicationRefNo);
                viewerDetail.LocalReport.SetParameters(parameters1);


                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 5, null, null, null, null, null); // Application Form Data
                string HeaderArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                ReportParameter parameters2 = new ReportParameter("HeaderArea", HeaderArea);
                viewerDetail.LocalReport.SetParameters(parameters2);

                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);

                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }

                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string DehName = "";
                DataTable dtDeh = new BAL_Deh().usp_Setup_Deh(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["Electoreal_Area_Deh"].ToString()), null, null, null, null);
                if (dtDeh != null && dtDeh.Rows.Count > 0)
                {
                    DehName = dtDeh.Rows[0]["DehName"].ToString();
                }

                DataTable dtEducation = new DataTable();
                dtEducation.Columns.Add("Sr_No", typeof(string));
                dtEducation.Columns.Add("Name_of_Institution", typeof(string));
                dtEducation.Columns.Add("Town_Village", typeof(string));
                dtEducation.Columns.Add("From", typeof(string));
                dtEducation.Columns.Add("To", typeof(string));

                dtEducation.Rows.Add("1", dtReport.Rows[0]["Primary_School"].ToString(), dtReport.Rows[0]["Town_PrimarySchool"].ToString(), dtReport.Rows[0]["FromDate_PrimarySchool"].ToString(), dtReport.Rows[0]["ToDate_PrimarySchool"].ToString());
                dtEducation.Rows.Add("2", dtReport.Rows[0]["Middle_School"].ToString(), dtReport.Rows[0]["Town_MiddleSchool"].ToString(), dtReport.Rows[0]["FromDate_MiddleSchool"].ToString(), dtReport.Rows[0]["ToDate_MiddleSchool"].ToString());
                dtEducation.Rows.Add("3", dtReport.Rows[0]["High_School"].ToString(), dtReport.Rows[0]["Town_HighSchool"].ToString(), dtReport.Rows[0]["FromDate_HighSchool"].ToString(), dtReport.Rows[0]["ToDate_HighSchool"].ToString());


                string tableData = "<!DOCTYPE html><html><body><table><tr><th>Coloumn 1</th> <th>Coloumn 2</th> </tr>";
                tableData += "<tr><td>Coloumn 1 val</td> <td>Coloumn 2 val</td> </tr> </table> </body></html>";

                FinalMiddleArea.Replace("@Name", "<u>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@FName", "<u>" + dtReport.Rows[0]["Father_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@ACnic", "<u>" + dtReport.Rows[0]["Applicant_Cnic"].ToString() + "</u>");
                FinalMiddleArea.Replace("@PlaceIssueDomicile", "<u>" + dtReport.Rows[0]["District"].ToString() + "</u>");
                FinalMiddleArea.Replace("@Single/Married/Widow", "<u>" + MaritialStatus + "</u>");
                FinalMiddleArea.Replace("@Wife_HusbandName", "<u>" + dtReport.Rows[0]["Husband_Wife_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@DoB", "<u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Birth"].ToString()).ToString("dd/M/yyyy") + "</u>");
                FinalMiddleArea.Replace("@ResidentOf", "<u>" + dtReport.Rows[0]["Permanent_Address"].ToString() + "</u>");
                FinalMiddleArea.Replace("@Taluka", "<u>" + TalukaName.ToUpper() + "</u>");
                FinalMiddleArea.Replace("@Cast", "<u>" + dtReport.Rows[0]["Surname"].ToString() + "</u>");
                // FinalMiddleArea.Replace("@EducationDetails", tableData);    


                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder FinalLowerArea = new StringBuilder(LowerArea);

                FinalLowerArea.Replace("@Sr.No", "<u>" + dtReport.Rows[0]["Electoral_Area"].ToString() + "</u>");
                FinalLowerArea.Replace("@Deh", "<u>" + DehName + "</u>");

                string GuardianCNIC = dtReport.Rows[0]["Guardian_NIC"].ToString() == "" ? "________" : dtReport.Rows[0]["Guardian_NIC"].ToString();
                FinalLowerArea.Replace("@FatherCNIC", "<u>" + GuardianCNIC + "</u>");

                string GuardianDomicileDate = dtReport.Rows[0]["GuardianDomicile_CertificateDate"].ToString() == "1/1/0001 12:00:00 AM" ? "__________" : Convert.ToDateTime(dtReport.Rows[0]["GuardianDomicile_CertificateDate"].ToString()).ToString("dd/MM/yyyy");

                FinalLowerArea.Replace("@TempAddress", "<u>" + dtReport.Rows[0]["Temporary_Address"].ToString() + "</u>");
                FinalLowerArea.Replace("@PermanentAddress", "<u>" + dtReport.Rows[0]["Permanent_Address"].ToString() + "</u>");
                FinalLowerArea.Replace("@GuardianDomicileDate", "<u>" + GuardianDomicileDate + "</u>");
                FinalLowerArea.Replace("@ParticularsProperty", "<u>" + dtReport.Rows[0]["ParticularsProperty"].ToString() + "</u>");
                FinalLowerArea.Replace("@ParticularsLocation", "<u>" + dtReport.Rows[0]["Location"].ToString() + "</u>");
                FinalLowerArea.Replace("@Taluka", "<u>" + TalukaName.ToUpper() + "</u>");
                FinalLowerArea.Replace("@District", "<u>" + "KHAIRPUR" + "</u>");

                ReportParameter parameters4 = new ReportParameter("LowerArea", FinalLowerArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters4);

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtEducation));
                viewerDetail.LocalReport.Refresh();


                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "ApplicationForm_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileName + "" + "');", true);


                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                divReceipt.Visible = true;
                divApplciationFormPDF.Visible = true;
                divApplciationFormExcel.Visible = true;


                //Response.ClearHeaders();
                //// Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename=" + UniqueTemporaryFileName_Application("Detail", ".pdf"));

                //Response.BinaryWrite(bytes2); // create the file
                //Response.Flush();

            }

        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnReceipt_Click", ex.Message);
        }
    }

    protected void btnApplicationFormExcel_Click(object sender, EventArgs e)
    {


        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null);

            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptApplicationForm.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                string ApplicationRefNo = dtReport.Rows[0]["Application_RefNo"].ToString();
                ReportParameter parameters1 = new ReportParameter("ApplicationNo", ApplicationRefNo);
                viewerDetail.LocalReport.SetParameters(parameters1);


                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 5, null, null, null, null, null); // Application Form Data
                string HeaderArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                ReportParameter parameters2 = new ReportParameter("HeaderArea", HeaderArea);
                viewerDetail.LocalReport.SetParameters(parameters2);

                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);

                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }

                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string DehName = "";
                DataTable dtDeh = new BAL_Deh().usp_Setup_Deh(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["Electoreal_Area_Deh"].ToString()), null, null, null, null);
                if (dtDeh != null && dtDeh.Rows.Count > 0)
                {
                    DehName = dtDeh.Rows[0]["DehName"].ToString();
                }

                DataTable dtEducation = new DataTable();
                dtEducation.Columns.Add("Sr_No", typeof(string));
                dtEducation.Columns.Add("Name_of_Institution", typeof(string));
                dtEducation.Columns.Add("Town_Village", typeof(string));
                dtEducation.Columns.Add("From", typeof(string));
                dtEducation.Columns.Add("To", typeof(string));

                dtEducation.Rows.Add("1", dtReport.Rows[0]["Primary_School"].ToString(), dtReport.Rows[0]["Town_PrimarySchool"].ToString(), dtReport.Rows[0]["FromDate_PrimarySchool"].ToString(), dtReport.Rows[0]["ToDate_PrimarySchool"].ToString());
                dtEducation.Rows.Add("2", dtReport.Rows[0]["Middle_School"].ToString(), dtReport.Rows[0]["Town_MiddleSchool"].ToString(), dtReport.Rows[0]["FromDate_MiddleSchool"].ToString(), dtReport.Rows[0]["ToDate_MiddleSchool"].ToString());
                dtEducation.Rows.Add("3", dtReport.Rows[0]["High_School"].ToString(), dtReport.Rows[0]["Town_HighSchool"].ToString(), dtReport.Rows[0]["FromDate_HighSchool"].ToString(), dtReport.Rows[0]["ToDate_HighSchool"].ToString());


                string tableData = "<!DOCTYPE html><html><body><table><tr><th>Coloumn 1</th> <th>Coloumn 2</th> </tr>";
                tableData += "<tr><td>Coloumn 1 val</td> <td>Coloumn 2 val</td> </tr> </table> </body></html>";

                FinalMiddleArea.Replace("@Name", dtReport.Rows[0]["Applicant_Name"].ToString());
                FinalMiddleArea.Replace("@FName", dtReport.Rows[0]["Father_Name"].ToString());
                FinalMiddleArea.Replace("@ACnic", dtReport.Rows[0]["Applicant_Cnic"].ToString());
                FinalMiddleArea.Replace("@PlaceIssueDomicile", dtReport.Rows[0]["District"].ToString());
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);
                FinalMiddleArea.Replace("@Wife_HusbandName", dtReport.Rows[0]["Husband_Wife_Name"].ToString());
                FinalMiddleArea.Replace("@DoB", Convert.ToDateTime(dtReport.Rows[0]["Date_of_Birth"].ToString()).ToString("dd/M/yyyy"));
                FinalMiddleArea.Replace("@ResidentOf", dtReport.Rows[0]["Permanent_Address"].ToString());
                FinalMiddleArea.Replace("@Taluka", TalukaName.ToUpper());
                FinalMiddleArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());
                // FinalMiddleArea.Replace("@EducationDetails", tableData);    


                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder FinalLowerArea = new StringBuilder(LowerArea);

                FinalLowerArea.Replace("@Sr.No", dtReport.Rows[0]["Electoral_Area"].ToString());
                FinalLowerArea.Replace("@Deh", DehName);

                string GuardianCNIC = dtReport.Rows[0]["Guardian_NIC"].ToString() == "" ? "________" : dtReport.Rows[0]["Guardian_NIC"].ToString();
                FinalLowerArea.Replace("@FatherCNIC", GuardianCNIC);

                string GuardianDomicileDate = dtReport.Rows[0]["GuardianDomicile_CertificateDate"].ToString() == "1/1/0001 12:00:00 AM" ? "__________" : Convert.ToDateTime(dtReport.Rows[0]["GuardianDomicile_CertificateDate"].ToString()).ToString("dd/MM/yyyy");

                FinalLowerArea.Replace("@TempAddress", dtReport.Rows[0]["Temporary_Address"].ToString());
                FinalLowerArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString());
                FinalLowerArea.Replace("@GuardianDomicileDate", GuardianDomicileDate);
                FinalLowerArea.Replace("@ParticularsProperty", dtReport.Rows[0]["ParticularsProperty"].ToString());
                FinalLowerArea.Replace("@ParticularsLocation", dtReport.Rows[0]["Location"].ToString());
                FinalLowerArea.Replace("@Taluka", TalukaName);
                FinalLowerArea.Replace("@District", "<u>Khairpur</u>");

                ReportParameter parameters4 = new ReportParameter("LowerArea", FinalLowerArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters4);

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtEducation));
                viewerDetail.LocalReport.Refresh();


                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "ApplicationForm_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();


                string fileNameExcel = UniqueTemporaryFileName_Application(FileType, ".XLSX");
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""));
                XlsxLineLayoutOptions options = new XlsxLineLayoutOptions(false, true, true, true);
                pdf.ConvertOptions.SetPdfToXlsxOptions(options);
                pdf.SaveToFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + ""), Spire.Pdf.FileFormat.XLSX);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + "" + "');", true);

                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                divReceipt.Visible = true;
                divApplciationFormPDF.Visible = true;
                divApplciationFormExcel.Visible = true;

            }
        }



        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnReceipt_Click", ex.Message);
        }


    }

    protected void btnPrintDomicile_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null);
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptDomicile.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                string DomicileNo = dtReport.Rows[0]["Domicile_No"].ToString();
                ReportParameter parameters1 = new ReportParameter("DomicileNumber", DomicileNo);
                viewerDetail.LocalReport.SetParameters(parameters1);


                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 8, null, null, null, null, null); // Application Form Data
                //string HeaderArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                //ReportParameter parameters2 = new ReportParameter("HeaderArea", HeaderArea);
                //viewerDetail.LocalReport.SetParameters(parameters2);





                string HeaderArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                StringBuilder FinalHeaderArea = new StringBuilder(HeaderArea);


                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string Date_Arrival = dtReport.Rows[0]["Domicile_ApprovedDate"].ToString() == "" ? "" : Convert.ToDateTime(dtReport.Rows[0]["Domicile_ApprovedDate"].ToString()).ToString("dd/MM/yyyy");

                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                else
                {
                    Error("Deputy Commissioner Not Available");
                    return;
                }

                FinalHeaderArea.Replace("CERTIFICATE OF DOMICILE", "<u>CERTIFICATE OF DOMICILE</u>");
                FinalHeaderArea.Replace("@Name", "<u><b>" + dtReport.Rows[0]["Applicant_Name"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@FName", "<u><b>" + dtReport.Rows[0]["Father_Name"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@Single/Married/Widow", "<u>" + MaritialStatus + "</u>");
                FinalHeaderArea.Replace("@Wife_HusbandName", "<u>" + dtReport.Rows[0]["Husband_Wife_Name"].ToString() + "</u>");

                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }
                FinalHeaderArea.Replace("@PermanentAddress", " <u><b>" + dtReport.Rows[0]["Permanent_Address"].ToString().ToUpper() + "</b></u> Taluka <u><b>" + TalukaName + "</b></u> District <u><b>KHAIRPUR</b></u>");
                FinalHeaderArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalHeaderArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalHeaderArea.Replace("@PlaceofDomicile", "<u>KHAIRPUR</u>");
                FinalHeaderArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                FinalHeaderArea.Replace("@Cast", "<u><b>" + dtReport.Rows[0]["Surname"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@DomicileApprovalDate", "<u>" + Date_Arrival + "</u>");
                ReportParameter parameters2 = new ReportParameter("HeaderArea", FinalHeaderArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters2);



                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);



                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                FinalMiddleArea.Replace("PARTICULARS RELATING TO THE APPLICANT", "<u>PARTICULARS RELATING TO THE APPLICANT</u>");
                FinalMiddleArea.Replace("@Name", dtReport.Rows[0]["Applicant_Name"].ToString());
                FinalMiddleArea.Replace("@FName", dtReport.Rows[0]["Father_Name"].ToString());
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);

                string Husband_Wife = "";
                if (dtReport.Rows[0]["Husband_Wife_Name"].ToString() == "")
                    Husband_Wife = "N/A";
                else
                    Husband_Wife = dtReport.Rows[0]["Husband_Wife_Name"].ToString();

                FinalMiddleArea.Replace("@Wife_HusbandName", Husband_Wife);

                FinalMiddleArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString() + " , TALUKA " + TalukaName.ToUpper() + " DISTRICT KHAIRPUR");

                string AddressForeign = dtReport.Rows[0]["Address_ForeignCountry"].ToString() == "" ? "N/A" : dtReport.Rows[0]["Address_ForeignCountry"].ToString();
                FinalMiddleArea.Replace("@ForeignAddress", AddressForeign);
                FinalMiddleArea.Replace("@DateArrival", Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy"));
                FinalMiddleArea.Replace("@PlaceofDomicile", "KHAIRPUR");
                FinalMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                FinalMiddleArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());



                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder FinalLowerArea = new StringBuilder(LowerArea);

                FinalLowerArea.Replace("@TradeOccupation", dtReport.Rows[0]["Trade_Occupation"].ToString());
                FinalLowerArea.Replace("@MarkIdentification", dtReport.Rows[0]["Mark_of_Identification"].ToString());
                FinalLowerArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalLowerArea.Replace("@DeputyCommissioner", DeputyCommissioner);

                ReportParameter parameters4 = new ReportParameter("LowerArea", FinalLowerArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters4);


                string image = "";
                string Images = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images = Images + "Assets/Images/sindh_govt.JPG";
                image = Images;
                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                ReportParameter parameters5 = new ReportParameter("Image", image);
                viewerDetail.LocalReport.SetParameters(parameters5);


                string ApplicantImage = dtReport.Rows[0]["Applicant_Photo_Path"].ToString();
                string Images1 = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images1 = Images1 + "" + ApplicantImage.Replace("~/", "");
                ApplicantImage = Images1;
                ReportParameter parameters6 = new ReportParameter("ApplicantImage", ApplicantImage);
                viewerDetail.LocalReport.SetParameters(parameters6);



                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                // Revision Case
                if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters7 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters7);
                }
                // Revision Case

                // Duplicate Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters7 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters7);
                }
                // Duplicate Case

                else
                {
                    ReportParameter parameters7 = new ReportParameter("Revision_Image", "No Image");
                    viewerDetail.LocalReport.SetParameters(parameters7);
                }

                ReportParameter parameters8 = new ReportParameter("CommissionerName", DeputyCommissioner);
                viewerDetail.LocalReport.SetParameters(parameters8);

                DataTable dtChild = new BAL_Application().usp_Application(OperationTypesID.GetChildByApplicationId, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null);

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtChild));
                viewerDetail.LocalReport.Refresh();

                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "Domicile_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileName + "" + "');", true);

                // GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                //divDomicile.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnApplicationForm_Click", ex.Message);
        }
    }

    protected void btnPrintDomicileExcel_Click(object sender, EventArgs e)
    {


        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null, null, null, null, null, null, null, null,
                                                                        null, null, null);
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptDomicile.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                string DomicileNo = dtReport.Rows[0]["Domicile_No"].ToString();
                ReportParameter parameters1 = new ReportParameter("DomicileNumber", DomicileNo);
                viewerDetail.LocalReport.SetParameters(parameters1);


                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 8, null, null, null, null, null); // Application Form Data
                //string HeaderArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                //ReportParameter parameters2 = new ReportParameter("HeaderArea", HeaderArea);
                //viewerDetail.LocalReport.SetParameters(parameters2);





                string HeaderArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                StringBuilder FinalHeaderArea = new StringBuilder(HeaderArea);


                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string Date_Arrival = dtReport.Rows[0]["Domicile_ApprovedDate"].ToString() == "" ? "" : Convert.ToDateTime(dtReport.Rows[0]["Domicile_ApprovedDate"].ToString()).ToString("dd/MM/yyyy");

                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                FinalHeaderArea.Replace("CERTIFICATE OF DOMICILE", "<u>CERTIFICATE OF DOMICILE</u>");
                FinalHeaderArea.Replace("@Name", "<b>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</b>");
                FinalHeaderArea.Replace("@FName", "<b>" + dtReport.Rows[0]["Father_Name"].ToString() + "</b>");
                FinalHeaderArea.Replace("@Single/Married/Widow", MaritialStatus);
                FinalHeaderArea.Replace("@Wife_HusbandName", dtReport.Rows[0]["Husband_Wife_Name"].ToString());
                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }
                FinalHeaderArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString() + " Taluka " + TalukaName + "  District KHAIRPUR");
                FinalHeaderArea.Replace("@ForeignAddress", dtReport.Rows[0]["Address_ForeignCountry"].ToString());
                FinalHeaderArea.Replace("@DateArrival", Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy"));
                FinalHeaderArea.Replace("@PlaceofDomicile", "KHAIRPUR");
                FinalHeaderArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                FinalHeaderArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());
                FinalHeaderArea.Replace("@DomicileApprovalDate", Date_Arrival);
                ReportParameter parameters2 = new ReportParameter("HeaderArea", FinalHeaderArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters2);



                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);



                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                else
                {
                    Error("Deputy Commissioner Not Available");
                    return;
                }
                FinalMiddleArea.Replace("@Name", dtReport.Rows[0]["Applicant_Name"].ToString());
                FinalMiddleArea.Replace("@FName", dtReport.Rows[0]["Father_Name"].ToString());
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);
                string Husband_Wife = dtReport.Rows[0]["Husband_Wife_Name"].ToString() == "" ? "N/A" : dtReport.Rows[0]["Husband_Wife_Name"].ToString();
                FinalMiddleArea.Replace("@Wife_HusbandName", Husband_Wife);
                FinalMiddleArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString() + " , TALUKA " + TalukaName.ToUpper() + " DISTRICT KHAIRPUR");

                string AddressForeign = dtReport.Rows[0]["Address_ForeignCountry"].ToString() == "" ? "N/A" : dtReport.Rows[0]["Address_ForeignCountry"].ToString();
                FinalMiddleArea.Replace("@ForeignAddress", AddressForeign);
                FinalMiddleArea.Replace("@DateArrival", Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalMiddleArea.Replace("@PlaceofDomicile", "KHAIRPUR");
                FinalMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                FinalMiddleArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());

                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder FinalLowerArea = new StringBuilder(LowerArea);

                FinalLowerArea.Replace("@TradeOccupation", dtReport.Rows[0]["Trade_Occupation"].ToString());
                FinalLowerArea.Replace("@MarkIdentification", dtReport.Rows[0]["Mark_of_Identification"].ToString());
                FinalLowerArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalLowerArea.Replace("@DeputyCommissioner", DeputyCommissioner);

                ReportParameter parameters4 = new ReportParameter("LowerArea", FinalLowerArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters4);


                string image = "";
                string Images = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images = Images + "Assets/Images/sindh_govt.JPG";
                image = Images;
                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                ReportParameter parameters5 = new ReportParameter("Image", image);
                viewerDetail.LocalReport.SetParameters(parameters5);


                string ApplicantImage = dtReport.Rows[0]["Applicant_Photo_Path"].ToString();
                string Images1 = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images1 = Images1 + "" + ApplicantImage.Replace("~/", "");
                ApplicantImage = Images1;
                ReportParameter parameters6 = new ReportParameter("ApplicantImage", ApplicantImage);
                viewerDetail.LocalReport.SetParameters(parameters6);



                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                // Revision Case
                if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters7 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters7);
                }
                // Revision Case

                // Duplicate Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters7 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters7);
                }
                // Duplicate Case

                else
                {
                    ReportParameter parameters7 = new ReportParameter("Revision_Image", "No Image");
                    viewerDetail.LocalReport.SetParameters(parameters7);
                }

                ReportParameter parameters8 = new ReportParameter("CommissionerName", DeputyCommissioner);
                viewerDetail.LocalReport.SetParameters(parameters8);

                DataTable dtChild = new BAL_Application().usp_Application(OperationTypesID.GetChildByApplicationId, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null);

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtChild));
                viewerDetail.LocalReport.Refresh();


                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "Domicile_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();


                string fileNameExcel = UniqueTemporaryFileName_Application(FileType, ".XLSX");
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""));
                XlsxLineLayoutOptions options = new XlsxLineLayoutOptions(false, true, true, true);
                pdf.ConvertOptions.SetPdfToXlsxOptions(options);
                pdf.SaveToFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + ""), Spire.Pdf.FileFormat.XLSX);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + "" + "');", true);


                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));



            }

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnPrintDomicileExcel_Click", ex.Message);
        }


    }

    protected void btnPrintFormCTest_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null);

            if (dtReport != null && dtReport.Rows.Count > 0)
            {

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFormC.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 7, null, null, null, null, null); // Application Form Data
                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);
                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }

                string Date_Arrival = dtReport.Rows[0]["Domicile_ApprovedDate"].ToString() == "" ? "" : Convert.ToDateTime(dtReport.Rows[0]["Domicile_ApprovedDate"].ToString()).ToString("dd/MM/yyyy");
                FinalMiddleArea.Replace("@Name", "<u>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@FName", "<u>" + dtReport.Rows[0]["Father_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);
                string Husband_Wife = dtReport.Rows[0]["Husband_Wife_Name"].ToString() == "" ? "N/A" : dtReport.Rows[0]["Husband_Wife_Name"].ToString();
                FinalMiddleArea.Replace("@Wife_HusbandName", Husband_Wife);
                FinalMiddleArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString());
                FinalMiddleArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalMiddleArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalMiddleArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalMiddleArea.Replace("@Taluka", "<u>" + TalukaName.ToUpper() + "</u>");
                FinalMiddleArea.Replace("@Place", "KHAIRPUR");


                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);
                string image = "";
                string Images = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images = Images + "Assets/Images/sindh_govt.JPG";
                image = Images;
                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                ReportParameter parameters4 = new ReportParameter("Image", image);
                viewerDetail.LocalReport.SetParameters(parameters4);

                string ApplicantImage = dtReport.Rows[0]["Applicant_Photo_Path"].ToString();
                string Images1 = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images1 = Images1 + "" + ApplicantImage.Replace("~/", "");
                ApplicantImage = Images1;
                ReportParameter parameters5 = new ReportParameter("ApplicantImage", ApplicantImage);
                viewerDetail.LocalReport.SetParameters(parameters5);

                ReportParameter parameters6 = new ReportParameter("DomicileNumber", dtReport.Rows[0]["Domicile_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters6);

                ReportParameter parameters7 = new ReportParameter("FormCNumber", dtReport.Rows[0]["FormC_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters7);


                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                else
                {
                    Error("Deputy Commissioner Not Available");
                    return;
                }

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder LowerMiddleArea = new StringBuilder(LowerArea);
                LowerMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                ReportParameter parameters8 = new ReportParameter("LowerArea", LowerMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters8);

                ReportParameter parameters11 = new ReportParameter("CommissionerName", DeputyCommissioner);
                viewerDetail.LocalReport.SetParameters(parameters11);

                string Header_Area = dt.Rows[0]["Header_Area"].ToString();
                StringBuilder FinalHeaderArea = new StringBuilder(Header_Area);
                FinalHeaderArea.Replace("CERTIFICATE", "<u>CERTIFICATE</u>");
                FinalHeaderArea.Replace("@Name", "<u><b>" + dtReport.Rows[0]["Applicant_Name"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@FName", "<u><b>" + dtReport.Rows[0]["Father_Name"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@Single/Married/Widow", "<u>" + MaritialStatus + "</u>");
                FinalHeaderArea.Replace("@Wife_HusbandName", "<u>" + dtReport.Rows[0]["Husband_Wife_Name"].ToString() + "</u>");
                FinalHeaderArea.Replace("@PermanentAddress", " <u><b>" + dtReport.Rows[0]["Permanent_Address"].ToString().ToUpper() + "</b></u> Taluka <u><b>" + TalukaName.ToUpper() + "</b></u> District <u><b>KHAIRPUR</b></u>");
                FinalHeaderArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalHeaderArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalHeaderArea.Replace("@DomicileApprovalDate", "<u>" + Date_Arrival + "</u>");
                FinalHeaderArea.Replace("@Taluka", " <u><b>" + TalukaName.ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@District", "<u>" + dtReport.Rows[0]["District"].ToString().ToUpper() + "</u>");
                FinalHeaderArea.Replace("@PlaceOfBirth", dtReport.Rows[0]["Place_of_Birth"].ToString());
                FinalHeaderArea.Replace("@Cast", "<u><b>" + dtReport.Rows[0]["Surname"].ToString().ToUpper() + "</b></u>");

                string Educated_At = "";
                DataTable dtIssuedDocsRecord = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, DocumentType.FormC, Convert.ToInt32(hdnApplicationID.Value), null, true, UserId, UserIP);
                if (dtIssuedDocsRecord != null && dtIssuedDocsRecord.Rows.Count > 0)
                {
                    Educated_At = dtIssuedDocsRecord.Rows[0]["Education"].ToString();
                }

                FinalHeaderArea.Replace("@Educatedat", Educated_At);
                FinalHeaderArea.Replace("@SettledPermanentBelow", dtReport.Rows[0]["Permanent_Address"].ToString() + " , TALUKA " + TalukaName.ToUpper() + " DISTRICT KHAIRPUR");
                FinalHeaderArea.Replace("@District", "<u>Khairpur</u>");

                string Header_Dummy = "<!DOCTYPE html><html><head><style></style></head><body><div>" + FinalHeaderArea + "</div></body></html>";
                ReportParameter parameters9 = new ReportParameter("HeaderArea", Header_Dummy);
                viewerDetail.LocalReport.SetParameters(parameters9);


                // Revision Case
                if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Revision Case

                // Duplicate Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Duplicate Case

                else
                {
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", "No Image");
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.Refresh();

                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "FormC_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileName + "" + "');", true);

                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                //divDomicile.Visible = true;

            }

        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnPrintFormC_Click", ex.Message);
        }
    }

    protected void btnPrintFormCExcel_Click(object sender, EventArgs e)
    {


        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      null, null, null, null, null, null, null, null, null, null,
                                                                      null, null, null);

            if (dtReport != null && dtReport.Rows.Count > 0)
            {

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFormC.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 7, null, null, null, null, null); // Application Form Data
                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);
                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }

                string Date_Arrival = dtReport.Rows[0]["Domicile_ApprovedDate"].ToString() == "" ? "__________" : Convert.ToDateTime(dtReport.Rows[0]["Domicile_ApprovedDate"].ToString()).ToString("dd/MM/yyyy");
                FinalMiddleArea.Replace("@Name", dtReport.Rows[0]["Applicant_Name"].ToString());
                FinalMiddleArea.Replace("@FName", dtReport.Rows[0]["Father_Name"].ToString());
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);
                string Husband_Wife = dtReport.Rows[0]["Husband_Wife_Name"].ToString() == "" ? "N/A" : dtReport.Rows[0]["Husband_Wife_Name"].ToString();
                FinalMiddleArea.Replace("@Wife_HusbandName", Husband_Wife);
                FinalMiddleArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString());
                FinalMiddleArea.Replace("@ForeignAddress", dtReport.Rows[0]["Address_ForeignCountry"].ToString());
                FinalMiddleArea.Replace("@DateArrival", Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy"));
                FinalMiddleArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalMiddleArea.Replace("@Taluka", TalukaName.ToUpper());
                FinalMiddleArea.Replace("@Place", "KHAIRPUR");


                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);
                string image = "";
                string Images = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images = Images + "Assets/Images/sindh_govt.JPG";
                image = Images;
                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                ReportParameter parameters4 = new ReportParameter("Image", image);
                viewerDetail.LocalReport.SetParameters(parameters4);

                string ApplicantImage = dtReport.Rows[0]["Applicant_Photo_Path"].ToString();
                string Images1 = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images1 = Images1 + "" + ApplicantImage.Replace("~/", "");
                ApplicantImage = Images1;
                ReportParameter parameters5 = new ReportParameter("ApplicantImage", ApplicantImage);
                viewerDetail.LocalReport.SetParameters(parameters5);

                ReportParameter parameters6 = new ReportParameter("DomicileNumber", dtReport.Rows[0]["Domicile_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters6);

                ReportParameter parameters7 = new ReportParameter("FormCNumber", dtReport.Rows[0]["FormC_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters7);


                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                else
                {
                    Error("Deputy Commissioner Not Available");
                    return;
                }

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder LowerMiddleArea = new StringBuilder(LowerArea);
                LowerMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                ReportParameter parameters8 = new ReportParameter("LowerArea", LowerMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters8);

                ReportParameter parameters11 = new ReportParameter("CommissionerName", DeputyCommissioner);
                viewerDetail.LocalReport.SetParameters(parameters11);

                string Header_Area = dt.Rows[0]["Header_Area"].ToString();
                StringBuilder FinalHeaderArea = new StringBuilder(Header_Area);
                FinalHeaderArea.Replace("CERTIFICATE", "<u>CERTIFICATE</u>");
                FinalHeaderArea.Replace("@Name", dtReport.Rows[0]["Applicant_Name"].ToString());
                FinalHeaderArea.Replace("@FName", "<b><span STYLE=font - size:18.0pt>" + dtReport.Rows[0]["Father_Name"].ToString() + "</span></b>");
                FinalHeaderArea.Replace("@Single/Married/Widow", MaritialStatus);
                FinalHeaderArea.Replace("@Wife_HusbandName", dtReport.Rows[0]["Husband_Wife_Name"].ToString());
                FinalHeaderArea.Replace("@PermanentAddress", "<b>" + dtReport.Rows[0]["Permanent_Address"].ToString() + "</b> Taluka <b>" + TalukaName.ToUpper() + "</b> District <b>KHAIRPUR</b>");
                FinalHeaderArea.Replace("@ForeignAddress", dtReport.Rows[0]["Address_ForeignCountry"].ToString());
                FinalHeaderArea.Replace("@DateArrival", Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy"));
                FinalHeaderArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalHeaderArea.Replace("@Taluka", TalukaName.ToUpper());
                FinalHeaderArea.Replace("@District", dtReport.Rows[0]["District"].ToString());
                FinalHeaderArea.Replace("@PlaceOfBirth", dtReport.Rows[0]["Place_of_Birth"].ToString());
                FinalHeaderArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());

                string Educated_At = "";
                DataTable dtIssuedDocsRecord = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, DocumentType.FormC, Convert.ToInt32(hdnApplicationID.Value), null, true, UserId, UserIP);
                if (dtIssuedDocsRecord != null && dtIssuedDocsRecord.Rows.Count > 0)
                {
                    Educated_At = dtIssuedDocsRecord.Rows[0]["Education"].ToString();
                }

                FinalHeaderArea.Replace("@Educatedat", Educated_At);
                FinalHeaderArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());
                FinalHeaderArea.Replace("@SettledPermanentBelow", dtReport.Rows[0]["Permanent_Address"].ToString() + " Taluka " + TalukaName + " District KHAIRPUR");
                FinalHeaderArea.Replace("@District", "Khairpur");

                string Header_Dummy = "<!DOCTYPE html><html><head><style></style></head><body><div>" + FinalHeaderArea + "</div></body></html>";
                ReportParameter parameters9 = new ReportParameter("HeaderArea", Header_Dummy);
                viewerDetail.LocalReport.SetParameters(parameters9);


                // Revision Case
                if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Revision Case

                // Duplicate Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Duplicate Case

                else
                {
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", "No Image");
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.Refresh();

                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "FormC_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();


                string fileNameExcel = UniqueTemporaryFileName_Application(FileType, ".XLSX");
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""));
                XlsxLineLayoutOptions options = new XlsxLineLayoutOptions(false, true, true, true);
                pdf.ConvertOptions.SetPdfToXlsxOptions(options);
                pdf.SaveToFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + ""), Spire.Pdf.FileFormat.XLSX);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + "" + "');", true);


                //GetARNNumber(Convert.ToInt32(hdnApplicationID.Value));
                //divDomicile.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnPrintDomicileExcel_Click", ex.Message);
        }

    }

    protected void btnPrintFormD_Click(object sender, EventArgs e)
    {

        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null);
            if (dtReport != null && dtReport.Rows.Count > 0)
            {

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFormD.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 6, null, null, null, null, null); // Application Form Data
                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);
                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }

                string Date_Arrival = dtReport.Rows[0]["Domicile_ApprovedDate"].ToString() == "" ? "__________" : Convert.ToDateTime(dtReport.Rows[0]["Domicile_ApprovedDate"].ToString()).ToString("dd/MM/yyyy");
                FinalMiddleArea.Replace("@Name", "<u>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@FName", "<u>" + dtReport.Rows[0]["Father_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);

                string Husband_Wife = dtReport.Rows[0]["Husband_Wife_Name"].ToString() == "" ? "N/A" : dtReport.Rows[0]["Husband_Wife_Name"].ToString();
                FinalMiddleArea.Replace("@Wife_HusbandName", Husband_Wife);
                FinalMiddleArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString());
                FinalMiddleArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalMiddleArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalMiddleArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalMiddleArea.Replace("@Taluka", "<u>" + TalukaName.ToUpper() + "</u>");
                FinalMiddleArea.Replace("@Place", "KHAIRPUR");
                string Educated_At = "";
                DataTable dtIssuedDocsRecord = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, DocumentType.FormD, Convert.ToInt32(hdnApplicationID.Value), null, true, UserId, UserIP);
                if (dtIssuedDocsRecord != null && dtIssuedDocsRecord.Rows.Count > 0)
                {
                    Educated_At = dtIssuedDocsRecord.Rows[0]["Education"].ToString();
                }

                FinalMiddleArea.Replace("@Educatedat", Educated_At);
                FinalMiddleArea.Replace("@Place", "<u>KHAIRPUR</u>");
                FinalMiddleArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());

                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);
                string image = "";
                string Images = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images = Images + "Assets/Images/sindh_govt.JPG";
                image = Images;
                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                ReportParameter parameters4 = new ReportParameter("Image", image);
                viewerDetail.LocalReport.SetParameters(parameters4);

                string ApplicantImage = dtReport.Rows[0]["Applicant_Photo_Path"].ToString();
                string Images1 = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images1 = Images1 + "" + ApplicantImage.Replace("~/", "");
                ApplicantImage = Images1;
                ReportParameter parameters5 = new ReportParameter("ApplicantImage", ApplicantImage);
                viewerDetail.LocalReport.SetParameters(parameters5);

                ReportParameter parameters6 = new ReportParameter("DomicileNumber", dtReport.Rows[0]["Domicile_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters6);

                ReportParameter parameters7 = new ReportParameter("FormDNumber", dtReport.Rows[0]["FormD_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters7);


                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder LowerMiddleArea = new StringBuilder(LowerArea);
                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                else
                {
                    Error("Deputy Commissioner Not Available");
                    return;
                }

                LowerMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                ReportParameter parameters8 = new ReportParameter("LowerArea", LowerMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters8);


                string Header_Area = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                StringBuilder FinalHeaderArea = new StringBuilder(Header_Area);
                FinalHeaderArea.Replace("CERTIFICATE", "<u>CERTIFICATE</u>");
                FinalHeaderArea.Replace("@Name", "<u><b>" + dtReport.Rows[0]["Applicant_Name"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@FName", "<u><b>" + dtReport.Rows[0]["Father_Name"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@Single/Married/Widow", "<u>" + MaritialStatus + "</u>");
                FinalHeaderArea.Replace("@Wife_HusbandName", "<u>" + dtReport.Rows[0]["Husband_Wife_Name"].ToString() + "</u>");
                FinalHeaderArea.Replace("@PermanentAddress", " <u><b>" + dtReport.Rows[0]["Permanent_Address"].ToString().ToUpper() + "</b></u> Taluka <u><b>" + TalukaName.ToUpper() + "</b></u> District <u><b>KHAIRPUR</b></u>");
                FinalHeaderArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalHeaderArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalHeaderArea.Replace("@DomicileApprovalDate", "<u>" + Date_Arrival + "</u>");
                FinalHeaderArea.Replace("@Taluka", "<u><b>" + TalukaName.ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@District", "<u>" + dtReport.Rows[0]["District"].ToString() + "</u>");
                FinalHeaderArea.Replace("@PlaceOfBirth", dtReport.Rows[0]["Place_of_Birth"].ToString());
                FinalHeaderArea.Replace("@Cast", "<u><b>" + dtReport.Rows[0]["Surname"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@Educatedat", Educated_At);
                FinalHeaderArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());
                FinalHeaderArea.Replace("@SettledPermanentBelow", dtReport.Rows[0]["Permanent_Address"].ToString() + " , TALUKA " + TalukaName.ToUpper() + " DISTRICT KHAIRPUR");
                FinalHeaderArea.Replace("@District", "<u>Khairpur</u>");


                ReportParameter parameters9 = new ReportParameter("HeaderArea", FinalHeaderArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters9);


                if (dtReport.Rows[0]["ApplicationId_Revised_Duplicate"].ToString() != "0" && (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString() || dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                   && dtReport.Rows[0]["ApplicationTypeId"].ToString() == ApplicationTypeValueId.Minor.ToString())
                {
                    int ApplicationId_Revised_Duplicate = Convert.ToInt32(dtReport.Rows[0]["ApplicationId_Revised_Duplicate"].ToString());
                    DataTable dtIssuedDocs = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, DocumentType.FormD, Convert.ToInt32(ApplicationId_Revised_Duplicate), null, true, UserId, UserIP);
                    if (dtIssuedDocs != null && dtIssuedDocs.Rows.Count == 0)
                    {
                        ReportParameter parameters10 = new ReportParameter("Revision_Image", "No Image");
                        viewerDetail.LocalReport.SetParameters(parameters10);
                    }
                    // Revision Case
                    else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                    {
                        string Revision_Image = "";
                        string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                        ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                        Revision_Image = ImagesRevision;
                        ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                        viewerDetail.LocalReport.SetParameters(parameters10);
                    }
                    // Revision Case

                    // Duplicate Case
                    else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                    {
                        string Revision_Image = "";
                        string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                        ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                        Revision_Image = ImagesRevision;
                        ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                        viewerDetail.LocalReport.SetParameters(parameters10);
                    }
                    // Duplicate Case
                }


                // Revision Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Revision Case

                // Duplicate Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Duplicate Case


                else
                {
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", "No Image");
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }



                ReportParameter parameters11 = new ReportParameter("CommissionerName", DeputyCommissioner);
                viewerDetail.LocalReport.SetParameters(parameters11);

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.Refresh();

                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "FormD_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileName + "" + "');", true);

                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                //divDomicile.Visible = true;

            }

        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnPrintFormC_Click", ex.Message);
        }

    }


    protected void btnPrintFormDExcel_Click(object sender, EventArgs e)
    {


        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                UserId, UserIP, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                null, null, null);
            if (dtReport != null && dtReport.Rows.Count > 0)
            {

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer viewerDetail = new ReportViewer();

                viewerDetail.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFormD.rdlc");
                viewerDetail.LocalReport.EnableExternalImages = true;

                DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, 6, null, null, null, null, null); // Application Form Data
                string MiddleArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Middle_Area"].ToString() + "</body></html>";
                StringBuilder FinalMiddleArea = new StringBuilder(MiddleArea);
                string MaritialStatus = "";

                if (dtReport.Rows[0]["Marital_Status"].ToString() == "1")
                    MaritialStatus = "Single";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "2")
                    MaritialStatus = "Married";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "3")
                    MaritialStatus = "Widow";

                else if (dtReport.Rows[0]["Marital_Status"].ToString() == "4")
                    MaritialStatus = "Divorced";

                string TalukaName = "";
                DataTable dtTaluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.OtIsExistById, Convert.ToInt32(dtReport.Rows[0]["TalukaId"].ToString()), null, null, null);
                if (dtTaluka != null && dtTaluka.Rows.Count > 0)
                {
                    TalukaName = dtTaluka.Rows[0]["TalukaName"].ToString();
                }

                string Date_Arrival = dtReport.Rows[0]["Domicile_ApprovedDate"].ToString() == "" ? "__________" : Convert.ToDateTime(dtReport.Rows[0]["Domicile_ApprovedDate"].ToString()).ToString("dd/MM/yyyy");
                FinalMiddleArea.Replace("@Name", "<u>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@FName", "<u>" + dtReport.Rows[0]["Father_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);
                string Husband_Wife = dtReport.Rows[0]["Husband_Wife_Name"].ToString() == "" ? "N/A" : dtReport.Rows[0]["Husband_Wife_Name"].ToString();
                FinalMiddleArea.Replace("@Wife_HusbandName", Husband_Wife);
                FinalMiddleArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString());
                FinalMiddleArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalMiddleArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalMiddleArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalMiddleArea.Replace("@Taluka", "<u>" + TalukaName.ToUpper() + "</u>");
                FinalMiddleArea.Replace("@Place", "KHAIRPUR");
                string Educated_At = "";
                DataTable dtIssuedDocsRecord = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, DocumentType.FormD, Convert.ToInt32(hdnApplicationID.Value), null, true, UserId, UserIP);
                if (dtIssuedDocsRecord != null && dtIssuedDocsRecord.Rows.Count > 0)
                {
                    Educated_At = dtIssuedDocsRecord.Rows[0]["Education"].ToString();
                }

                FinalMiddleArea.Replace("@Educatedat", Educated_At);
                FinalMiddleArea.Replace("@Place", "<u>KHAIRPUR</u>");
                FinalMiddleArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());

                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);
                string image = "";
                string Images = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images = Images + "Assets/Images/sindh_govt.JPG";
                image = Images;
                //string Image4 = Server.MapPath("../../Assets/Images/sindh_govt.jpg");

                ReportParameter parameters4 = new ReportParameter("Image", image);
                viewerDetail.LocalReport.SetParameters(parameters4);

                string ApplicantImage = dtReport.Rows[0]["Applicant_Photo_Path"].ToString();
                string Images1 = new Uri(Server.MapPath("~")).AbsoluteUri;
                Images1 = Images1 + "" + ApplicantImage.Replace("~/", "");
                ApplicantImage = Images1;
                ReportParameter parameters5 = new ReportParameter("ApplicantImage", ApplicantImage);
                viewerDetail.LocalReport.SetParameters(parameters5);

                ReportParameter parameters6 = new ReportParameter("DomicileNumber", dtReport.Rows[0]["Domicile_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters6);

                ReportParameter parameters7 = new ReportParameter("FormDNumber", dtReport.Rows[0]["FormD_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters7);


                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder LowerMiddleArea = new StringBuilder(LowerArea);
                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }
                else
                {
                    Error("Deputy Commissioner Not Available");
                    return;
                }

                LowerMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                ReportParameter parameters8 = new ReportParameter("LowerArea", LowerMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters8);


                string Header_Area = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                StringBuilder FinalHeaderArea = new StringBuilder(Header_Area);
                FinalHeaderArea.Replace("CERTIFICATE", "<u>CERTIFICATE</u>");
                FinalHeaderArea.Replace("@Name", "<b>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</b>");
                FinalHeaderArea.Replace("@FName", "<b>" + dtReport.Rows[0]["Father_Name"].ToString() + "</b>");
                FinalHeaderArea.Replace("@Single/Married/Widow", MaritialStatus);
                FinalHeaderArea.Replace("@Wife_HusbandName", dtReport.Rows[0]["Husband_Wife_Name"].ToString());
                FinalHeaderArea.Replace("@PermanentAddress", " <b>" + dtReport.Rows[0]["Permanent_Address"].ToString().ToUpper() + "</b> Taluka <b> " + TalukaName.ToUpper() + "</b> District <b>KHAIRPUR</b>");
                FinalHeaderArea.Replace("@ForeignAddress", dtReport.Rows[0]["Address_ForeignCountry"].ToString());
                FinalHeaderArea.Replace("@DateArrival", Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy"));
                FinalHeaderArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalHeaderArea.Replace("@Taluka", "<b>" + TalukaName + "</b>");
                FinalHeaderArea.Replace("@District", dtReport.Rows[0]["District"].ToString());
                FinalHeaderArea.Replace("@PlaceOfBirth", dtReport.Rows[0]["Place_of_Birth"].ToString());
                FinalHeaderArea.Replace("@Cast", "<b>" + dtReport.Rows[0]["Surname"].ToString() + "</b>");
                FinalHeaderArea.Replace("@Educatedat", Educated_At);
                FinalHeaderArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());
                FinalHeaderArea.Replace("@SettledPermanentBelow", dtReport.Rows[0]["Permanent_Address"].ToString() + " , Taluka " + TalukaName.ToUpper() + " District KHAIRPUR");
                FinalHeaderArea.Replace("@District", "Khairpur");


                ReportParameter parameters9 = new ReportParameter("HeaderArea", FinalHeaderArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters9);

                if (dtReport.Rows[0]["ApplicationId_Revised_Duplicate"].ToString() != "0" && (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString() || dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
          && dtReport.Rows[0]["ApplicationTypeId"].ToString() == ApplicationTypeValueId.Minor.ToString())
                {
                    int ApplicationId_Revised_Duplicate = Convert.ToInt32(dtReport.Rows[0]["ApplicationId_Revised_Duplicate"].ToString());
                    DataTable dtIssuedDocs = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, DocumentType.FormD, Convert.ToInt32(ApplicationId_Revised_Duplicate), null, true, UserId, UserIP);
                    if (dtIssuedDocs != null && dtIssuedDocs.Rows.Count == 0)
                    {
                        ReportParameter parameters10 = new ReportParameter("Revision_Image", "No Image");
                        viewerDetail.LocalReport.SetParameters(parameters10);
                    }
                    // Revision Case
                    else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                    {
                        string Revision_Image = "";
                        string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                        ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                        Revision_Image = ImagesRevision;
                        ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                        viewerDetail.LocalReport.SetParameters(parameters10);
                    }
                    // Revision Case

                    // Duplicate Case
                    else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                    {
                        string Revision_Image = "";
                        string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                        ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                        Revision_Image = ImagesRevision;
                        ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                        viewerDetail.LocalReport.SetParameters(parameters10);
                    }
                    // Duplicate Case
                }



                // Revision Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Revision.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Revision Case

                // Duplicate Case
                else if (dtReport.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                {
                    string Revision_Image = "";
                    string ImagesRevision = new Uri(Server.MapPath("~")).AbsoluteUri;
                    ImagesRevision = ImagesRevision + "Assets/Images/Duplicate.png";
                    Revision_Image = ImagesRevision;
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", Revision_Image);
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }
                // Duplicate Case

                else
                {
                    ReportParameter parameters10 = new ReportParameter("Revision_Image", "No Image");
                    viewerDetail.LocalReport.SetParameters(parameters10);
                }



                ReportParameter parameters11 = new ReportParameter("CommissionerName", DeputyCommissioner);
                viewerDetail.LocalReport.SetParameters(parameters11);

                viewerDetail.LocalReport.DataSources.Clear();
                viewerDetail.LocalReport.Refresh();

                string path = Server.MapPath("~/Pages/Domicile/DocumentFiles/TempPDF/" + UserId);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                string folderPath = Server.MapPath("DocumentFiles/TempPDF/" + UserId);

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "FormD_" + hdnApplicationID.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();


                string fileNameExcel = UniqueTemporaryFileName_Application(FileType, ".XLSX");
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""));
                XlsxLineLayoutOptions options = new XlsxLineLayoutOptions(false, true, true, true);
                pdf.ConvertOptions.SetPdfToXlsxOptions(options);
                pdf.SaveToFile(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + ""), Spire.Pdf.FileFormat.XLSX);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileNameExcel + "" + "');", true);


                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                //divDomicile.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("ViewAplicationDetails.aspx", "btnPrintDomicileExcel_Click", ex.Message);
        }


    }


    public void DuplicateDocumentsRecord(int DocumentTypeID)
    {
        DataTable dt = new BAL_DuplicateDocumentsRecord().usp_Setup_DuplicateDocumentsRecord(OperationTypesID.Insert, DocumentTypeID, Convert.ToInt32(hdnApplicationID.Value), true, UserId, UserIP);
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["HasError"].ToString() == "1")
            {
                Error(dt.Rows[0]["Message"].ToString());
            }
            else if (dt.Rows[0]["HasError"].ToString() == "0")
            {

            }
        }

    }

    public void IssuedDocumentsRecord(int DocumentTypeID)
    {
        DataTable dt = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Insert, DocumentTypeID, Convert.ToInt32(hdnApplicationID.Value), txtEducation.Text, true, UserId, UserIP);
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["HasError"].ToString() == "1")
            {
                Error(dt.Rows[0]["Message"].ToString());
            }
            else if (dt.Rows[0]["HasError"].ToString() == "0")
            {

            }
        }

    }


    public void GetIssuedDocumentsRecord()
    {
        DataTable dt = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, null, Convert.ToInt32(hdnApplicationID.Value), null, true, UserId, UserIP);
        if (dt != null && dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["DocumentTypeId"].ToString() == DocumentType.Domicile.ToString())
                {
                    ViewState["DomicileIssued"] = "true";
                }
                else if (dt.Rows[i]["DocumentTypeId"].ToString() == DocumentType.FormC.ToString())
                {
                    ViewState["FormCIssued"] = "true";
                }
                else if (dt.Rows[i]["DocumentTypeId"].ToString() == DocumentType.FormD.ToString())
                {
                    ViewState["FormDIssued"] = "true";
                }
            }

        }
    }



    private string UniqueTemporaryFileName_Receipt(string Filetype, string extension)
    {
        Guid id = Guid.NewGuid();
        string rnd = id.ToString().ToUpper().Replace('_', '2').Replace('-', '1').Substring(0, 20);


        return "Receipt_"
             + Filetype
             + rnd
             + extension;
    }

    private string UniqueTemporaryFileName_Application(string Filetype, string extension)
    {
        Guid id = Guid.NewGuid();
        string rnd = id.ToString().ToUpper().Replace('_', '2').Replace('-', '1').Substring(0, 20);

        return Filetype
       + rnd
       + extension;
    }
    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == StatusId.Objected.ToString() || ddlStatus.SelectedValue == StatusId.Rejected.ToString())
        {
            divObjection_Rejection.Visible = true;
        }
        else
            divObjection_Rejection.Visible = false;
    }
    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);

    }

    private void DisableControls()
    {
        if (hdnStatusId.Value != StatusId.InitialDraft.ToString() && hdnStatusId.Value != StatusId.Objected.ToString())
        {
            FileUploadCNICFront.Enabled = false;
            FileUploadCNICBack.Enabled = false;
            FileUpload_AsstComm.Enabled = false;
            FileUpload4.Enabled = false;
            FileUpload5.Enabled = false;
            FileUpload6.Enabled = false;
            FileUpload7.Enabled = false;
            FileUpload8.Enabled = false;
            FileUpload9.Enabled = false;
            FileUpload10.Enabled = false;
            FileUpload11.Enabled = false;
            FileUpload12.Enabled = false;
            FileUploadCNICFront_Dup.Enabled = false;
            FileUploadCNICBack_Dup.Enabled = false;
            FileUploadFIR_Dup.Enabled = false;
            FileUploadAuthority_Dup.Enabled = false;
            FileUploadChallan_Dup.Enabled = false;
            FileUploadApp_Dup.Enabled = false;
            FileUpload13.Enabled = false;
            FileUpload14.Enabled = false;
            FileUpload15.Enabled = false;
            FileUpload16.Enabled = false;
            FileUpload17.Enabled = false;
            FileUpload18.Enabled = false;
            FileUpload19.Enabled = false;
            FileCNICFront_Cancel.Enabled = false;
            FileCNICBack_Cancel.Enabled = false;
            FileResidence_Cancel.Enabled = false;
            FileVote_Cancel.Enabled = false;
            FileApplication_Cancel.Enabled = false;
            FileAffidevit_Cancel.Enabled = false;
            FileApproval_Cancel.Enabled = false;
            FileOtherDoc1_Cancel.Enabled = false;
            FileOtherDoc2_Cancel.Enabled = false;
            FileOtherDoc3.Enabled = false;
            FileOtherDoc4.Enabled = false;
            FileOtherDoc5.Enabled = false;
            FileDupOther1.Enabled = false;
            FileDupOther2.Enabled = false;
            FileDupOther3.Enabled = false;
            FileOtherRev2.Enabled = false;
            FileOtherRev3.Enabled = false;
            FileOtherRev4.Enabled = false;
            FileCancelOther3.Enabled = false;
            FileCancelOther4.Enabled = false;
            FileCancelOther5.Enabled = false;

        }

    }

    private void Show_Hide_Controls()
    {
        if (hdnRequestTypeId.Value == RequestTypeID.New_Request.ToString() && (hdnStatusId.Value == StatusId.InitialDraft.ToString() || hdnStatusId.Value == StatusId.Objected.ToString() || hdnStatusId.Value == StatusId.Submitted_to_DDO.ToString()))
        {
            divNewDocs.Visible = true;

        }

        if (hdnRequestTypeId.Value == RequestTypeID.Duplication.ToString() && (hdnStatusId.Value == StatusId.InitialDraft.ToString() || hdnStatusId.Value == StatusId.Objected.ToString() || hdnStatusId.Value == StatusId.Submitted_to_DDO.ToString()))
        {
            divDuplicateDocs.Visible = true;
        }

        if (hdnRequestTypeId.Value == RequestTypeID.Revision.ToString() && (hdnStatusId.Value == StatusId.InitialDraft.ToString() || hdnStatusId.Value == StatusId.Objected.ToString() || hdnStatusId.Value == StatusId.Submitted_to_DDO.ToString()))
        {
            divRevisionDocs.Visible = true;
        }

        if (hdnRequestTypeId.Value == RequestTypeID.Cancellation.ToString() && (hdnStatusId.Value == StatusId.InitialDraft.ToString() || hdnStatusId.Value == StatusId.Objected.ToString() || hdnStatusId.Value == StatusId.Submitted_to_DDO.ToString()))
        {
            divCancelDocs.Visible = true;
        }

        if (hdnRequestTypeId.Value == RequestTypeID.Cancellation.ToString())
            divCancelDocs.Visible = true;

        if (hdnRequestTypeId.Value == RequestTypeID.Revision.ToString())
            divRevisionDocs.Visible = true;

        if (hdnRequestTypeId.Value == RequestTypeID.Duplication.ToString())
            divDuplicateDocs.Visible = true;

        if (hdnRequestTypeId.Value == RequestTypeID.New_Request.ToString())
            divNewDocs.Visible = true;



        if ((hdnStatusId.Value == StatusId.Submitted_to_DDO.ToString()) && ((RoleId == UserRole.Approver || RoleId == UserRole.SuperAdmin)))
        {
            divApproverSubmit.Visible = true;
        }

        if ((hdnStatusId.Value == StatusId.Approved.ToString() || hdnStatusId.Value == StatusId.Issued.ToString()) 
            && (RoleId == UserRole.SuperAdmin))
        {
            divApproverSubmit.Visible = true;
            divSuperAdmin_AdditionalDocs.Visible = true;
            btnSuperAdminDocs.Visible = true;
        }

        if (hdnStatusId.Value == StatusId.InitialDraft.ToString() || hdnStatusId.Value == StatusId.Objected.ToString())
        {
            if (IsAdd.Value == "1")
            {
                btnAdd.Visible = true;
                btnApproveDuplicate.Visible = true;
                btnApproveRev.Visible = true;
                btnApproveCancel.Visible = true;
               
            }
        }

        if (hdnStatusId.Value != StatusId.Approved.ToString() && hdnStatusId.Value != StatusId.Disable.ToString()
            && hdnStatusId.Value != StatusId.Issued.ToString() && lblObjectionComments.Text != "N/A")
        {
            divObjectionComments.Visible = true;
        }

        if (hdnStatusId.Value != StatusId.Approved.ToString() && hdnStatusId.Value != StatusId.Disable.ToString()
       && hdnStatusId.Value != StatusId.Issued.ToString() && lblRejectionComments.Text != "N/A")
        {
            divRejectionComments.Visible = true;
        }

        int ApplicantAge = Convert.ToInt32(lblAge.Text);
        if (ApplicantAge < 18)
        {
            btnIssueFormD.Visible = false;
        }

        if (IsView.Value == "1")
        {
            divReceiptPrint.Visible = true;
        }



        GetIssuedDocumentsRecord();

        if (hdnStatusId.Value == StatusId.Approved.ToString() || hdnStatusId.Value == StatusId.Issued.ToString())
        {

            if ((RoleId == UserRole.Approver || RoleId == UserRole.SuperAdmin))
            {
                if (IsView.Value == "1")
                {
                    divIssueDomicile.Visible = true;
                    divEducation.Visible = true;
                    // divDomicilePrint.Visible = true;
                }
            }


            if (ViewState["DomicileIssued"] != null && ViewState["DomicileIssued"].ToString() != "")
            {
                btnIssueDomicile.Visible = false;
                btnDomicile.Visible = true;
                btnDomicileExcel.Visible = true;
                divDomicilePrint.Visible = true;
            }
            else
            {
                divIssueDomicile.Visible = true;
                btnIssueDomicile.Visible = true;
            }

            if (ViewState["FormCIssued"] != null && ViewState["FormCIssued"].ToString() != "")
            {
                btnIssueFormC.Visible = false;
                btnFormC.Visible = true;
                btnFormCExcel.Visible = true;
                divDomicilePrint.Visible = true;
            }

            else
            {
                divIssueDomicile.Visible = true;
                btnIssueFormC.Visible = true;
            }

            if (ViewState["FormDIssued"] != null && ViewState["FormDIssued"].ToString() != "")
            {
                btnIssueFormD.Visible = false;
                btnFormD.Visible = true;
                btnFormDExcel.Visible = true;
                divDomicilePrint.Visible = true;
            }
            else
            {
                divIssueDomicile.Visible = true;
                btnIssueFormD.Visible = true;
            }

        }

        if ((ViewState["FormCIssued"] != null && ViewState["FormCIssued"].ToString() != "") &&
            (ViewState["FormDIssued"] != null && ViewState["FormDIssued"].ToString() != ""))
        {
            divEducation.Visible = false;
        }

        if ((ViewState["FormCIssued"] != null && ViewState["FormCIssued"].ToString() != "") &&
           (ViewState["IsMinor"] != null && ViewState["IsMinor"].ToString() == "true") && ApplicantAge < 18)
        {
            divEducation.Visible = false;
            btnIssueFormD.Visible = false;
        }
    }

    public void GetApplicationDomicileHistory()
    {
        DataTable dt = new BAL_Application().usp_Application(OperationTypesID.GetDomicileHistory, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            UserId, UserIP, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, null,
            null, null, null);
        if (dt != null && dt.Rows.Count > 1)
        {
            divDomicileHistory.Visible = true;
            rptDomicileHistory.DataSource = dt;
            rptDomicileHistory.DataBind();
        }
        else
        {
            divDomicileHistory.Visible = false;
            rptDomicileHistory.DataSource = null;
            rptDomicileHistory.DataBind();
        }

    }

    public void GetApplicationReceiverDetail()
    {

        DataTable dt = new BAL_IssuanceRegister().usp_Setup_IssuanceRegister(OperationTypesID.Select, Convert.ToInt32(hdnApplicationID.Value), null, null, null, null, UserId, UserIP);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                DivReceiver.Visible = true;
                rptReceiver.DataSource = dt;
                rptReceiver.DataBind();
            }
        }

    }

    private void SetFeature()
    {
        try
        {

            string url = HttpContext.Current.Request.Url.PathAndQuery;
            string[] Array = url.Split('?');
            url = Array[0];
            DataTable dt = new BAL_Setup_MenuItem().usp_CheckMenuAccess(RoleId, url);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["FeatureId"].ToString()) == (int)Feature.Add)
                    {
                        IsAdd.Value = "1";

                    }
                    else if (Convert.ToInt32(dt.Rows[i]["FeatureId"].ToString()) == (int)Feature.Update)
                    {
                        IsEdit.Value = "1";
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["FeatureId"].ToString()) == (int)Feature.Delete)
                    {
                        IsDelete.Value = "1";
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["FeatureId"].ToString()) == (int)Feature.View)
                    {
                        IsView.Value = "1";
                    }
                }
            }
        }
        catch (Exception ex) { }
    }
}