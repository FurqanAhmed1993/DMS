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

public partial class DomicileApplication_Major : Base
{
    string ApplicationType = "";
    string RequestType = "";
    int? Nullint = null;
    int StepCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.UrlReferrer != null)
            {
                ViewState["Previous_URL"] = Request.UrlReferrer.AbsoluteUri;
            }

            Session["RowNo"] = null;
            BindDropDown();
            DateTime CurentDate = DateTime.Now;
            txtDateSubmission.Text = CurentDate.ToString("yyyy-MM-dd");
            txtIssueDate.Text = CurentDate.ToString("yyyy-MM-dd");
            SetFeature();


            if (Request.QueryString["ApplicationTypeId"] != null && Request.QueryString["ApplicationTypeId"].ToString() != "")
            {
                hdnApplicationType.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["ApplicationTypeId"].ToString()));
                if (hdnApplicationType.Value == ApplicationTypeValueId.Major.ToString())
                {
                    RequiredFieldValidator5.Enabled = true;
                    RegularExpressionValidatorStreet.Enabled = true;
                    spnCNIC.Visible = true;
                    lblAppType.Text = "Application for Certificate of Domcile Pakistan(Age 18 Years of above)";
                }
                else if (hdnApplicationType.Value == ApplicationTypeValueId.Minor.ToString())
                {
                    RequiredFieldValidator5.Enabled = false;
                    RegularExpressionValidatorStreet.Enabled = false;
                    spnCNIC.Visible = false;
                    lblAppType.Text = "Application for Certificate of Domcile Pakistan(Below 18 Years)";
                }

            }

            if (Request.QueryString["RequestTypeId"] != null && Request.QueryString["RequestTypeId"].ToString() != "")
            {

                hdnRequestType.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["RequestTypeId"].ToString()));
            }


            if (Request.QueryString["ApplicationStatusId"] != null && Request.QueryString["ApplicationStatusId"].ToString() != "")
            {

                hdnApplicationStatusId.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["ApplicationStatusId"].ToString()));
            }

            if (Request.QueryString["ApplicationId"] != null)
            {
                if (Request.QueryString["ApplicationId"].ToString() != "")
                {

                    hdnApplicationId.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["ApplicationId"].ToString()));
                    // GetDuplicateDocInfo();
                    if (Request.QueryString["IsView"] != null && Request.QueryString["IsView"].ToString() == "True")
                    {
                        ViewState["IsView"] = "true";

                        hdnApplicationId.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["ApplicationId"].ToString()));
                        GetApplicationDetails(Convert.ToInt32(hdnApplicationId.Value));
                        divDelivery.Visible = true;
                        DisablePageControls(false);
                        FileUpload1.Visible = false;
                        btnSubmit.Visible = false;
                        CalculateDeliveryDate();
                        btnBack.Enabled = true;


                        ViewState["IsView"] = "true";




                        #region Revision and Duplicate and Cancellation Case Start 

                        if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && (hdnRequestType.Value == RequestTypeID.Revision.ToString()))
                        {
                            if (IsAdd.Value == "1")
                            {
                                btnSubmit.Visible = true;
                                btnSubmit.Enabled = true;
                            }
                            DisablePageControls(true);
                        }

                        if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && (hdnRequestType.Value == RequestTypeID.Duplication.ToString()))
                        {
                            DisablePageControls(false);

                            if (IsAdd.Value == "1")
                            {
                                btnSubmit.Visible = true;
                                btnSubmit.Enabled = true;
                            }
                            btnBack.Enabled = true;

                        }

                        if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && (hdnRequestType.Value == RequestTypeID.Cancellation.ToString()))
                        {
                            DisablePageControls(false);

                            if (IsAdd.Value == "1")
                            {
                                btnSubmit.Visible = true;
                                btnSubmit.Enabled = true;
                            }
                            btnBack.Enabled = true;

                        }

                        /*
                        if (hdnApplicationStatusId.Value == StatusId.Disable.ToString())
                        {
                            btnPrint.Visible = false;

                            DataTable dt = new BAL_Application().usp_Application(OperationTypesID.GetRevised_DuplicationApplication, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                 null, null, null, null, null, null, UserId, UserIP, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                divAlertStep2.Visible = true;

                                if (dt.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Revision.ToString())
                                {
                                    string RequestType = "Revised";
                                    lblAlertDivStep2.Text = "This Application has been " + RequestType + ". New ARN Number is : " + dt.Rows[0]["Application_RefNo"].ToString();
                                }

                                if (dt.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
                                {
                                    string RequestType = "Duplicated";
                                    lblAlertDivStep2.Text = "This Application has been " + RequestType + ". New ARN Number is : " + dt.Rows[0]["Application_RefNo"].ToString();
                                }

                            }

                        }  */

                        #endregion


                    }
                    if (Request.QueryString["IsEdit"] != null && Request.QueryString["IsEdit"].ToString() == "True")
                    {
                        ViewState["IsEdit"] = "true";
                        hdnApplicationId.Value = Server.UrlEncode(Encryption.Decrypt(Request.QueryString["ApplicationId"].ToString()));
                        GetApplicationDetails(Convert.ToInt32(hdnApplicationId.Value));

                        if (hdnRequestType.Value == RequestTypeID.Duplication.ToString() || hdnRequestType.Value == RequestTypeID.Cancellation.ToString())
                        {
                            DisablePageControls(false);

                        }
                    }

                }
            }


        }

        FileUpload1.Attributes["onchange"] = "UploadFile(this)";

    }


    public void DisablePageControls(bool status)
    {
        foreach (Control c in upData.Controls)
        {
            foreach (Control ctrl in c.Controls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Enabled = status;
                else if (ctrl is Button)
                    ((Button)ctrl).Enabled = status;
                else if (ctrl is RadioButton)
                    ((RadioButton)ctrl).Enabled = status;
                else if (ctrl is RadioButtonList)
                    ((RadioButtonList)ctrl).Enabled = status;
                else if (ctrl is ImageButton)
                    ((ImageButton)ctrl).Enabled = status;
                else if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Enabled = status;
                else if (ctrl is CheckBoxList)
                    ((CheckBoxList)ctrl).Enabled = status;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).Enabled = status;
            }
        }

        if (status == false)
        {
            foreach (RepeaterItem item in rptChild.Items)
            {

                TextBox txtChild_New = (TextBox)item.FindControl("txtChild_New");
                TextBox txtChildDob_New = (TextBox)item.FindControl("txtChildDob_New");
                System.Web.UI.WebControls.Image ImgDelete = (System.Web.UI.WebControls.Image)item.FindControl("imgBtnDelete");

                txtChild_New.Enabled = false;
                txtChildDob_New.Enabled = false;
                btnAddMoreOption.Visible = false;
                ImgDelete.Visible = false;
            }
            btnAddMoreOption.Visible = false;
            txtChildName.Enabled = false;
            txtChildDob.Enabled = false;
            txtWifeHusband.Enabled = false;
        }

        else
        {
            txtChildName.Enabled = true;
            txtChildDob.Enabled = true;
            txtWifeHusband.Enabled = true;
            foreach (RepeaterItem item in rptChild.Items)
            {

                TextBox txtChild_New = (TextBox)item.FindControl("txtChild_New");
                TextBox txtChildDob_New = (TextBox)item.FindControl("txtChildDob_New");
                //      Image ImgDelete = (Image)item.FindControl("imgBtnDelete");

                txtChild_New.Enabled = true;
                txtChildDob_New.Enabled = true;
                btnAddMoreOption.Visible = true;
                // ImgDelete.Visible = true;
            }
            btnAddMoreOption.Visible = true;
        }
    }


    protected void CalculateDate_TextChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtDOB.Text))
        {
            Regex dateRegExp = new Regex("((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))");

            Match m = dateRegExp.Match(txtDOB.Text);
            if (m.Success)
            {
                DateTime Dob = Convert.ToDateTime(txtDOB.Text).Date;
                //int age = 0;
                //age = DateTime.Now.Subtract(Dob).Days;
                //age = age / 365;
                //txtApplicantAge.Text = age.ToString();


                var today = DateTime.Today;

                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (Dob.Year * 100 + Dob.Month) * 100 + Dob.Day;

                int age = (a - b) / 10000;
                txtApplicantAge.Text = age.ToString();
            }
            else
            {
                txtApplicantAge.Text = "0";
            }
        }
    }

    protected void ddlTaluka_Changed(object sender, EventArgs e)
    {
        txtMukhtiarkar.Text = txtDistrictOfficerTaluka.Text = txtAreaTaluka.Text = ddlTaluka.SelectedItem.Text;
        DataTable dt = new DataTable();
        dt = new BAL_Deh().usp_Setup_Deh(OperationTypesID.GetDehByTaluka, null, null, Convert.ToInt32(ddlTaluka.SelectedItem.Value), UserId, UserIP);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                CommonObjects.BindDropDown(ddlDeh, dt, "DehName", "DehId", true, false);
            }

            else
            {
                CommonObjects.BindDropDown(ddlDeh, null, "DehName", "DehId", true, false);
            }
        }
    }

    protected void rbdMaritial_CheckedChanged(object sender, EventArgs e)
    {
        if (rbdMaritialStatus.SelectedItem.Value != "1")
        {
            divChild.Visible = true;
            txtChildName.Text = "";
            txtChildDob.Text = "";
            rptChild.DataSource = null;
            rptChild.DataBind();
            btnSubmit.Focus();
        }
        else
        {
            divChild.Visible = false;
        }
    }

    public void BindDropDown()
    {
        // if (RoleId == UserRole.SuperAdmin || RoleId == UserRole.Approver)    // SuperAdmin and Admin
        // {
        DataTable dt_Taluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.Select, null, "", UserId, UserIP);
        CommonObjects.BindDropDown(ddlTaluka, dt_Taluka, "TalukaName", "TalukaId", true, false);
        //}

        //else if (RoleId == UserRole.Creator)   //Creator
        //{
        //    DataTable dt_Taluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.GetUserTalukaMapping, null, "", UserId, UserIP);
        //    CommonObjects.BindDropDown(ddlTaluka, dt_Taluka, "TalukaName", "TalukaId", true, false);
        //}

        DataTable dt_Guarduan = new BAL_Guardian().usp_tblGuardian(OperationTypesID.Select, 0);
        CommonObjects.BindDropDown(ddlGuardianRelationShip, dt_Guarduan, "GuardianRelationName", "GuardianRelationId", true, false);
        CommonObjects.BindDropDown(ddlGuardian2, dt_Guarduan, "GuardianRelationName", "GuardianRelationId", true, false);
    }

    protected void btnAddMoreOption_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;

            if (Session["RowNo"] == null)
                dt = new DataTable();
            else
                dt = (DataTable)Session["RowNo"];

            if (Session["RowNo"] == null || dt.Rows.Count == 0)
            {
                dt.Columns.Add(new DataColumn("RowNo", typeof(int)));
                dt.Columns.Add(new DataColumn("txtChild_New", typeof(string)));
                dt.Columns.Add(new DataColumn("txtChildDob_New", typeof(string)));
                AddNewRow(dt, 1);
            }
            else
            {
                SetPreviousData(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int rowNumber = dt.Rows.Count + 1;
                    AddNewRow(dt, rowNumber);
                }
            }

            Button btn = new Button();
            btn = (Button)sender;


            rptChild.DataSource = dt;
            rptChild.DataBind();

            //   Page.ClientScript.RegisterStartupScript(GetType(), "", "document.getElementById('btnSubmit').scrollIntoView(true);", true);
            btnSubmit.Focus();

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "btnAddMoreOption_Click", ex.Message);
        }
    }

    private void AddNewRow(DataTable dt, int rowNumber)
    {
        DataRow dr = null;
        dr = dt.NewRow();
        dr["RowNo"] = rowNumber;
        dt.Rows.Add(dr);

        Session["RowNo"] = dt;
    }

    protected void rptchild_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Delete":

                    int RowNumber = int.Parse(e.CommandArgument.ToString());
                    DataTable dt = Session["RowNo"] as DataTable;



                    //Session["RowNo"] = dt;

                    foreach (RepeaterItem item in rptChild.Items)
                    {
                        HiddenField hdnRow = (HiddenField)item.FindControl("hdnRow");
                        TextBox txtChild_New = (TextBox)item.FindControl("txtChild_New");
                        TextBox txtChildDob_New = (TextBox)item.FindControl("txtChildDob_New");

                        dt.Rows[item.ItemIndex]["RowNo"] = Convert.ToInt32(hdnRow.Value);
                        dt.Rows[item.ItemIndex]["txtChild_New"] = txtChild_New.Text;
                        dt.Rows[item.ItemIndex]["txtChildDob_New"] = txtChildDob_New.Text;
                    }
                    dt.Rows[RowNumber].Delete();
                    rptChild.DataSource = dt;
                    rptChild.DataBind();
                    if (dt.Rows.Count == 0)
                    { Session["RowNo"] = null; };

                    break;

            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "rptchild_ItemCommand", ex.Message);
        }
    }


    private void SetPreviousData(DataTable dt)
    {
        foreach (RepeaterItem item in rptChild.Items)
        {
            HiddenField hdnRow = (HiddenField)item.FindControl("hdnRow");
            TextBox txtChild_New = (TextBox)item.FindControl("txtChild_New");
            TextBox txtChildDob_New = (TextBox)item.FindControl("txtChildDob_New");

            dt.Rows[item.ItemIndex]["RowNo"] = Convert.ToInt32(hdnRow.Value);
            dt.Rows[item.ItemIndex]["txtChild_New"] = txtChild_New.Text;
            dt.Rows[item.ItemIndex]["txtChildDob_New"] = txtChildDob_New.Text;
        }

    }


    private bool CheckFileType()
    {
        string filePath = FileUpload1.PostedFile.FileName;
        string filename = Path.GetFileName(filePath);
        string ext = Path.GetExtension(filename);
        string contenttype = String.Empty;
        Stream checkStream = FileUpload1.PostedFile.InputStream;
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

    protected void Upload(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                if (CheckFileType())
                {
                    int maxFileSize = 10;
                    long FileSize = FileUpload1.FileContent.Length / 1024;
                    if (FileSize > (maxFileSize * 1024))
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please Upload Image up to 10MB.";
                    }

                    else
                    {
                        string folderPath = Server.MapPath("~/Applicant_Image/");

                        //Check whether Directory (Folder) exists.
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists Create it.
                            Directory.CreateDirectory(folderPath);
                        }


                        string FileName = DateTime.Now.ToString("dd-MMMM-yyyy") + "_" + txtApplicantName.Text.Replace(" ", "_") + "_" + txtDOB.Text + "_" + FileUpload1.FileName;
                        //Save the File to the Directory (Folder).

                        //   RequiredFieldValidator18.Enabled = false;
                        FileUpload1.SaveAs(folderPath + Path.GetFileName(FileName));

                        //Display the Picture in Image control.
                        ViewState["ApplicantPhoto_Path"] = "~/Applicant_Image/" + Path.GetFileName(FileName);
                        Image1.ImageUrl = "~/Applicant_Image/" + Path.GetFileName(FileName);
                        lblMessage.Visible = false;
                        divPic.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "You can upload only jpg , jpeg , png files.";
                    Image1.ImageUrl = "";
                    divPic.Visible = false;
                }
            }

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "Upload", ex.Message);
        }

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {

        // Creator and SuperAdmin end

        if (ViewState["IsEdit"] != null && ViewState["IsEdit"].ToString() == "true")
        {
            Session["lblSuccess"] = "Application Updated Successfully.";
            Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value);
        }
        else if (ViewState["IsView"] != null && ViewState["IsView"].ToString() == "true")
        {
            if (ViewState["IsApproved"] != null && ViewState["IsApproved"].ToString() != "")
            {
                Session["lblSuccess"] = "Application Approved Successfully.";
                Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value + "&ApplicationStatusId=" + StatusId.Approved);
            }
            else if ((hdnRequestType.Value == RequestTypeID.Revision.ToString()) || (hdnRequestType.Value == RequestTypeID.Duplication.ToString()))
            {
                if (ViewState["IsSubmit"] != null && ViewState["IsSubmit"].ToString() != "false")
                {
                    Response.Redirect("ViewApplicationSaved.aspx");
                }
                else
                {
                    Session["lblSuccess"] = "Application Created Successfully.";
                    Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value + "&ApplicationStatusId=" + StatusId.InitialDraft);
                }
            }
            else
            {
                Response.Redirect("ViewApplicationSaved.aspx");
            }
        }
        else
        {
            Session["lblSuccess"] = "Application Created Successfully.";
            Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value + "&ApplicationStatusId=" + StatusId.InitialDraft);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("ViewApplicationSaved.aspx");

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Button button = (Button)sender;
            string buttonId = button.ID;

            if (hdnApplicationType.Value == ApplicationTypeValueId.Major.ToString() && Convert.ToInt32(txtApplicantAge.Text) < 18)
            {
                Error("Below 18 Years Application Not allowed in Major Case");
                return;
            }
            else if (hdnApplicationType.Value == ApplicationTypeValueId.Minor.ToString() && Convert.ToInt32(txtApplicantAge.Text) >= 18
                && hdnRequestType.Value == RequestTypeID.New_Request.ToString())
            {
                Error("18 Years or Above Application Not allowed in Minor Case");
                return;
            }


            if (buttonId == "btnAdd")
            {
                hdnCNIC.Value = "0";
            }
            DataTable dtRecord_CNIC = new DataTable();
            if (txtCnic.Text == "")
                dtRecord_CNIC = null;
            else
            {
                dtRecord_CNIC = new BAL_Application().usp_Application(OperationTypesID.OtIsExist, null, null, null, null, null, null, null, null, null,
                txtCnic.Text, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                UserId, UserIP, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null);
            }




            if (dtRecord_CNIC != null && dtRecord_CNIC.Rows.Count > 0 && hdnCNIC.Value == "1")
            {
                OpenPopup();
                lblARNNumber.Text = dtRecord_CNIC.Rows[0]["Application_RefNo"].ToString();
                lblDomicileNumber.Text = dtRecord_CNIC.Rows[0]["Domicile_No"].ToString();
                lblName.Text = dtRecord_CNIC.Rows[0]["Applicant_Name"].ToString();
                lblFatherName.Text = dtRecord_CNIC.Rows[0]["Father_Name"].ToString();
            }
            else
            {
                int ApplicationTypeId = 0;
                int RequestTypeId = 0;

                if (hdnApplicationType.Value != "" && hdnRequestType.Value != "")
                {
                    ApplicationTypeId = Convert.ToInt32(hdnApplicationType.Value);
                    RequestTypeId = Convert.ToInt32(hdnRequestType.Value);
                }


                int LastId = 1;

                DataTable dtLastRecord = new BAL_Application().usp_Application(OperationTypesID.GetLastRecord_ID, null, null, null, null, null, null, null, null, null,
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



                if (dtLastRecord != null && dtLastRecord.Rows.Count > 0)
                {
                    LastId = Convert.ToInt32(dtLastRecord.Rows[0]["ApplicationId"].ToString());
                    LastId = LastId + 1;

                }

                int StatusIdd = 0;


                if (hdnApplicationStatusId.Value != "")
                    StatusIdd = Convert.ToInt32(hdnApplicationStatusId.Value);

                else
                    StatusIdd = StatusId.InitialDraft;

                string Application_RefNo = "";
                string Domicile_No = "";
                string FormC_No = "";
                string FormD_No = "";
                int Application_RevisedDuplicate = 0;
                DateTime? DomicileApprovedDate = null;

                // MAJOR CASE Start
                if (hdnRequestType.Value == RequestTypeID.New_Request.ToString() && hdnApplicationType.Value == ApplicationTypeValueId.Major.ToString())  // Revise Domicile 
                {
                    Application_RefNo = "DMC/MAJ/" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    Domicile_No = "DMC/MAJ/00" + LastId;
                    FormC_No = "SPRC/MAJ" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    int FormDNo = LastId + 1;
                    FormD_No = "SPRC/MAJ" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + FormDNo;
                }
                // MAJOR CASE End


                // MINOR CASE Start
                if (hdnRequestType.Value == RequestTypeID.New_Request.ToString() && hdnApplicationType.Value == ApplicationTypeValueId.Minor.ToString())  // Revise Domicile 
                {
                    Application_RefNo = "DMC/MIN18/" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    Domicile_No = "DMC/MIN18/00" + LastId;
                    FormC_No = "SPRC/MIN18" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    int FormDNo = LastId + 1;
                    FormD_No = "SPRC/MIN18" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + FormDNo;
                }
                // MINOR CASE End


                // Revise Domicile Start

                if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && hdnRequestType.Value == RequestTypeID.Revision.ToString())
                {
                    Application_RefNo = "DMC/REV/" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    Domicile_No = "DMC/REV/00" + LastId;
                    FormC_No = "SPRC/REV" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    int FormDNo = LastId + 1;
                    FormD_No = "SPRC/REV" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + FormDNo;
                    Application_RevisedDuplicate = Convert.ToInt32(hdnApplicationId.Value);
                    StatusIdd = StatusId.InitialDraft;
                }
                // Revise Domicile End


                // Duplicate Domicile Start

                if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && hdnRequestType.Value == RequestTypeID.Duplication.ToString())
                {
                    Application_RefNo = "DMC/DUP/" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    Domicile_No = "DMC/DUP/00" + LastId;
                    FormC_No = "SPRC/DUP" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    int FormDNo = LastId + 1;
                    FormD_No = "SPRC/DUP" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + FormDNo;
                    Application_RevisedDuplicate = Convert.ToInt32(hdnApplicationId.Value);
                    StatusIdd = StatusId.InitialDraft;

                    if (ViewState["DomicileApprovalDate"] != null && ViewState["DomicileApprovalDate"].ToString() != "")
                    {
                        DomicileApprovedDate = Convert.ToDateTime(ViewState["DomicileApprovalDate"].ToString());
                    }
                }

                // Dulicate Domicile End



                // Cancellation Domicile Start

                if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && hdnRequestType.Value == RequestTypeID.Cancellation.ToString())
                {
                    Application_RefNo = "DMC/CAN/" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    Domicile_No = "DMC/CAN/00" + LastId;
                    FormC_No = "SPRC/CAN" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + LastId;
                    int FormDNo = LastId + 1;
                    FormD_No = "SPRC/CAN" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + FormDNo;
                    Application_RevisedDuplicate = Convert.ToInt32(hdnApplicationId.Value);
                    StatusIdd = StatusId.InitialDraft;
                }
                // Cancellation Domicile End



                int? ApplicationStatusId = StatusIdd;
                string Title = ddlTitle.SelectedValue == null ? "0" : ddlTitle.SelectedValue;
                string Applicant_Name = txtApplicantName.Text == null ? "" : txtApplicantName.Text.Trim();
                string Father_Name = txtFatherName.Text == "" ? null : txtFatherName.Text.Trim();
                string Applicant_Cnic = txtCnic.Text == "" ? null : txtCnic.Text.Trim();
                DateTime Date_of_Birth = txtDOB.Text == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(txtDOB.Text).Date;
                string Place_of_Birth = txtPlaceofBirth.Text == "" ? null : txtPlaceofBirth.Text.Trim();
                string Resident_of = txtResident.Text == "" ? null : txtResident.Text.Trim();
                string Surname = txtSurname.Text == "" ? null : txtSurname.Text.Trim();
                string Primary_School = ddlPrimarySchool.SelectedValue;
                string Town_PrimarySchool = txtTownVillagePrimary.Text == "" ? null : txtTownVillagePrimary.Text.Trim();
                string FromDate_PrimarySchool = txtFromDatePrimary.Text == "" ? null : txtFromDatePrimary.Text.Trim();
                string ToDate_PrimarySchool = txtToDatePrimary.Text == "" ? null : txtToDatePrimary.Text.Trim();
                string Middle_School = ddlMiddleSchool.SelectedValue;
                string Town_MiddleSchool = txtTownVillageMiddle.Text == "" ? null : txtTownVillageMiddle.Text.Trim();
                string FromDate_MiddleSchool = txtFromDateMiddle.Text == "" ? null : txtFromDateMiddle.Text.Trim();
                string ToDate_ModdleSchool = txtToDateMiddle.Text == "" ? null : txtToDateMiddle.Text.Trim();
                string High_School = ddlHighSchool.SelectedValue;
                string Town_HighSchool = txtTownVillageHigh.Text == "" ? null : txtTownVillageHigh.Text.Trim();
                string FromDate_HighSchool = txtFromDateHigh.Text == "" ? null : txtFromDateHigh.Text.Trim();
                string ToDate_HighSchool = txtToDateHigh.Text == "" ? null : txtToDateHigh.Text.Trim();
                string District_Education = txtDistrictEducation.Text == "" ? null : txtDistrictEducation.Text.Trim();
                DateTime Date_Submission = txtDateSubmission.Text == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(txtDateSubmission.Text).Date;
                DateTime Date_Issue = txtIssueDate.Text == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(txtIssueDate.Text).Date;
                string ParticularsProperty = txtParticularProperty.Text == "" ? null : txtParticularProperty.Text.Trim();
                string Location = txtLocation.Text == "" ? null : txtLocation.Text.Trim();
                int? TalukaId = Convert.ToInt32(ddlTaluka.SelectedValue);
                string District = txtDistrict.Text == "" ? null : txtDistrict.Text.Trim();
                string Electoral_Area = txtSnoElectoralArea.Text == "" ? null : txtSnoElectoralArea.Text.Trim();
                string Electoral_Area_Taluka = txtAreaTaluka.Text == "" ? null : txtAreaTaluka.Text.Trim();
                int? Electoreal_Area_Deh = Convert.ToInt32(ddlDeh.SelectedValue);
                string Guardian_NIC = txtGuardiansCnic.Text == "" ? null : txtGuardiansCnic.Text.Trim();
                int Guardian_RelationShip = Convert.ToInt32(ddlGuardianRelationShip.SelectedValue);
                string Applicant_PhoneNo = txtPhoneNo.Text == "" ? null : txtPhoneNo.Text.Trim();
                string Temporary_Address = txtTempAddress.Text == "" ? null : txtTempAddress.Text.Trim();
                string Permanent_Address = txtPermanentAddress.Text == "" ? null : txtPermanentAddress.Text.Trim();
                DateTime? GuardianDomicile_CertificateDate = txtGuardianDomicileDate.Text == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(txtGuardianDomicileDate.Text).Date;
                int Guardian_RelationShip2 = Convert.ToInt32(ddlGuardian2.SelectedValue);
                int? Applicant_Age = 0;
                string Trade_Occupation = txtTradeOccupation.Text == "" ? null : txtTradeOccupation.Text.Trim();
                string Mark_of_Identification = txtMarkIdentification.Text == "" ? null : txtMarkIdentification.Text.Trim();
                DateTime Date_of_Arrival = txtDateofArrival.Text == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(txtDateofArrival.Text).Date;
                string Applicant_Photo_Path = "";
                if (ViewState["ApplicantPhoto_Path"] != null && ViewState["ApplicantPhoto_Path"].ToString() != "")
                {
                    Applicant_Photo_Path = ViewState["ApplicantPhoto_Path"].ToString();
                }
                string Address_ForeignCountry = txtForeignAddress.Text == "" ? null : txtForeignAddress.Text.Trim();
                string Mukhtiarkar_Taluka = txtMukhtiarkar.Text == "" ? null : txtMukhtiarkar.Text.Trim();
                string Deputy_District_Officer_Taluka = txtDistrictOfficerTaluka.Text == "" ? null : txtDistrictOfficerTaluka.Text.Trim();
                string Husband_Wife_Name = txtWifeHusband.Text == "" ? null : txtWifeHusband.Text.Trim();
                string Marital_Status = rbdMaritialStatus.SelectedValue;
                int? CreatedBy = UserId;
                bool IsActive = true;
                int? ModifiedBy = Nullint;
                string IP = UserIP;
                bool IsByBirth = false;

                if (ChkByBirth.Checked)
                    IsByBirth = true;

                int OperationType = OperationTypesID.Insert;
                if (ViewState["IsEdit"] != null && ViewState["IsEdit"].ToString() == "true")
                {
                    OperationType = OperationTypesID.Update;
                }

                int? ApplicationId;
                if (hdnApplicationId.Value == "")
                { ApplicationId = null; }
                else
                {
                    ApplicationId = Convert.ToInt32(hdnApplicationId.Value);

                    DataTable dtIssuedDocsRecord = new BAL_IssuedDocumentsRecord().usp_Setup_IssuedDocumentsRecord(OperationTypesID.Select, DocumentType.FormD, Convert.ToInt32(hdnApplicationId.Value), null, true, UserId, UserIP);
                    if (dtIssuedDocsRecord != null && dtIssuedDocsRecord.Rows.Count == 0)
                    {
                        int FormDNo = LastId + 1;
                        if (hdnApplicationType.Value == ApplicationTypeValueId.Major.ToString())
                        {
                            FormD_No = "SPRC/MAJ" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + FormDNo;
                        }
                        else if (hdnApplicationType.Value == ApplicationTypeValueId.Minor.ToString())
                        {
                            FormD_No = "SPRC/MIN" + (DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"))) + "/00" + FormDNo;
                        }
                    }

                }

                DataTable dt = new BAL_Application().usp_Application(OperationType, ApplicationId, Application_RefNo, Domicile_No, ApplicationTypeId, RequestTypeId, ApplicationStatusId, Title, Applicant_Name, Father_Name, Applicant_Cnic, Date_of_Birth, Place_of_Birth, Resident_of,
                    Surname, Primary_School, Town_PrimarySchool, FromDate_PrimarySchool, ToDate_PrimarySchool, Middle_School, Town_MiddleSchool, FromDate_MiddleSchool, ToDate_ModdleSchool, High_School, Town_HighSchool, FromDate_HighSchool, ToDate_HighSchool,
                    District_Education, Date_Submission, Date_Issue, ParticularsProperty, Location, TalukaId, District, Electoral_Area, Electoral_Area_Taluka, Electoreal_Area_Deh, Guardian_NIC, Guardian_RelationShip, Applicant_PhoneNo, Temporary_Address, Permanent_Address,
                    GuardianDomicile_CertificateDate, Guardian_RelationShip2, Applicant_Age, Trade_Occupation, Mark_of_Identification, Date_of_Arrival, Applicant_Photo_Path, Address_ForeignCountry, Mukhtiarkar_Taluka, Deputy_District_Officer_Taluka, Husband_Wife_Name, Marital_Status, null, null, null, null, null, null, UserId, UserIP, null
                    , null, null, null, null, null, null, null, null, null, null, null, null, null, DomicileApprovedDate, FormC_No, FormD_No, Application_RevisedDuplicate, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, IsByBirth);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {

                        #region Revise CASE : Disable last application in Revise/Duplicate/Cancellation Domicile case and transfer documents in new application

                        int LastApplicationId = Convert.ToInt32(dt.Rows[0]["ApplicationId"].ToString());

                        if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && (hdnRequestType.Value == RequestTypeID.Revision.ToString() || hdnRequestType.Value == RequestTypeID.Duplication.ToString() || hdnRequestType.Value == RequestTypeID.Cancellation.ToString())
                            && (ViewState["IsView"] !=null && ViewState["IsView"].ToString() == "true"))
                        {
                            DataTable dtStatusDisable = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, StatusId.Disable, null, null, null,
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


                            if (dtStatusDisable.Rows[0]["HasError"].ToString() == "1")
                            {
                                Error(dtStatusDisable.Rows[0]["Message"].ToString());
                            }
                            else if (dtStatusDisable.Rows[0]["HasError"].ToString() == "0")
                            {
                                /*

                                DataTable dtDocs = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                null, null, null, null, null, null, UserId, UserIP, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                                if (dtDocs != null && dtDocs.Rows.Count > 0)
                                {
                                    string CNIC_Front = dtDocs.Rows[0]["CNIC_Front"].ToString();
                                    string CNIC_Back = dtDocs.Rows[0]["CNIC_Back"].ToString();
                                    string ASSISTANT_COMMISSIONERS_REPORT_Path = dtDocs.Rows[0]["ASSISTANT_COMMISSIONERS_REPORT_Path"].ToString();
                                    string MUKHTIARKAR_REPORT_Path = dtDocs.Rows[0]["MUKHTIARKAR_REPORT_Path"].ToString();
                                    string PRIMARY_CERTIFICATE_Path = dtDocs.Rows[0]["PRIMARY_CERTIFICATE_Path"].ToString();
                                    string MATRIC_CERTIFICATE_Path = dtDocs.Rows[0]["MATRIC_CERTIFICATE_Path"].ToString();
                                    string RESIDENCE_CERTIFICATE_Path = dtDocs.Rows[0]["RESIDENCE_CERTIFICATE_Path"].ToString();
                                    string VOTE_CERTIFICATE_Path = dtDocs.Rows[0]["VOTE_CERTIFICATE_Path"].ToString();
                                    string GUARDIANS_DOMICILE_Path = dtDocs.Rows[0]["GUARDIANS_DOMICILE_Path"].ToString();
                                    string BANK_CHALLANS_Path = dtDocs.Rows[0]["BANK_CHALLANS_Path"].ToString();
                                    string OTHER_DOCUMENT1_Path = dtDocs.Rows[0]["OTHER_DOCUMENT1_Path"].ToString();
                                    string OTHER_DOCUMENT2_Path = dtDocs.Rows[0]["OTHER_DOCUMENT2_Path"].ToString();

                                    DataTable dtUpdateDocs = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationDocuments, LastApplicationId, null, null, null, null, 2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, UserId, UserIP, null
      , CNIC_Front, CNIC_Back, ASSISTANT_COMMISSIONERS_REPORT_Path, MUKHTIARKAR_REPORT_Path, PRIMARY_CERTIFICATE_Path, MATRIC_CERTIFICATE_Path, RESIDENCE_CERTIFICATE_Path, VOTE_CERTIFICATE_Path, GUARDIANS_DOMICILE_Path,
      BANK_CHALLANS_Path, OTHER_DOCUMENT1_Path, OTHER_DOCUMENT2_Path, null, null, null, null, null, null, null, null, null, null, null, null);
                                    if (dtUpdateDocs != null && dtUpdateDocs.Rows.Count > 0)
                                    {

                                    }

                                }


                                */
                            }

                        }

                        #endregion


                        Success(dt.Rows[0]["Message"].ToString());

                        hdnApplicationId.Value = dt.Rows[0]["ApplicationId"].ToString();


                        if (ViewState["IsEdit"] != null && ViewState["IsEdit"].ToString() == "true")
                        {
                            DataTable dtChild_Delete = new BAL_Application().usp_Application(OperationTypesID.DeleteChildByApplicationId, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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

                        }


                        DataTable dtchild = new DataTable();
                        dtchild.Columns.Add("Id", typeof(int));
                        dtchild.Columns.Add("ApplicationId", typeof(int));
                        dtchild.Columns.Add("Child_Name", typeof(string));
                        dtchild.Columns.Add("Child_DoB", typeof(string));
                        dtchild.Columns.Add("CreatedDate", typeof(DateTime));
                        dtchild.Columns.Add("CreatedBy", typeof(int));
                        dtchild.Columns.Add("IsActive", typeof(bool));
                        dtchild.Columns.Add("ModifiedBy", typeof(int));
                        dtchild.Columns.Add("ModifiedDate", typeof(DateTime));
                        dtchild.Columns.Add("UserIp", typeof(string));

                        if (txtChildName.Text != "" && txtChildDob.Text != "")
                            dtchild.Rows.Add(0, Convert.ToInt32(hdnApplicationId.Value), txtChildName.Text, txtChildDob.Text, DateTime.Now, UserId, 1, 0, "1900/01/01", UserIP);

                        for (int i = 0; i < rptChild.Items.Count; i++)
                        {
                            TextBox txtChild = rptChild.Items[i].FindControl("txtChild_New") as TextBox;
                            TextBox txtChildDob_New = rptChild.Items[i].FindControl("txtChildDob_New") as TextBox;

                            if (txtChild.Text != "" && txtChildDob_New.Text != "")
                                dtchild.Rows.Add(0, Convert.ToInt32(hdnApplicationId.Value), txtChild.Text, txtChildDob_New.Text, DateTime.Now, UserId, 1, 0, "1900/01/01", UserIP);
                        }

                        if (dtchild.Rows.Count > 0)
                        {
                            DataTable dtChild_Insert = new BAL_Application().usp_Application(OperationTypesID.InsertChild_BulkRecord, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, StatusId.Cancelled, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                UserId, UserIP, dtchild, null, null, null, null, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                null, null, null, null, null, null, null, null, null, null,
                                                                                                null, null, null);

                            if (dtChild_Insert.Rows[0]["HasError"].ToString() == "1")
                            {
                                Error(dtChild_Insert.Rows[0]["Message"].ToString());
                            }
                            else if (dtChild_Insert.Rows[0]["HasError"].ToString() == "0")
                            {
                                //Session["lblSuccess"] = "Application Created Successfully.";
                                //Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + ApplicationId);
                                GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                                divReceipt.Visible = true;
                                divApplciationFormPDF.Visible = true;
                                divApplciationFormExcel.Visible = true;
                                if (ViewState["IsEdit"] != null)
                                {
                                    Session["lblSuccess"] = "Application Updated Successfully.";
                                    Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value);
                                }
                                else if (hdnRequestType.Value == RequestTypeID.Cancellation.ToString())
                                {
                                    Session["lblSuccess"] = "Application Created Successfully.";
                                    Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value);
                                }
                                else
                                {
                                    OpenSuccessModal();
                                }
                            }
                        }

                        GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                        divReceipt.Visible = true;
                        divApplciationFormPDF.Visible = true;
                        divApplciationFormExcel.Visible = true;
                        if (ViewState["IsEdit"] != null)
                        {
                            Session["lblSuccess"] = "Application Updated Successfully.";
                            Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value);
                        }
                        else if (hdnRequestType.Value == RequestTypeID.Cancellation.ToString())
                        {
                            Session["lblSuccess"] = "Application Created Successfully.";
                            Response.Redirect("ViewApplicationSaved.aspx?ApplicationId=" + hdnApplicationId.Value);
                        }
                        else
                        {
                            OpenSuccessModal();
                        }
                    }
                }



            }
        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "btnSubmit_Click", ex.Message);
        }
    }

    private void SetFeature()
    {
        try
        {
            btnSubmit.Visible = false;
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
                        btnSubmit.Visible = true;
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

    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }



    protected void CalculateDeliveryDate()
    {

        DataTable dt = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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
            DataTable dtDeliveryDay = new BAL_DeliveryDateDays().usp_Setup_DeliveryDateDays(OperationTypesID.Select, null, null, null, null);
            int TotalDeliveryDay = Convert.ToInt32(dtDeliveryDay.Rows[0]["DeliveryDateDays"].ToString());
            DateTime DeliveryDay = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString()).AddDays(TotalDeliveryDay);
            string Day = DeliveryDay.DayOfWeek.ToString();

            if (Day == "Saturday")
                DeliveryDay = DeliveryDay.AddDays(2);

            if (Day == "Sunday")
                DeliveryDay = DeliveryDay.AddDays(1);

            txtDeliveryDate.Text = DeliveryDay.ToString("yyyy-MM-dd");
            txtDeliveryDate.Enabled = false;
        }
    }


    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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
                OpenSuccessModal();

            }

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "btnReceipt_Click", ex.Message);
        }

    }

    protected void btnApplicationForm_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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
                FinalMiddleArea.Replace("@Taluka", "<u>" + TalukaName + "</u>");
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
                FinalLowerArea.Replace("@Taluka", "<u>" + TalukaName + "</u>");
                FinalLowerArea.Replace("@District", "<u>" + "Khairpur" + "</u>");

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

                string FileType = "ApplicationForm_" + hdnApplicationId.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + UserId + "/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + UserId + "/" + fileName + "" + "');", true);

                OpenSuccessModal();
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
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "btnApplicationForm_Click", ex.Message);
        }
    }

    protected void btnPrintDomicile_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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
                string HeaderArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Header_Area"].ToString() + "</body></html>";
                ReportParameter parameters2 = new ReportParameter("HeaderArea", HeaderArea);
                viewerDetail.LocalReport.SetParameters(parameters2);

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

                string Date_Arrival = dtReport.Rows[0]["Domicile_ApprovedDate"].ToString() == "" ? "__________" : Convert.ToDateTime(dtReport.Rows[0]["Domicile_ApprovedDate"].ToString()).ToString("dd/MM/yyyy");

                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }

                FinalMiddleArea.Replace("@Name", "<u>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@FName", "<u>" + dtReport.Rows[0]["Father_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@Single/Married/Widow", "<u>" + MaritialStatus + "</u>");
                FinalMiddleArea.Replace("@Wife_HusbandName", "<u>" + dtReport.Rows[0]["Husband_Wife_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@PermanentAddress", " <u>" + dtReport.Rows[0]["Permanent_Address"].ToString() + "</u>");
                FinalMiddleArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalMiddleArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalMiddleArea.Replace("@DomicileApprovalDate", "<u>" + Date_Arrival + "</u>");
                FinalMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                FinalMiddleArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());

                ReportParameter parameters3 = new ReportParameter("MiddleArea", FinalMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters3);

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder FinalLowerArea = new StringBuilder(LowerArea);

                FinalLowerArea.Replace("@TradeOccupation", "<u>" + dtReport.Rows[0]["Trade_Occupation"].ToString() + "</u>");
                FinalLowerArea.Replace("@MarkIdentification", "<u>" + dtReport.Rows[0]["Mark_of_Identification"].ToString() + "</u>");
                FinalLowerArea.Replace("@DomicileApprovalDate", "<u>" + Date_Arrival + "</u>");
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

                DataTable dtChild = new BAL_Application().usp_Application(OperationTypesID.GetChildByApplicationId, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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

                System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/"));
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "Domicile_" + hdnApplicationId.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + fileName + "" + "');", true);

                GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));

                OpenSuccessModal();

                if ((ViewState["IsDuplicate"] != null && ViewState["IsDuplicate"].ToString() == "True") || (hdnRequestType.Value == RequestTypeID.Duplication.ToString()))
                    DuplicateDocumentsRecord(DocumentType.Domicile);

            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "btnApplicationForm_Click", ex.Message);
        }
    }


    protected void btnPrintFormCTest_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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
                FinalMiddleArea.Replace("@Name", "<u>" + dtReport.Rows[0]["Applicant_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@FName", "<u>" + dtReport.Rows[0]["Father_Name"].ToString() + "</u>");
                FinalMiddleArea.Replace("@Single/Married/Widow", MaritialStatus);
                FinalMiddleArea.Replace("@Wife_HusbandName", dtReport.Rows[0]["Husband_Wife_Name"].ToString());
                FinalMiddleArea.Replace("@PermanentAddress", dtReport.Rows[0]["Permanent_Address"].ToString());
                FinalMiddleArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalMiddleArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalMiddleArea.Replace("@DomicileApprovalDate", Date_Arrival);
                FinalMiddleArea.Replace("@Taluka", "<u>" + TalukaName + "</u>");
                FinalMiddleArea.Replace("@Place", "KHAIRPUR");
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

                ReportParameter parameters7 = new ReportParameter("FormCNumber", dtReport.Rows[0]["FormC_No"].ToString());
                viewerDetail.LocalReport.SetParameters(parameters7);


                string DeputyCommissioner = "";
                DataTable dtCommissioner = new DataTable();
                DateTime CommissionerDate = DateTime.ParseExact(Date_Arrival, "dd/MM/yyyy", null);
                string CommissionerDate_sqlFormattedDate = CommissionerDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                dtCommissioner = new BAL_DeputyCommissioner().usp_Setup_DeputyCommissioner(OperationTypesID.Select, null, null, null, null, Convert.ToDateTime(CommissionerDate_sqlFormattedDate), UserId, UserIP);
                if (dtCommissioner != null && dtCommissioner.Rows.Count > 0)
                { DeputyCommissioner = dtCommissioner.Rows[0]["CommisionerName"].ToString(); }

                string LowerArea = "<!DOCTYPE html><html><body>" + dt.Rows[0]["Lower_Area"].ToString() + "</body></html>";
                StringBuilder LowerMiddleArea = new StringBuilder(LowerArea);
                LowerMiddleArea.Replace("@DeputyCommissioner", DeputyCommissioner);
                ReportParameter parameters8 = new ReportParameter("LowerArea", LowerMiddleArea.ToString());
                viewerDetail.LocalReport.SetParameters(parameters8);

                ReportParameter parameters11 = new ReportParameter("CommissionerName", "SAIFULLAH ABRO");
                viewerDetail.LocalReport.SetParameters(parameters11);

                string Header_Area = dt.Rows[0]["Header_Area"].ToString();
                StringBuilder FinalHeaderArea = new StringBuilder(Header_Area);
                FinalHeaderArea.Replace("CERTIFICATE", "<u>CERTIFICATE</u>");
                FinalHeaderArea.Replace("@Name", "<u><b>" + dtReport.Rows[0]["Applicant_Name"].ToString().ToUpper() + "</b></u>");
                FinalHeaderArea.Replace("@FName", "<u><b><span STYLE=font - size:18.0pt>" + dtReport.Rows[0]["Father_Name"].ToString().ToUpper() + "</span></b></u> Taluka <u><b>" + TalukaName + "</b></u> District <u><b>KHAIRPUR</b></u>");
                FinalHeaderArea.Replace("@Single/Married/Widow", "<u>" + MaritialStatus + "</u>");
                FinalHeaderArea.Replace("@Wife_HusbandName", "<u>" + dtReport.Rows[0]["Husband_Wife_Name"].ToString() + "</u>");
                FinalHeaderArea.Replace("@PermanentAddress", " <u><b>" + dtReport.Rows[0]["Permanent_Address"].ToString().ToUpper() + "</b></u> Taluka <u><b>" + TalukaName + "</b></u> District <u><b>KHAIRPUR</b></u>");
                FinalHeaderArea.Replace("@ForeignAddress", " <u>" + dtReport.Rows[0]["Address_ForeignCountry"].ToString() + "</u>");
                FinalHeaderArea.Replace("@DateArrival", " <u>" + Convert.ToDateTime(dtReport.Rows[0]["Date_of_Arrival"].ToString()).ToString("dd/MM/yyyy") + "</u>");
                FinalHeaderArea.Replace("@DomicileApprovalDate", "<u>" + Date_Arrival + "</u>");
                FinalHeaderArea.Replace("@Taluka", " <u><b>" + TalukaName + "</b></u>");
                FinalHeaderArea.Replace("@District", "<u>" + dtReport.Rows[0]["District"].ToString() + "</u>");
                FinalHeaderArea.Replace("@PlaceOfBirth", dtReport.Rows[0]["Place_of_Birth"].ToString());
                // FinalHeaderArea.Replace("@Educatedat", "<u>" + txtEducation.Text + "</u>");
                FinalHeaderArea.Replace("@Educatedat", "LARKANA");
                FinalHeaderArea.Replace("@Cast", dtReport.Rows[0]["Surname"].ToString());

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

                System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/"));
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                //viewerSumarry.SetPageSettings(pg);
                byte[] bytes2 = viewerDetail.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FileType = "FormC_" + hdnApplicationId.Value;
                string fileName = UniqueTemporaryFileName_Application(FileType, ".pdf");
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("DocumentFiles/TempPDF/" + fileName + ""), FileMode.Create);
                fs.Write(bytes2, 0, bytes2.Length);
                fs.Close();



                //  SetCustomFontAndSize("DocumentFiles/TempPDF/" + fileName + "");


                // Save resulting PDF document.



                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DocumentFiles/TempPDF/" + fileName + "" + "');", true);


                GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));

                OpenSuccessModal();

                if ((ViewState["IsDuplicate"] != null && ViewState["IsDuplicate"].ToString() == "True") || (hdnRequestType.Value == RequestTypeID.Duplication.ToString()))
                    DuplicateDocumentsRecord(DocumentType.FormC);


            }

        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "btnPrintFormC_Click", ex.Message);
        }
    }



    public static void SetCustomFontAndSize(string FilePath)
    {
        // Path to a loadable document.
        string inpFile = HttpContext.Current.Server.MapPath(FilePath);
        string outFile = HttpContext.Current.Server.MapPath(FilePath);

        DocumentCore dc = DocumentCore.Load(inpFile);

        string singleFontName = "Arial";
        float singleFontSize = 11.0f;
        float singleLineSpacing = 1.4f;

        dc.DefaultCharacterFormat.FontName = singleFontName;
        dc.DefaultCharacterFormat.Size = singleFontSize;

        foreach (Element element in dc.GetChildElements(true, ElementType.Run, ElementType.Paragraph))
        {
            if (element is Run)
            {
                //   (element as Run).CharacterFormat.FontName = singleFontName;
                //  (element as Run).CharacterFormat.Size = singleFontSize;
            }
            else if (element is Paragraph)
            {
                (element as Paragraph).ParagraphFormat.LineSpacing = singleLineSpacing;
            }
        }
        dc.Save(outFile);



        // Open the result for demonstration purposes.

        //System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(inpFile) { UseShellExecute = true });
        //System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outFile) { UseShellExecute = true });
    }



    //public static void FintTextInPDFAndReplaceIt(PdfDocument documents, Dictionary<string, string> dictionary)
    //{
    //    PdfTextFind[] result = null;
    //    foreach (var word in dictionary)
    //    {
    //        foreach (PdfPageBase page in documents.Pages)
    //        {
    //            result = PdfTextFinder.Find(word.Key);
    //            result = page.FindText(word.Key).Finds;
    //            foreach (PdfTextFind find in result)
    //            {
    //                //replace word in pdf                   
    //                find.ApplyRecoverString(word.Value, System.Drawing.Color.White, true);
    //            }
    //        }
    //    }
    //}




    protected void btnApplicationFormExcel_Click(object sender, EventArgs e)
    {


        try
        {
            DataTable dtReport = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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
                FinalMiddleArea.Replace("@Taluka", TalukaName);
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

                string FileType = "ApplicationForm_" + hdnApplicationId.Value;
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

                OpenSuccessModal();
                //GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                divReceipt.Visible = true;
                divApplciationFormPDF.Visible = true;
                divApplciationFormExcel.Visible = true;

            }
        }



        catch (Exception ex)
        {
            Logger.WriteErrorLog("DomicileApplication_Major.aspx", "btnApplicationFormExcel_Click", ex.Message);
        }


    }

    public void OpenSuccessModal()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenSuccessModal()", "OpenSuccessModal();", true);
    }

    public void GetARNNumber(int ApplicationId)
    {
        DataTable dt = new BAL_Application().usp_Application(OperationTypesID.OtIsExistById, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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
            lblSuccess.Text = "Application Created Successfully . ARN Number is " + dt.Rows[0]["Application_RefNo"].ToString();
        }
    }


    public void GetApplicationDetails(int ApplicationId)
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
            btnSubmit.Text = "Update";

            if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) &&
                      ViewState["IsEdit"] != null && ViewState["IsEdit"].ToString() == "true")
            {
                btnSubmit.Text = "Update";
            }

            else if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && hdnRequestType.Value == RequestTypeID.Revision.ToString())  // Revise Domicile 
            {
                btnSubmit.Text = "Revise Domicile";
            }
            else if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && hdnRequestType.Value == RequestTypeID.Duplication.ToString())  // Duplicate Domicile 
            {
                btnSubmit.Text = "Duplicate Domicile";
                ViewState["DomicileApprovalDate"] = dt.Rows[0]["Domicile_ApprovedDate"].ToString();
            }
            else if ((hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()) && hdnRequestType.Value == RequestTypeID.Cancellation.ToString())  // Cancel Domicile 
            {
                btnSubmit.Text = "Cancel Domicile";

            }


            ddlTitle.SelectedValue = dt.Rows[0]["Title"].ToString();
            txtApplicantName.Text = dt.Rows[0]["Applicant_Name"].ToString();
            txtFatherName.Text = dt.Rows[0]["Father_Name"].ToString();
            txtCnic.Text = dt.Rows[0]["Applicant_Cnic"].ToString();

            DateTime DoB = Convert.ToDateTime(dt.Rows[0]["Date_of_Birth"].ToString()).Date;
            txtDOB.Text = DoB.ToString("yyyy-MM-dd");
            CalculateDate_TextChanged(null, null);


            txtPlaceofBirth.Text = dt.Rows[0]["Place_of_Birth"].ToString();
            txtResident.Text = dt.Rows[0]["Resident_of"].ToString();
            txtSurname.Text = dt.Rows[0]["Surname"].ToString();
            ddlPrimarySchool.SelectedValue = dt.Rows[0]["Primary_School"].ToString();
            txtTownVillagePrimary.Text = dt.Rows[0]["Town_PrimarySchool"].ToString();
            txtFromDatePrimary.Text = dt.Rows[0]["FromDate_PrimarySchool"].ToString();
            txtToDatePrimary.Text = dt.Rows[0]["ToDate_PrimarySchool"].ToString();
            ddlMiddleSchool.SelectedValue = dt.Rows[0]["Middle_School"].ToString();
            txtTownVillageMiddle.Text = dt.Rows[0]["Town_MiddleSchool"].ToString();
            txtFromDateMiddle.Text = dt.Rows[0]["FromDate_MiddleSchool"].ToString();
            txtToDateMiddle.Text = dt.Rows[0]["ToDate_MiddleSchool"].ToString();
            ddlHighSchool.SelectedValue = dt.Rows[0]["High_School"].ToString();
            txtTownVillageHigh.Text = dt.Rows[0]["Town_HighSchool"].ToString();
            txtFromDateHigh.Text = dt.Rows[0]["FromDate_HighSchool"].ToString();
            txtToDateHigh.Text = dt.Rows[0]["ToDate_HighSchool"].ToString();
            txtDistrictEducation.Text = dt.Rows[0]["District_Education"].ToString();
            //txtDateSubmission.Text = dt.Rows[0]["Date_Submission"].ToString();

            DateTime DateSubmission = Convert.ToDateTime(dt.Rows[0]["Date_Submission"].ToString()).Date;
            txtDateSubmission.Text = DateSubmission.ToString("yyyy-MM-dd");

            // txtIssueDate.Text = dt.Rows[0]["Date_Issue"].ToString();
            DateTime Date_Issue = Convert.ToDateTime(dt.Rows[0]["Date_Issue"].ToString()).Date;
            txtIssueDate.Text = Date_Issue.ToString("yyyy-MM-dd");

            txtParticularProperty.Text = dt.Rows[0]["ParticularsProperty"].ToString();
            txtLocation.Text = dt.Rows[0]["Location"].ToString();
            ddlTaluka.SelectedValue = dt.Rows[0]["TalukaId"].ToString();
            ddlTaluka_Changed(null, null);
            txtDistrict.Text = dt.Rows[0]["District"].ToString();
            txtSnoElectoralArea.Text = dt.Rows[0]["Electoral_Area"].ToString();
            txtAreaTaluka.Text = dt.Rows[0]["Electoral_Area_Taluka"].ToString();
            ddlDeh.SelectedValue = dt.Rows[0]["Electoreal_Area_Deh"].ToString();
            txtGuardiansCnic.Text = dt.Rows[0]["Guardian_NIC"].ToString();
            ddlGuardianRelationShip.SelectedValue = dt.Rows[0]["Guardian_RelationShip"].ToString();

            txtPhoneNo.Text = dt.Rows[0]["Applicant_PhoneNo"].ToString();
            txtTempAddress.Text = dt.Rows[0]["Temporary_Address"].ToString();
            txtPermanentAddress.Text = dt.Rows[0]["Permanent_Address"].ToString();

            if (dt.Rows[0]["GuardianDomicile_CertificateDate"].ToString() != "1/1/0001 12:00:00 AM")
            {
                DateTime GuardianDomicile_CertificateDate = Convert.ToDateTime(dt.Rows[0]["GuardianDomicile_CertificateDate"].ToString()).Date;
                txtGuardianDomicileDate.Text = GuardianDomicile_CertificateDate.ToString("yyyy-MM-dd");
            }

            ddlGuardian2.SelectedValue = dt.Rows[0]["Guardian_RelationShip2"].ToString();
            //  txtApplicantAge.Text = dt.Rows[0]["Applicant_Age"].ToString();
            txtTradeOccupation.Text = dt.Rows[0]["Trade_Occupation"].ToString();
            txtMarkIdentification.Text = dt.Rows[0]["Mark_of_Identification"].ToString();
            // txtDateofArrival.Text = dt.Rows[0]["Date_of_Arrival"].ToString();

            if (dt.Rows[0]["Date_of_Arrival"].ToString() != "1/1/0001 12:00:00 AM")
            {
                DateTime Date_of_Arrival = Convert.ToDateTime(dt.Rows[0]["Date_of_Arrival"].ToString()).Date;
                txtDateofArrival.Text = Date_of_Arrival.ToString("yyyy-MM-dd");
            }

            txtMukhtiarkar.Text = dt.Rows[0]["Mukhtiarkar_Taluka"].ToString();
            txtForeignAddress.Text = dt.Rows[0]["Address_ForeignCountry"].ToString();
            txtDistrictOfficerTaluka.Text = dt.Rows[0]["Deputy_District_Officer_Taluka"].ToString();
            txtWifeHusband.Text = dt.Rows[0]["Husband_Wife_Name"].ToString();
            //  RequiredFieldValidator18.Enabled = false;
            ViewState["ApplicantPhoto_Path"] = dt.Rows[0]["Applicant_Photo_Path"].ToString();
            Image1.ImageUrl = dt.Rows[0]["Applicant_Photo_Path"].ToString();
            lblMessage.Visible = false;
            divPic.Visible = true;
            rbdMaritialStatus.Items.FindByValue(dt.Rows[0]["Marital_Status"].ToString()).Selected = true;


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
                txtChildName.Text = dtChild.Rows[0]["Child_Name"].ToString();
                DateTime Child_DoB = Convert.ToDateTime(dtChild.Rows[0]["Child_DoB"].ToString()).Date;
                txtChildDob.Text = Child_DoB.ToString("yyyy-MM-dd");

                for (int i = 1; i < dtChild.Rows.Count; i++)
                {
                    btnAddMoreOption_Click(null, null);

                    TextBox txtChild = rptChild.Items[i - 1].FindControl("txtChild_New") as TextBox;
                    TextBox txtChildDob_New = rptChild.Items[i - 1].FindControl("txtChildDob_New") as TextBox;

                    txtChild.Text = dtChild.Rows[i]["Child_Name"].ToString();
                    DateTime Child_DoB_rpt = Convert.ToDateTime(dtChild.Rows[i]["Child_DoB"].ToString()).Date;
                    txtChildDob_New.Text = Child_DoB_rpt.ToString("yyyy-MM-dd");

                }


            }
            if (dt.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Duplication.ToString())
            {
                ViewState["IsDuplicate"] = "True";
            }

            if (dt.Rows[0]["RequestTypeId"].ToString() == RequestTypeID.Cancellation.ToString())
            {
                ViewState["IsCancel"] = "True";
            }

            ddlTitle.Focus();

            GetApplicationDomicileHistory();

            if (dt.Rows[0]["Is_ByBirth"].ToString() == "True")
            {
                ChkByBirth.Checked = true;
            }
            else
            {
                ChkByBirth.Checked = false;
            }
        }

    }

    public void GetApplicationDomicileHistory()
    {
        DataTable dt = new BAL_Application().usp_Application(OperationTypesID.GetDomicileHistory, Convert.ToInt32(hdnApplicationId.Value), null, null, null, null, null, null, null, null,
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


    public void DuplicateDocumentsRecord(int DocumentTypeID)
    {
        DataTable dt = new BAL_DuplicateDocumentsRecord().usp_Setup_DuplicateDocumentsRecord(OperationTypesID.Insert, DocumentTypeID, Convert.ToInt32(hdnApplicationId.Value), true, UserId, UserIP);
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

    public void GetDuplicateDocInfo()
    {
        string DocumentName = "";
        DataTable dt = new BAL_DuplicateDocumentsRecord().usp_Setup_DuplicateDocumentsRecord(OperationTypesID.Select, null, Convert.ToInt32(hdnApplicationId.Value), true, UserId, UserIP);
        if (dt != null && dt.Rows.Count > 0)
        {
            divDuplicateDocInfo.Visible = true;
            lblDuplicateDocInfo.Text = "Duplicate ";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lblDuplicateDocInfo.Text += dt.Rows[i]["DocumentName"].ToString() + " and ";
            }

            lblDuplicateDocInfo.Text = lblDuplicateDocInfo.Text.Trim();
            if (lblDuplicateDocInfo.Text.Contains(" "))
            {
                lblDuplicateDocInfo.Text = lblDuplicateDocInfo.Text.Remove(lblDuplicateDocInfo.Text.LastIndexOf(' ')).TrimEnd();
            }
            lblDuplicateDocInfo.Text += " has been issued to candidate";
        }

    }

}

