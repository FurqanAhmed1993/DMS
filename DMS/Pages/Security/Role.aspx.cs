using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;


public partial class Pages_Security_Role : Base
{
    int? Nullint = null;
    bool? Nullbool = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetFeature();

            BindRepeater();
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

            string Name = txtSearch.Text.Trim() == "" ? null : txtSearch.Text.Trim();
            DataTable dt = new BAL_Role().usp_GetUserRole(Nullint, Name, Nullbool,RoleId,1);
            if (dt != null && dt.Rows.Count > 0)
            {
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
            Logger.WriteErrorLog("/Pages/Setup/Role.aspx", "BindRepeater", ex.Message);
        }

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
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            if (Id == 0)
            {
                DataTable dt = new BAL_Role().usp_Insert_UserRole(txtRole.Text.Trim(), chk_IsActive.Checked, UserId, UserIP);
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
            else
            {
                DataTable dt = new BAL_Role().usp_Update_UserRole(Id, txtRole.Text.Trim(), chk_IsActive.Checked, UserId, UserIP);
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
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/Role.aspx", "btnAdd_Click", ex.Message);
        }
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {

        try
        {
            ResetModalControls();
            ImageButton lbEdit = (ImageButton)sender;
            RepeaterItem rptItem = (RepeaterItem)lbEdit.NamingContainer;
            int hfRoleId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRoleId")).Value);
            if (hfRoleId > 0)
            {
                bool hfIsactive = bool.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfIsactive")).Value);
                string hfRoleName = ((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRoleName")).Value;
                chk_IsActive.Checked = hfIsactive;
                txtRole.Text = hfRoleName.Trim();
                hfId.Value = hfRoleId.ToString();
                OpenPopup();

            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/Role.aspx", "lbEdit_Click", ex.Message);
        }

    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ResetModalControls();
            ImageButton lbEdit = (ImageButton)sender;
            RepeaterItem rptItem = (RepeaterItem)lbEdit.NamingContainer;
            int hfRoleId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRoleId")).Value);
            if (hfRoleId > 0)
            {
                hfmodalDeleteId.Value = hfRoleId.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalDeleteModal()", "OpenModalDeleteModal();", true);


            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/Role.aspx", "lbDelete_Click", ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = Convert.ToInt32(hfmodalDeleteId.Value == "" ? "0" : hfmodalDeleteId.Value);
            if (Id > 0)
            {
                DataTable dt = new BAL_Role().usp_Delete_UserRole(Id, UserId, UserIP);
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
            Logger.WriteErrorLog("/Pages/Setup/Role.aspx", "btnDelete_Click", ex.Message);
        }
    }
    private void ResetControl()
    {
        txtSearch.Text = "";
        ResetModalControls();
    }
    private void ResetModalControls()
    {
        chk_IsActive.Checked = true;
        txtRole.Text = "";
        hfId.Value = "0";
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

}