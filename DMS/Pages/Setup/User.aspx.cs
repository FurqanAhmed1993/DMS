using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;

public partial class Pages_Setup_User : Base
{
    int? Nullint = null;
    double? Nulldouble = null;
    bool? Nullbool = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetFeature();
            BindDropDown();
            BindRepeater();
            BindRepeaterBranch();
            div_Status.Visible = false;
        }
        PagingHandler();

    }

    #region PAGING
    private void PagingHandler()
    {
        PagingAndSorting.ImgNext.Click += ImgNext_Click;
        PagingAndSorting.ImgPrevious.Click += ImgPrevious_Click;
        PagingAndSorting.DdlPage.SelectedIndexChanged += DdlPage_SelectedIndexChanged;
        PagingAndSorting.DdlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
    }

    void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void ImgNext_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    void ImgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    #endregion

    public void BindDropDown()
    {
        try
        {
            DataTable dt_Role = new BAL_Role().usp_GetUserRole(Nullint, null, true, RoleId, 1);
            CommonObjects.BindDropDown(ddlRole, dt_Role, "RoleName", "RoleId", true, false);

            DataTable dt_RoleListing = new BAL_Role().usp_GetUserRole(Nullint, null, true, Nullint, 2);
            CommonObjects.BindDropDown(ddlRoleSearch, dt_RoleListing, "RoleName", "RoleId", true, false);

            //DataTable dt_Taluka = new BAL_User().usp_Setup_User("GetTaluka", null, null, null, null, null, null, null, null, null, false, UserIP, null, null, null);
            //CommonObjects.BindDropDown(ddlTaluka, dt_Taluka, "TalukaName", "TalukaId", true, false);


        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/User.aspx", "BindDropDown", ex.ToString());
        }
    }
    public void BindRepeater()
    {
        try
        {
         //   btnExport.Visible = false;

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
            DataTable dt = GetUserLogin();
            if (dt != null && dt.Rows.Count > 0)
            {
              //  btnExport.Visible = true;
                var li = dt.Select().Skip(skip).Take(pageSize).CopyToDataTable();
                rpt.DataSource = li;
                rpt.DataBind();
                PagingAndSorting.setPagingOptions(dt.Rows.Count);
            }
            else
            {
                rpt.DataSource = null;
                rpt.DataBind();
                PagingAndSorting.setPagingOptions(0);
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/User.aspx.aspx", "BindRepeater", ex.Message);
        }
    }
    public void BindRepeaterBranch()
    {
        try
        {
            int pageSize = 100;
            int pageNumber = 1;
            if (PagingAndSorting1.DdlPageSize.SelectedValue.toInt() > 0)
            {
                pageSize = PagingAndSorting1.DdlPageSize.SelectedValue.toInt();
            }
            if (PagingAndSorting1.DdlPage.Items.Count > 0)
            {
                pageNumber = PagingAndSorting1.DdlPage.SelectedValue.toInt();
            }

            //int skip = pageNumber * pageSize - pageSize;
            //DataTable dt = new BAL_Branch().usp_Setup_Branch(GenericConstants.OtSelect, null, null, null, null, UserId, UserIP);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    var li = dt.Select().Skip(skip).Take(pageSize).CopyToDataTable();
            //    rpt_Branch.DataSource = li;
            //    rpt_Branch.DataBind();
            //    PagingAndSorting1.setPagingOptions(dt.Rows.Count);
            //}
            //else
            //{
            //    rpt_Branch.DataSource = null;
            //    rpt_Branch.DataBind();
            //    PagingAndSorting1.setPagingOptions(0);
            //}


            int skip = pageNumber * pageSize - pageSize;
            DataTable dt = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.Select, null, "", UserId, UserIP);
            if (dt != null && dt.Rows.Count > 0)
            {
                var li = dt.Select().Skip(skip).Take(pageSize).CopyToDataTable();
                rpt_Taluka.DataSource = li;
                rpt_Taluka.DataBind();
                PagingAndSorting1.setPagingOptions(dt.Rows.Count);
            }
            else
            {
                rpt_Taluka.DataSource = null;
                rpt_Taluka.DataBind();
                PagingAndSorting1.setPagingOptions(0);
            }


        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/User.aspx", "BindRepeater", ex.Message);
        }
    }
    public DataTable GetUserLogin()
    {
        DataTable dt = new DataTable();
        string UserId = txtUserSearch.Text.Trim() == "" ? null : txtUserSearch.Text.Trim();
        string PhoneNo = txtPhoneSearch.Text.Trim() == "" ? null : txtPhoneSearch.Text.Trim();
        string Email = txtEmailSearch.Text.Trim() == "" ? null : txtEmailSearch.Text.Trim();
        int? _RoleId = (ddlRoleSearch.SelectedValue == "" || ddlRoleSearch.SelectedValue == "0") ? Nullint : Convert.ToInt32(ddlRoleSearch.SelectedValue);
        dt = new BAL_User().usp_Setup_User(OperationTypesID.Select, null, UserId, PhoneNo, Email, null, null, _RoleId, null, false, UserIP, null, null, null);
        return dt;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControl();
        BindRepeater();
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        ResetModalControls();
        OpenPopup();
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {


        try
        {
            string UserName = txtUserName.Text.Trim();
            string EmailAddress = txtEmailAddress.Text.Trim();
            string LoginId = txtLoginId.Text.Trim();
            string PhoneNo = txtPhoneNo.Text.Trim();
            string TalukaIds = "";
            int RoleID = (ddlRole.SelectedValue == "" || ddlRole.SelectedValue == "0") ? 0 : Convert.ToInt32(ddlRole.SelectedValue);
            //  int TalukaId = (ddlTaluka.SelectedValue == "" || ddlTaluka.SelectedValue == "0") ? 0 : Convert.ToInt32(ddlTaluka.SelectedValue);

            #region SelectTaluka
            for (int i = 0; i < rpt_Taluka.Items.Count; i++)
            {
                CheckBox chkBranch = (CheckBox)rpt_Taluka.Items[i].FindControl("ChkTaluka");
                Label lblTalukaId = (Label)rpt_Taluka.Items[i].FindControl("lblTalukaId");
                if (chkBranch.Checked == true)
                {
                    int hfTalukaId = int.Parse(lblTalukaId.Text);
                    TalukaIds += hfTalukaId + ",";
                }
            }
            #endregion

            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string msg = new BAL_User().ValidateControls(OperationTypesID.OtIsExist, RoleID, UserName, LoginId, Id, UserIP);
            if (PhoneNo.Length == GenericConstants.PhoneNumberLength)
            {
                if (msg == "")
                {
                    if (Id == 0)
                    {
                        if (ddlRole.SelectedItem.Text == GenericConstants.RoleCreator && TalukaIds == "")
                        {
                            Error("Please Select Taluka");
                        }
                        else
                        {
                            DataTable dt = new BAL_User().usp_Setup_User(OperationTypesID.Insert, UserId, UserName, PhoneNo, EmailAddress, GenericConstants.Password, LoginId, RoleID, null, chk_IsActive.Checked, UserIP, TalukaIds, UserId, null);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["HasError"].ToString() == "1")
                                {
                                    Error(dt.Rows[0]["Message"].ToString());
                                }
                                else if (dt.Rows[0]["HasError"].ToString() == "0")
                                {
                                    Success(dt.Rows[0]["Message"].ToString());
                                    ResetModalControls();
                                    BindRepeater();
                                }
                            }
                        }
                    }

                    else
                    {
                        if (ddlRole.SelectedItem.Text == GenericConstants.RoleCreator && TalukaIds == "")
                        {
                            Error("Please Select Taluka");
                        }

                        else
                        {
                            DataTable dt = new BAL_User().usp_Setup_User(OperationTypesID.Update, Id, UserName, PhoneNo, EmailAddress, null, LoginId, RoleID, null, chk_IsActive.Checked, UserIP, TalukaIds, null, UserId);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["HasError"].ToString() == "1")
                                {
                                    Error(dt.Rows[0]["Message"].ToString());
                                }
                                else if (dt.Rows[0]["HasError"].ToString() == "0")
                                {
                                    Success(dt.Rows[0]["Message"].ToString());
                                    ResetModalControls();
                                    ClosePopup();
                                    BindRepeater();
                                }
                            }
                        }

                    }


                }
                else
                {
                    Error(msg);
                }
            }
            else
            {
                Error("Please Enter " + GenericConstants.PhoneNumberLength + " digits PhoneNumber");
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/User.aspx", "btnAdd_Click", ex.Message);
        }


    }




    protected void lbEdit_Click(object sender, EventArgs e)
    {


        try
        {
            ResetModalControls();
            ImageButton lbEdit = (ImageButton)sender;
            RepeaterItem rptItem = (RepeaterItem)lbEdit.NamingContainer;
            int hfUserId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfUserId")).Value);
            if (hfUserId > 0)
            {
                DataTable dt = new BAL_User().usp_Setup_User(OperationTypesID.Select, hfUserId, null, null, null, null, null, null, null, false, UserIP, null, null, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtUserName.Text = Convert.ToString(dt.Rows[0]["Name"].ToString());
                    txtEmailAddress.Text = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                    txtLoginId.Text = Convert.ToString(dt.Rows[0]["LoginId"].ToString());
                    chk_IsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString() == "" ? "0" : dt.Rows[0]["IsActive"].ToString());
                    ddlRole.SelectedValue = Convert.ToString(dt.Rows[0]["RoleId"].ToString() == "" ? "0" : dt.Rows[0]["RoleId"].ToString());
                    txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["MobileNo"].ToString());
                    hfId.Value = hfUserId.ToString();
                    DataTable dt_Taluka = new BAL_User().usp_Setup_UserTaluka(OperationTypesID.GetTaluka, hfUserId, UserIP);
                    if (dt_Taluka != null && dt_Taluka.Rows.Count > 0)
                    {
                        for (int i = 0; i < rpt_Taluka.Items.Count; i++)
                        {
                            CheckBox chkTaluka = (CheckBox)rpt_Taluka.Items[i].FindControl("ChkTaluka");
                            Label lblTalukaId = (Label)rpt_Taluka.Items[i].FindControl("lblTalukaId");
                            for (int k = 0; k < dt_Taluka.Rows.Count; k++)
                            {
                                if (lblTalukaId.Text == dt_Taluka.Rows[k]["TalukaId"].ToString())
                                {
                                    chkTaluka.Checked = true;
                                }
                            }
                        }
                    }
                    if (ddlRole.SelectedItem.Text == GenericConstants.RoleCreator)
                    {
                        div_Status.Visible = true;
                    }
                    else
                    {
                        div_Status.Visible = false;
                    }
                    OpenPopup();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/User.aspx", "lbEdit_Click", ex.Message);
        }


    }


    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            hfmodalDeleteId.Value = "0";
            ImageButton lbEdit = (ImageButton)sender;
            RepeaterItem rptItem = (RepeaterItem)lbEdit.NamingContainer;
            int hfUserId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfUserId")).Value);
            if (hfUserId > 0)
            {
                hfmodalDeleteId.Value = hfUserId.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalDeleteModal()", "OpenModalDeleteModal();", true);
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/User.aspx", "lbDelete_Click", ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = Convert.ToInt32(hfmodalDeleteId.Value == "" ? "0" : hfmodalDeleteId.Value);
            if (Id > 0)
            {
                DataTable dt = new BAL_User().usp_Setup_User(OperationTypesID.Delete, Id, null, null, null, null, null, null, null, false, UserIP, null, UserId, UserId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CloseDeleteModal()", "CloseDeleteModal();", true);
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {
                        Success(dt.Rows[0]["Message"].ToString());
                        BindRepeater();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/User.aspx", "btnDelete_Click", ex.Message);
        }
    }

    private void ResetControl()
    {
        txtUserSearch.Text = "";
        txtPhoneSearch.Text = "";
        txtEmailSearch.Text = "";
        ddlRoleSearch.SelectedValue = "0";
        ResetModalControls();
    }
    private void ResetModalControls()
    {
        ddlRole.SelectedValue = "0";
        //ddlTaluka.SelectedValue = "0";
        txtUserName.Text = txtEmailAddress.Text = txtLoginId.Text = txtPhoneNo.Text = "";
        chk_IsActive.Checked = true;
        hfId.Value = "0";
        for (int i = 0; i < rpt_Taluka.Items.Count; i++)
        {
            CheckBox ChkTaluka = (CheckBox)rpt_Taluka.Items[i].FindControl("ChkTaluka");
            ChkTaluka.Checked = false;
        }
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

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRole.SelectedItem.Text == GenericConstants.RoleCreator)
            {
                div_Status.Visible = true;
            }
            else
            {
                div_Status.Visible = false;
            }
        }
        catch (Exception ex) { }
    }
}