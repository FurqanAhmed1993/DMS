using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;

public partial class Pages_DomicileFormText : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["FormTypeId"] != null)
            {
                if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "1")
                {
                    lblHeading.Text = "Setup-Domicile Verification-Single";
                }

                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "2")
                {
                    lblHeading.Text = "Setup-Domicile Verification-Multiple";
                }

                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "3")
                {
                    lblHeading.Text = "Setup-Submission Letter-Single";
                }

                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "4")
                {
                    lblHeading.Text = "Setup-Submission Letter-Multiple";
                }

                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "5")
                {
                    lblHeading.Text = "Setup-Domicile Application";
                }

                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "6")
                {
                    lblHeading.Text = "Setup-Form D";
                }

                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "7")
                {
                    lblHeading.Text = "Setup-Form C";
                }
                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "8")
                {
                    lblHeading.Text = "Setup-Domicile Format";
                }
                else if (Request.QueryString["FormTypeId"].ToString() != "" && Request.QueryString["FormTypeId"].ToString() == "9")
                {
                    lblHeading.Text = "Setup-Domicile Format";
                }
            }
            GetDetail();
            SetFeature();
        }

        

    }



    public void GetDetail()
    {
        try
        {
            DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.Select, null, Convert.ToInt32(Request.QueryString["FormTypeId"].ToString()), null, null, null, null, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    txtHeaderArea.Content = dt.Rows[0]["Header_Area"].ToString();
                    txtMiddleArea.Content= dt.Rows[0]["Middle_Area"].ToString();
                    txtLowerArea.Content = dt.Rows[0]["Lower_Area"].ToString();
                }

            }


        }

        catch (Exception ex)
        {
        }

    }

    protected void ddlHeader_SelectedChanged(object sender, EventArgs e)
    {
        if (ddlHeaderArea.SelectedIndex > 0)
        {
           // txtHeaderArea.Attributes.Add("onfocus", "this.select()");
            txtheaderToken.Text = ddlHeaderArea.SelectedItem.Value;
            //txtLowerArea.Text = txtLowerArea.Text.Insert(txtLowerArea.SelectionStart, "Hello world");
        }
    }

    protected void ddlMiddle_SelectedChanged(object sender, EventArgs e)
    {
        if (ddlMiddleArea.SelectedIndex > 0)
        {
            txtMiddleToken.Text = ddlMiddleArea.SelectedItem.Value;
        }
    }

    protected void ddlLower_SelectedChanged(object sender, EventArgs e)
    {
        if (ddlLowerArea.SelectedIndex > 0)
        {
            txtLowerToken.Text = ddlLowerArea.SelectedItem.Value;
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.OtIsExistById, null, Convert.ToInt32(Request.QueryString["FormTypeId"].ToString()), null, null, null, null, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string value1 = txtHeaderArea.Content;
                    string value2 = txtMiddleArea.Content;
                    string value3 = txtLowerArea.Content;

                    DataTable dtDomicile = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.Update, null, Convert.ToInt32(Request.QueryString["FormTypeId"].ToString()), value1, value2, value3, UserId, UserIP);
                    if (dtDomicile.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dtDomicile.Rows[0]["Message"].ToString());
                    }
                    else if (dtDomicile.Rows[0]["HasError"].ToString() == "0")
                    {
                        Success(dtDomicile.Rows[0]["Message"].ToString());
                    }

                }
                else
                {
                   string value1 = txtHeaderArea.Content;
                    string value2 = txtMiddleArea.Content;
                    string value3 = txtLowerArea.Content;

                    DataTable dtDomicile2 = new BAL_DomicileText().usp_Setup_DomicileData(OperationTypesID.Insert, null, Convert.ToInt32(Request.QueryString["FormTypeId"].ToString()), value1, value2, value3, UserId, UserIP);
                    if (dtDomicile2.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dtDomicile2.Rows[0]["Message"].ToString());
                    }
                    else if (dtDomicile2.Rows[0]["HasError"].ToString() == "0")
                    {
                        Success(dtDomicile2.Rows[0]["Message"].ToString());
                    }
                }
            }

        }

        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/DomicileFormText.aspx", "btnAdd_Click", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       txtHeaderArea.Content = "";
        txtLowerArea.Content= "";
        txtMiddleArea.Content= "";
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {

        OpenPopup();
    }


    public bool IsTransectionExist(int Id)
    {
        bool status = false;
        DataTable dt = new BAL_Taluka().usp_IsTransection_Exist_Taluka(OperationTypesID.OtIsExistById, Id);
        if (dt != null && dt.Rows.Count > 0)
        {
            status = true;
        }
        else
        {
            Error(Id + "Doesn't Exist");
        }
        return status;
    }


    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }
    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
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
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ImageButton lbEdit = (ImageButton)e.Item.FindControl("lbEdit");
                ImageButton lbDelete = (ImageButton)e.Item.FindControl("lbDelete");
                if (IsEdit.Value == "1")
                {
                    lbEdit.Visible = true;
                }
                else
                {
                    lbEdit.Visible = false;
                }
                if (IsDelete.Value == "1")
                {
                    lbDelete.Visible = true;
                }
                else
                {
                    lbDelete.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void SetFeature()
    {
        try
        {
            btnUpdate.Visible = false;
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
                        btnUpdate.Visible = true;
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