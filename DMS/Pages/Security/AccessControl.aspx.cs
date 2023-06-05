using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;

public partial class Pages_Security_AccessControl : Base
{
    int? Nullint = null;
    bool? Nullbool = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetFeature();
            BindDropDown();
        }
    }
    public void BindDropDown()
    {
        try
        {
            DataTable dt_Role = new BAL_Role().usp_GetUserRole(Nullint, null, Nullbool, RoleId,1);
            CommonObjects.BindDropDown(ddlRole, dt_Role, "RoleName", "RoleId", true, false);
            ddlRole_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Security/AccessControl.aspx", "BindDropDown()", ex.Message);
        }

    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TreeView1.Nodes.Clear();
            Load_tree();
            btnUpdate.Visible = true;
            if (ddlRole.SelectedValue == "0" || ddlRole.SelectedValue == "")
            {
                btnUpdate.Visible = false;
            }
            else
            {
                if (IsEdit.Value == "1")
                {
                    btnUpdate.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Security/AccessControl.aspx", "ddlRole_SelectedIndexChanged", ex.Message);
        }
    }
    public void Load_tree()
    {
        try
        {
            int _RolId = Convert.ToInt32(ddlRole.SelectedValue == "" ? "0" : ddlRole.SelectedValue);
            DataTable dt = new BAL_Setup_MenuItem().usp_GetParentMenuByRoleId(_RolId, null);
            TreeView1.Nodes.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(Convert.ToString(dr["Parent_Id"])) || (int)dr["Parent_Id"] == 0)
                {
                    TreeNode tnParent = new TreeNode();
                    tnParent.Value = dr["MenuId"].ToString();
                    tnParent.Text = dr["Menu_Name"].ToString();
                    tnParent.Checked = Convert.ToBoolean(dr["HasAccess"]);
                    int value = Convert.ToInt32(dr["MenuId"]);
                    tnParent.Expand();
                    TreeView1.Nodes.Add(tnParent);
                    Load_Child_Tree(tnParent, _RolId, value);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Security/AccessControl.aspx", "Load_tree()", ex.Message);
        }
    }
    public void Load_Child_Tree(TreeNode parent, int RoleId, int Id)
    {
        try
        {
            DataTable dt = new BAL_Setup_MenuItem().usp_Get_ChildMenuByRoleId_ParentId(RoleId, Id, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode child = new TreeNode();
                    child.Value = dr["MenuId"].ToString().Trim();
                    child.Text = dr["Menu_Name"].ToString().Trim();
                    child.Checked = Convert.ToBoolean(dr["HasAccess"]);
                    int temp = Convert.ToInt32(dr["MenuId"]);
                    child.Expand();
                    parent.ChildNodes.Add(child);
                    Load_Child_Tree(child, RoleId, temp);
                }
            }
            else
            {
                DataTable dtMenuItemFeature = new BAL_Setup_MenuItem().usp_Get_MenuItemFeatureMapping(Nullint, Id, Nullint, RoleId);
                foreach (DataRow drMenuItemFeature in dtMenuItemFeature.Rows)
                {
                    int _MenuItemFeatureId = Convert.ToInt32(drMenuItemFeature["MenuItemFeatureId"]);
                    TreeNode ChildMenuItemFeature_Tn = new TreeNode();
                    ChildMenuItemFeature_Tn.Text = drMenuItemFeature["Feature"].ToString() + "..";
                    ChildMenuItemFeature_Tn.Value = _MenuItemFeatureId.ToString();
                    ChildMenuItemFeature_Tn.Checked = Convert.ToBoolean(drMenuItemFeature["HasAccess"]);
                    parent.ChildNodes.Add(ChildMenuItemFeature_Tn);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Security/AccessControl.aspx", "Load_Child_Tree()", ex.Message);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int _RolId = Convert.ToInt32(ddlRole.SelectedValue == "" ? "0" : ddlRole.SelectedValue);
            if (_RolId > 0)
            {
                foreach (TreeNode tn_Parent in TreeView1.Nodes)
                {
                    string MenuItemFeatureId = "";
                    bool IsChecked = tn_Parent.Checked;
                    int _Id = Convert.ToInt32(tn_Parent.Value);
                    if (tn_Parent.ChildNodes.Count >= 1)
                    {
                        bool IsFeature = (tn_Parent.ChildNodes[0].Text.Contains(".."));
                        if (IsFeature == true)
                        {
                            if (IsChecked == true)
                            {
                                foreach (TreeNode tn_Child1 in tn_Parent.ChildNodes)
                                {
                                    if (tn_Child1.Checked == true)
                                    {
                                        int _MenuItemFeatureId = Convert.ToInt32(tn_Child1.Value);
                                        MenuItemFeatureId += _MenuItemFeatureId.ToString() + ',';
                                    }
                                }
                            }
                            new BAL_Setup_MenuItem().usp_Insert_RoleAccess_RoleMenuItemFeatureMapping_(_RolId, _Id, MenuItemFeatureId, IsChecked, UserId, UserIP);
                        }
                        else
                        {
                            new BAL_Setup_MenuItem().usp_Insert_RoleAccess_RoleMenuItemFeatureMapping_(_RolId, _Id, MenuItemFeatureId, IsChecked, UserId, UserIP);
                            foreach (TreeNode tn_Child1 in tn_Parent.ChildNodes)
                            {
                                MenuItemFeatureId = "";
                                IsChecked = tn_Child1.Checked;
                                _Id = Convert.ToInt32(tn_Child1.Value);
                                if (tn_Child1.ChildNodes.Count >= 1)
                                {
                                    if (IsChecked == true)
                                    {
                                        foreach (TreeNode tn_Child2 in tn_Child1.ChildNodes)
                                        {
                                            if (tn_Child2.Checked == true)
                                            {
                                                int _MenuItemFeatureId = Convert.ToInt32(tn_Child2.Value);
                                                MenuItemFeatureId += _MenuItemFeatureId.ToString() + ',';
                                            }
                                        }
                                    }
                                    new BAL_Setup_MenuItem().usp_Insert_RoleAccess_RoleMenuItemFeatureMapping_(_RolId, _Id, MenuItemFeatureId, IsChecked, UserId, UserIP);
                                }
                                else
                                {
                                    new BAL_Setup_MenuItem().usp_Insert_RoleAccess_RoleMenuItemFeatureMapping_(_RolId, _Id, MenuItemFeatureId, IsChecked, UserId, UserIP);
                                }
                            }
                        }
                    }
                    else
                    {
                        new BAL_Setup_MenuItem().usp_Insert_RoleAccess_RoleMenuItemFeatureMapping_(_RolId, _Id, MenuItemFeatureId, IsChecked, UserId, UserIP);
                    }
                }
                Success("Updated");
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("/Pages/Security/AccessControl.aspx", "btnUpdate_Click", ex.Message);
        }
    }
    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
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
                    if (Convert.ToInt32(dt.Rows[i]["FeatureId"].ToString()) == (int)Feature.Update)
                    {
                        IsEdit.Value = "1";
                    }
                }
            }
        }
        catch (Exception ex) { }
    }



}