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
using System.Web.UI.HtmlControls;

public partial class Pages_Domicile_ViewApplicationSaved : Base
{
    int ApplicationId = 0;
    string ApplicationType = "";
    string RequestType = "";
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


            BindDropDown();
            SetFeature();
            if (Request.QueryString["ApplicationId"] != null)
            {
                if (Request.QueryString["ApplicationId"].ToString() != "")
                {
                    hdnApplicationId.Value = Request.QueryString["ApplicationId"].ToString();
                    GetARNNumber(Convert.ToInt32(hdnApplicationId.Value));
                }
            }

            if (Request.QueryString["ApplicationTypeId"] != null)
            {
                if (Request.QueryString["ApplicationTypeId"].ToString() != "")
                {

                    hdnApplicationType.Value = Request.QueryString["ApplicationTypeId"].ToString();

                }
            }

            if (Request.QueryString["RequestTypeId"] != null)
            {
                if (Request.QueryString["RequestTypeId"].ToString() != "")
                {
                    hdnRequestType.Value = Request.QueryString["RequestTypeId"].ToString();

                    if (hdnRequestType.Value == RequestTypeID.Revision.ToString() || hdnRequestType.Value == RequestTypeID.Duplication.ToString() || hdnRequestType.Value == RequestTypeID.Cancellation.ToString())
                    {
                        Btn_Add.Visible = false;
                        ddlStatus.Enabled = false;
                        // ddlStatus.SelectedValue = StatusId.Approved.ToString();
                    }

                    if (hdnRequestType.Value == RequestTypeID.Revision.ToString())
                        lblReqType.Text = "(For Revision Purpose Only)";

                    else if (hdnRequestType.Value == RequestTypeID.Duplication.ToString())
                        lblReqType.Text = "(For Duplication Purpose Only)";

                    else if (hdnRequestType.Value == RequestTypeID.Cancellation.ToString())
                        lblReqType.Text = "(For Cancellation Purpose Only)";

                }
            }

            if (Request.QueryString["ApplicationStatusId"] != null)
            {
                if (Request.QueryString["ApplicationStatusId"].ToString() != "")
                {
                    ddlStatus.SelectedIndex = Convert.ToInt32(Request.QueryString["ApplicationStatusId"].ToString());
                    ddlStatus.Enabled = false;
                    btnSearch_Click(null, null);

                    string status = Request.QueryString["ApplicationStatusId"].ToString();

                    if (status == StatusId.InitialDraft.ToString())
                        lblStatus.Text = "INITIAL DRAFT";

                    else if (status == StatusId.Submitted_to_DDO.ToString())
                        lblStatus.Text = "PENDING AT APPROVER";

                    else if (status == StatusId.Approved.ToString())
                        lblStatus.Text = "APPROVED";

                    else if (status == StatusId.Objected.ToString())
                        lblStatus.Text = "OBJECTED";

                    else if (status == StatusId.Rejected.ToString())
                        lblStatus.Text = "REJECTED";

                    else if (status == StatusId.Issued.ToString())
                        lblStatus.Text = "ISSUED";
                }
            }

            if (Request.QueryString["ApplicationId"] == null && Request.QueryString["ApplicationTypeId"] == null && Request.QueryString["RequestTypeId"] == null)
            {
                Btn_Add.Visible = false;
            }

