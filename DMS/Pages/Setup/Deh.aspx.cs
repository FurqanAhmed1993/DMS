﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;

public partial class Pages_Setup_Deh : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetFeature();
            BindDropDown();
            BindRepeater();
            
        }
        PagingHandler();
    }


    public void BindDropDown()
    {
        DataTable dt_Taluka = new BAL_Taluka().usp_Setup_Taluka(OperationTypesID.Select, null, "", UserId, UserIP);
        CommonObjects.BindDropDown(ddlTaluka, dt_Taluka, "TalukaName", "TalukaId", true, false);
        CommonObjects.BindDropDown(ddlTAluka1, dt_Taluka, "TalukaName", "TalukaId", true, false);
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
            DataTable dt = GetDetail();
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
            Logger.WriteErrorLog("/Pages/Setup/Deh.aspx", "BindRepeater", ex.Message);
        }
    }
    public DataTable GetDetail()
    {
        DataTable dt = new DataTable();
        int Taluka = ddlTaluka.SelectedValue==null ? 0 : Convert.ToInt32(ddlTaluka.SelectedValue);
        string Deh = txtDeh.Text;
        dt = new BAL_Deh().usp_Setup_Deh(OperationTypesID.Select, null, Deh, Taluka, UserId, UserIP);
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
            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            string Value = txtDeh1.Text;
            //     string msg = new BAL_ComplaintCategory().ValidateControls(GenericConstants.OtIsExist, Value, UserId, UserIP);

            if (Id == 0)
            {
                DataTable dt = new BAL_Deh().usp_Setup_Deh(OperationTypesID.Insert, null, Value, Convert.ToInt32(ddlTAluka1.SelectedItem.Value), UserId, UserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {
                        Success(dt.Rows[0]["Message"].ToString());
                        BindRepeater();
                        ResetModalControlsOnAdd();

                    }
                }
            }
            else
            {
                DataTable dt = new BAL_Deh().usp_Setup_Deh(OperationTypesID.Update, Id, Value, Convert.ToInt32(ddlTAluka1.SelectedItem.Value), UserId, UserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HasError"].ToString() == "1")
                    {
                        Error(dt.Rows[0]["Message"].ToString());
                    }
                    else if (dt.Rows[0]["HasError"].ToString() == "0")
                    {
                        Success(dt.Rows[0]["Message"].ToString());
                        BindRepeater();
                        ResetModalControls();
                        ClosePopup();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/Deh.aspx", "btnAdd_Click", ex.Message);
        }
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ResetModalControls();
            ImageButton lbEdit = (ImageButton)sender;
            RepeaterItem rptItem = (RepeaterItem)lbEdit.NamingContainer;
            int _hfId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("_hfId")).Value);
            if (_hfId > 0)
            {
                DataTable dt = new BAL_Deh().usp_Setup_Deh(OperationTypesID.OtIsExistById, _hfId, null, null, null, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlTAluka1.SelectedValue = Convert.ToString(dt.Rows[0]["TalukaId"].ToString());
                    txtDeh1.Text = Convert.ToString(dt.Rows[0]["DehName"].ToString());
                    chk_IsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString() == "" ? "0" : dt.Rows[0]["IsActive"].ToString());
                    hfId.Value = _hfId.ToString();
                    OpenPopup();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/Deh.aspx", "lbEdit_Click", ex.Message);
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            hfmodalDeleteId.Value = "0";
            ImageButton lbEdit = (ImageButton)sender;
            RepeaterItem rptItem = (RepeaterItem)lbEdit.NamingContainer;
            int _hfId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("_hfId")).Value);
            if (_hfId > 0)
            {
                hfmodalDeleteId.Value = _hfId.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalDeleteModal()", "OpenModalDeleteModal();", true);



            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/Deh.aspx", "lbDelete_Click", ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = Convert.ToInt32(hfmodalDeleteId.Value == "" ? "0" : hfmodalDeleteId.Value);
            if (Id > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CloseDeleteModal()", "CloseDeleteModal();", true);
                //if (IsTransectionExist(Id) == true)
                //{
                    DataTable dt = new BAL_Deh().usp_Setup_Deh(OperationTypesID.Delete, Id, null, null ,UserId, UserIP);
                    if (dt != null && dt.Rows.Count > 0)
                    {
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
                //}
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Setup/Deh.aspx", "btnDelete_Click", ex.Message);
        }
    }

    private void ResetControl()
    {
        ddlTaluka.SelectedIndex = 0;
        txtDeh.Text = "";
        ResetModalControls();
    }
    private void ResetModalControlsOnAdd()
    {
        txtDeh1.Text = "";
        ddlTAluka1.SelectedIndex = 0;
        ddlTaluka.SelectedIndex = 0;
        txtDeh.Text = "";
        chk_IsActive.Checked = true;
        hfId.Value = "0";
    }
    private void ResetModalControls()
    {
        txtDeh1.Text = "";
        ddlTAluka1.SelectedIndex = 0;
        chk_IsActive.Checked = true;
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