using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;
using System.Web.UI.HtmlControls;


public partial class Pages_Dashboard : Base
{
    string Page = "/Pages/Dashboard.aspx";
    int? Nullint = null;
    bool? Nullbool = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCountControls();
            Timer1.Enabled = true;
        }

        else
        {
            divError1.Visible = false;
            lblError1.Text = "";
            ViewState["FromDate"] = "";
            ViewState["ToDate"] = "";
        }
    }
    private void Exception(string Excep)
    {

    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
     //   BindCountControls(txtFromDate.Text, txtToDate.Text);
    }
    public void BindCountControls()
    {
        try
        {
            DataTable dt = new BAL_Dashboard().usp_DashboardDatas(UserId);
            if (dt != null && dt.Rows.Count > 0)
            {
                lbl_InitialDraft.Text = string.IsNullOrEmpty(dt.Rows[0]["Initial Draft"].ToString()) ? "0" : dt.Rows[0]["Initial Draft"].ToString();
                lbl_ApproverPending.Text = string.IsNullOrEmpty(dt.Rows[0]["Submitted to DDO"].ToString()) ? "0" : dt.Rows[0]["Submitted to DDO"].ToString();
                lblObjected.Text = string.IsNullOrEmpty(dt.Rows[0]["Objected"].ToString()) ? "0" : dt.Rows[0]["Objected"].ToString();
                lblRejected.Text = string.IsNullOrEmpty(dt.Rows[0]["Rejected"].ToString()) ? "0" : dt.Rows[0]["Rejected"].ToString();
                lbl_Approved.Text = string.IsNullOrEmpty(dt.Rows[0]["Approved"].ToString()) ? "0" : dt.Rows[0]["Approved"].ToString();
                lblIssued.Text = string.IsNullOrEmpty(dt.Rows[0]["Issued"].ToString()) ? "0" : dt.Rows[0]["Issued"].ToString();
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog(Page, "BindOrderStatusCount()", ex.ToString());
        }
    }
    bool ValidateDate()
    {
        bool Check = true;
        //try
        //{
        //    if ((txtFromDate.Text != "" && txtToDate.Text == "") || (txtFromDate.Text == "" && txtToDate.Text != "") || (txtFromDate.Text == "" && txtToDate.Text == ""))
        //    {
        //        divError1.Visible = true;
        //        lblError1.Text = "Please Select Both Date";
        //        Check = false;
        //    }
        //    else
        //    {
        //        divError1.Visible = false;
        //        Check = true;
        //    }
        //}
        //catch (Exception ex)
        //{
        //}
        return Check;
    }
    protected void lbNew_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Domicile/ViewApplicationSaved.aspx?ApplicationStatusId=" + StatusId.InitialDraft);
         
        }
        catch (Exception ex)
        { }

    }

    protected void lbApproverSubmitted_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Domicile/ViewApplicationSaved.aspx?ApplicationStatusId=" + StatusId.Submitted_to_DDO);

        }
        catch (Exception ex)
        { }
    }

    protected void lbApprove_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Domicile/ViewApplicationSaved.aspx?ApplicationStatusId=" + StatusId.Approved);

        }
        catch (Exception ex)
        { }
    }

    protected void lbObjected_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Domicile/ViewApplicationSaved.aspx?ApplicationStatusId=" + StatusId.Objected);


        }
        catch (Exception ex)
        { }
    }

    protected void lbRejected_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Domicile/ViewApplicationSaved.aspx?ApplicationStatusId=" + StatusId.Rejected);


        }
        catch (Exception ex)
        { }
    }

    protected void lbIssued_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("Domicile/ViewApplicationSaved.aspx?ApplicationStatusId=" + StatusId.Issued);


        }
        catch (Exception ex)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateDate())
            {
               // BindCountControls(txtFromDate.Text, txtToDate.Text);
                //Response.Redirect("Complaint/Complaint.aspx?FromDate=" + ViewState["FromDate"].ToString() + "&ToDate=" + ViewState["ToDate"].ToString() + "", false);
            }
        }
        catch (Exception ex)
        { }
    }
    void ResetControl()
    {
        //txtFromDate.Text = "";
        //txtToDate.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControl();
    }
}