            if (Request.QueryString["RequestTypeId"] == null)
            {
                Btn_Add.Visible = false;
            }

        }

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
            if (Session["lblSuccess"] != null && Session["lblSuccess"].ToString() != "")
            {
                divSuccess.Visible = true;
                lblSuccessDiv.Text = Session["lblSuccess"].ToString() + " ARN Number is " + dt.Rows[0]["Application_RefNo"].ToString();
                Session.Remove("lblSuccess");
            }
        }
    }


    protected void lbFormView_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton lnkView = (ImageButton)sender;
            RepeaterItem rpt = (RepeaterItem)lnkView.NamingContainer;
            HiddenField hdnAppId = (HiddenField)(rpt.FindControl("hdnAppId"));
            HiddenField hdnApplicationTypeId = (HiddenField)(rpt.FindControl("hdnApplicationTypeId"));
            HiddenField hdnRequestTypeId = (HiddenField)(rpt.FindControl("hdnRequestTypeId"));
            HiddenField hdnApplicationStatusId = (HiddenField)(rpt.FindControl("hdnApplicationStatusId"));

            if (hdnRequestType.Value == RequestTypeID.Revision.ToString())    // Revise Case
            {

                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else if (hdnRequestType.Value == RequestTypeID.New_Request.ToString()) // Major and Minor Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else if (hdnRequestType.Value == RequestTypeID.Duplication.ToString()) // Duplicate Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else if (hdnRequestType.Value == RequestTypeID.Cancellation.ToString()) // Cancellation Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else    // New Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/ViewAplicationSaved.aspx", "lbFormView_Click", ex.Message);
        }

    }

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ImageButton lbEdit = (ImageButton)e.Item.FindControl("lbEdit");
                ImageButton lnkViewForm = (ImageButton)e.Item.FindControl("lnkViewForm");
                ImageButton lbDelete = (ImageButton)e.Item.FindControl("lbDelete");
                ImageButton lnkView = (ImageButton)e.Item.FindControl("lnkView");
                HiddenField hdnStatusReperter = (HiddenField)e.Item.FindControl("hdnStatusReperter");
                HiddenField hdnApplicationStatusId = (HiddenField)e.Item.FindControl("hdnApplicationStatusId");
                LinkButton lbEnable = (LinkButton)e.Item.FindControl("lbEnable");

                //  lbDelete.Visible = false;

                if ((hdnStatusReperter.Value == "Initial Draft" || hdnStatusReperter.Value == "Objected"))
                {
                    if (IsEdit.Value == "1")
                    {
                        lbEdit.Visible = true;
                    }

                }
                else
                {
                    lbEdit.Visible = false;

                    if (RoleId == UserRole.SuperAdmin && (hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()))
                    {
                        if (IsEdit.Value == "1")
                        {
                            lbEdit.Visible = true;

                        }
                    }

                }

                if ((hdnRequestType.Value == RequestTypeID.Duplication.ToString() || hdnRequestType.Value == RequestTypeID.Cancellation.ToString()
                    || hdnRequestType.Value == RequestTypeID.Revision.ToString()) && (hdnApplicationStatusId.Value == StatusId.Approved.ToString() || hdnApplicationStatusId.Value == StatusId.Issued.ToString()))
                {
                    lnkViewForm.Visible = true;
                }
                else
                    lnkViewForm.Visible = false;


                if (IsEdit.Value == "0")
                {
                    lbEdit.Visible = false;
                    lnkViewForm.Visible = false;
                    lnkView.Visible = false;
                }

                if (RoleId == UserRole.SuperAdmin)
                {
                    if (IsEdit.Value == "1")
                    {
                        lbDelete.Visible = true;

                        if (hdnApplicationStatusId.Value == StatusId.Disable.ToString())
                        {
                            lbEnable.Visible = true;
                        }
                        else
                            lbEnable.Visible = false;
                    }
                }
                else
                {
                    lbEnable.Visible = false;
                    lbDelete.Visible = false;
                }

            }

        }

        catch (Exception ex)
        {
        }

    }


    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton lbEdit = (ImageButton)sender;
            RepeaterItem rpt = (RepeaterItem)lbEdit.NamingContainer;
            HiddenField hdnAppId = (HiddenField)(rpt.FindControl("hdnAppId"));
            HiddenField hdnApplicationTypeId = (HiddenField)(rpt.FindControl("hdnApplicationTypeId"));
            HiddenField hdnRequestTypeId = (HiddenField)(rpt.FindControl("hdnRequestTypeId"));
            HiddenField hdnApplicationStatusId = (HiddenField)(rpt.FindControl("hdnApplicationStatusId"));
            Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) +
                "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestTypeId.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsEdit=True");


        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/ViewAplicationSaved.aspx", "lbEdit_Click", ex.Message);
        }

    }

    public void btnCancel_Click(object sender, EventArgs e)
    {
        txtARNNumber.Text = "";
        txtNicNumber.Text = "";
        txtDomicileNumber.Text = "";
        ddlTaluka.SelectedIndex = 0;
        txtFromIssuanceDate.Text = "";
        txtToIssuanceDate.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtFromCancellationDate.Text = "";
        txtToCancellationDate.Text = "";

        if (hdnRequestType.Value != RequestTypeID.Duplication.ToString() && hdnRequestType.Value != RequestTypeID.Revision.ToString()
            && hdnRequestType.Value != RequestTypeID.Cancellation.ToString())
            ddlStatus.SelectedIndex = 0;
    }

    public void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    public void Btn_Add_Click(object sender, EventArgs e)
    {

        Response.Redirect("/Pages/Domicile/DomicileApplication_Major.aspx?ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationType.Value)) + "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)));
    }


    public void BindRepeater()
    {
        try
        {
            int pageSize = 100;
            int pageNumber = 1;
            if (PagingAndSorting.DdlPageSize.SelectedValue.toInt() > 0)
            {
                pageSize = PagingAndSorting.DdlPageSize.SelectedValue.toInt();
            }
            if (PagingAndSorting.DdlPage.Items.Count > 0)
            {
                pageNumber = PagingAndSorting.DdlPage.SelectedValue.toInt();
            }

            int skip = pageNumber * pageSize - pageSize;
            DataTable dt = GetDetail();
            if (dt != null && dt.Rows.Count > 0)
            {
                //  Btn_Add.Visible = false;
                var li = dt.Select().Skip(skip).Take(pageSize).CopyToDataTable();
                rpt.DataSource = li;
                rpt.DataBind();
                PagingAndSorting.setPagingOptions(dt.Rows.Count);
                divAlert.Visible = false;
                lblErrorDiv.Text = "";
                //    divSuccess.Visible = false;
                //lblSuccessDiv.Text = "";
            }
            else
            {

                rpt.DataSource = null;
                rpt.DataBind();
                PagingAndSorting.setPagingOptions(0);
                divAlert.Visible = true;
                lblErrorDiv.Text = "No Record Found";
                divSuccess.Visible = false;
                lblSuccessDiv.Text = "";
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Domicile/ViewApplicationSaved.aspx", "btnReceipt_Click", ex.Message);
        }
    }

    public void BindDropDown()
    {
        DataTable dt_Taluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.Select, null, "", UserId, UserIP);
        CommonObjects.BindDropDown(ddlTaluka, dt_Taluka, "TalukaName", "TalukaId", true, false);
    }

    public DataTable GetDetail()
    {
        DataTable dt = new DataTable();
        string ARNNumber = txtARNNumber.Text.Trim() == "" ? null : txtARNNumber.Text.Trim();
        string DomicileNumber = txtDomicileNumber.Text.Trim() == "" ? null : txtDomicileNumber.Text.Trim();
        string NIC_Number = txtNicNumber.Text.Trim() == "" ? null : txtNicNumber.Text.Trim();
        DateTime FromDate = txtFromDate.Text.Trim() == "" ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(txtFromDate.Text.Trim());
        DateTime ToDate = txtToDate.Text.Trim() == "" ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(txtToDate.Text.Trim());
        DateTime FromCancellationDate = txtFromCancellationDate.Text.Trim() == "" ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(txtFromCancellationDate.Text.Trim());
        DateTime ToCancellationDate = txtToCancellationDate.Text.Trim() == "" ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(txtToCancellationDate.Text.Trim());
        int StatusId = Convert.ToInt32(ddlStatus.SelectedItem.Value);
        int TalukaId = Convert.ToInt32(ddlTaluka.SelectedItem.Value);
        int? RequestTypeId = null;
        if (hdnRequestType.Value != "")
        {
            RequestTypeId = Convert.ToInt32(hdnRequestType.Value);
        }

        dt = new BAL_Application().usp_Application(OperationTypesID.Select, null, ARNNumber, DomicileNumber, null, RequestTypeId, StatusId, null, null, null, NIC_Number, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, TalukaId, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
            , FromDate, ToDate, null, null, FromCancellationDate, ToCancellationDate, UserId, UserIP, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
            , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        return dt;
    }

    private void SetFeature()
    {
        try
        {
            Btn_Add.Visible = false;
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
                        Btn_Add.Visible = true;
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

    protected void lnkView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            /*

            ImageButton lnkView = (ImageButton)sender;
            RepeaterItem rpt = (RepeaterItem)lnkView.NamingContainer;
            HiddenField hdnAppId = (HiddenField)(rpt.FindControl("hdnAppId"));
            HiddenField hdnApplicationTypeId = (HiddenField)(rpt.FindControl("hdnApplicationTypeId"));
            HiddenField hdnRequestTypeId = (HiddenField)(rpt.FindControl("hdnRequestTypeId"));
            HiddenField hdnApplicationStatusId = (HiddenField)(rpt.FindControl("hdnApplicationStatusId"));

            if (hdnRequestType.Value == RequestTypeID.Revision.ToString())    // Revise Case
            {

                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else if (hdnRequestType.Value == RequestTypeID.New_Request.ToString()) // Major and Minor Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else if (hdnRequestType.Value == RequestTypeID.Duplication.ToString()) // Duplicate Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else if (hdnRequestType.Value == RequestTypeID.Cancellation.ToString()) // Cancellation Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestType.Value)) + "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }
            else    // New Case
            {
                Response.Redirect("DomicileApplication_Major.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&ApplicationTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationTypeId.Value)) +
                    "&ApplicationStatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)) + "&IsView=True");
            }


            */
            ImageButton lnkView = (ImageButton)sender;
            RepeaterItem rpt = (RepeaterItem)lnkView.NamingContainer;
            HiddenField hdnAppId = (HiddenField)(rpt.FindControl("hdnAppId"));
            HiddenField hdnRequestTypeId = (HiddenField)(rpt.FindControl("hdnRequestTypeId"));
            HiddenField hdnApplicationStatusId = (HiddenField)(rpt.FindControl("hdnApplicationStatusId"));
            Response.Redirect("ViewApplicationDetails.aspx?ApplicationId=" + Server.UrlEncode(Encryption.Encrypt(hdnAppId.Value)) + "&RequestTypeId=" + Server.UrlEncode(Encryption.Encrypt(hdnRequestTypeId.Value)) +
               "&StatusId=" + Server.UrlEncode(Encryption.Encrypt(hdnApplicationStatusId.Value)));

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/ViewAplicationSaved.aspx", "lbEdit_Click", ex.Message);
        }
    }


    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton lnkView = (ImageButton)sender;
            RepeaterItem rpt = (RepeaterItem)lnkView.NamingContainer;
            HiddenField hdnAppId = (HiddenField)(rpt.FindControl("hdnAppId"));
            if (Convert.ToInt32(hdnAppId.Value) > 0)
            {
                txtDeleteComments.Text = "";
                ViewState["DeleteApplicationID"] = hdnAppId.Value;
                OpenDeletePopup();
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/ViewAplicationSaved.aspx", "lbDelete_Click", ex.Message);
        }
    }

    protected void lbEnable_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbEnable = (LinkButton)sender;
            RepeaterItem rpt = (RepeaterItem)lbEnable.NamingContainer;
            HiddenField hdnAppId = (HiddenField)(rpt.FindControl("hdnAppId"));
            if (Convert.ToInt32(hdnAppId.Value) > 0)
            {
                DataTable dtStatus = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, Convert.ToInt32(hdnAppId.Value), null, null, null, null, StatusId.Issued, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, UserId, UserIP, null
                                      , null, null, null, null, null, null, null, null, null, null, null, null, null, DateTime.Now, null, null
                                      , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    if (dtStatus.Rows[0]["HasError"].ToString() == "0")
                    {
                        Success("Application Enabled Successfully");
                        BindRepeater();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/ViewAplicationSaved.aspx", "lbEnable_Click", ex.Message);
        }
    }
    public void CloseDeletePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "CloseDeletePopup()", "CloseDeletePopup();", true);
    }
    public void OpenDeletePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenDeletePopup()", "OpenDeletePopup();", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {


            DataTable dt = new BAL_Application().usp_Application(OperationTypesID.UpdateApplication_DeleteComments, Convert.ToInt32(ViewState["DeleteApplicationID"].ToString()), null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    UserId, UserIP, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, txtDeleteComments.Text);


            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HasError"].ToString() == "1")
                {

                }
                else if (dt.Rows[0]["HasError"].ToString() == "0")
                {
                    Success("Deleted Successfully");
                    BindRepeater();
                    CloseDeletePopup();
                }
            }

        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/ViewAplicationSaved.aspx", "btnAdd_Click", ex.Message);
        }
    }

}