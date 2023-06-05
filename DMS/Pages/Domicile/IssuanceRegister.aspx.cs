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

public partial class Pages_Domicile_IssuanceRegister : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new BAL_Application().usp_Application(OperationTypesID.Select, null, txtARNNumber.Text, null, null, null, StatusId.Approved, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
            , Convert.ToDateTime("1900/01/01"), Convert.ToDateTime("1900/01/01"), null, null, Convert.ToDateTime("1900/01/01"), Convert.ToDateTime("1900/01/01"), UserId, UserIP, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
            , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtIssuanceDate.Text = DateTime.Now.ToString("ddd, dd MMM yyyy");
                txtApplicantName.Text = dt.Rows[0]["Applicant_Name"].ToString();
                txtFatherName.Text = dt.Rows[0]["Father_Name"].ToString();
                txtSubModule.Text = dt.Rows[0]["SubModule"].ToString();
                ViewState["ApplicationId"] = dt.Rows[0]["ApplicationId"].ToString();

                divAlert.Visible = false;
                lblErrorDiv.Text = "";
                divSuccess.Visible = false;
                lblSuccessDiv.Text = "";

            }
            else
            {
                ResetControls();
                divAlert.Visible = true;
                lblErrorDiv.Text = "No such Application Found/Approved in system";
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("IssuanceRegister.aspx", "btnSearch_Click", ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new BAL_IssuanceRegister().usp_Setup_IssuanceRegister(OperationTypesID.Insert, Convert.ToInt32(ViewState["ApplicationId"].ToString()), txtReceiverName.Text, txtReceiverAddress.Text, txtReceiverCnic.Text, Convert.ToDateTime(txtIssuanceDate.Text), UserId, UserIP);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HasError"].ToString() == "1")
                {
                    Error(dt.Rows[0]["Message"].ToString());
                }
                else if (dt.Rows[0]["HasError"].ToString() == "0")
                {
                    DataTable dtStatus = new BAL_Application().usp_Application(OperationTypesID.UpdateApplicationStatus, Convert.ToInt32(ViewState["ApplicationId"].ToString()), null, null, null, null, StatusId.Issued, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, UserId, UserIP, null
                       , null, null, null, null, null, null, null, null, null, null, null, null, null, DateTime.Now, null, null
                       , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                    if (dtStatus != null && dtStatus.Rows.Count > 0)
                    {
                        if (dtStatus.Rows[0]["HasError"].ToString() == "0")
                        {
                            Success(dt.Rows[0]["Message"].ToString());
                            ResetControls();
                        }
                    }
                 
                }
            }

        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("IssuanceRegister.aspx", "btnSubmit_Click", ex.Message);
        }
    }


    private void ResetControls()
    {

        txtIssuanceDate.Text = "";
        txtApplicantName.Text = "";
        txtFatherName.Text = "";
        txtSubModule.Text = "";
        txtARNNumber.Text = "";
        txtReceiverName.Text = "";
        txtReceiverCnic.Text = "";
        txtReceiverAddress.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtARNNumber.Text = "";

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

    private void SetFeature()
    {
        try
        {
            btnSubmit.Visible = false;
            string url = HttpContext.Current.Request.Url.PathAndQuery;
            //string[] Array = url.Split('?');
            //url = Array[0];
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
                        //   IsEdit.Value = "1";
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["FeatureId"].ToString()) == (int)Feature.Delete)
                    {
                        //  IsDelete.Value = "1";
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["FeatureId"].ToString()) == (int)Feature.View)
                    {
                        //IsView.Value = "1";
                    }
                }
            }
        }
        catch (Exception ex) { }
    }

}