using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;

public partial class CustomControls_Menu : System.Web.UI.UserControl
{
    bool? Nullbool = null;
    Base obj = new Base();
    public System.Text.StringBuilder menu = new System.Text.StringBuilder();
    protected void Page_Init(object sender, EventArgs e)
    {
        if (obj.RoleId > 0 && obj.UserId > 0)
        {
            AuthenticateUser();
        }
        else
        {
            Response.Redirect("/Login.aspx", true);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MakeMenuHTML();
        }
        catch(Exception ex)
        {
            Response.Redirect("/Pages/NetworkError.aspx");
        }
    }
    private void AuthenticateUser()
    {
        if (CheckPageAccess() == false)
        {
            Response.Redirect("/Pages/Unauthorize.aspx");
        }
    }
    private bool CheckPageAccess()
    {
        string url = "";
        try
        {
            string pageName = System.IO.Path.GetFileNameWithoutExtension(Request.Path).ToLower();
            if (pageName == "changepassword")
            {
                return true;
            }
            else
            {
                int roleId = new Base().RoleId;
                url = HttpContext.Current.Request.Url.PathAndQuery;
                string[] Array = url.Split('?');
                url = Array[0];
                DataTable dt = new BAL_Setup_MenuItem().usp_CheckMenuAccess(roleId, url);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("/Pages/NetworkError.aspx");
            return false;
        }

    }
    private void MakeMenuHTML()
    {
        int roleId = new Base().RoleId;
        DataTable menuTop = new BAL_Setup_MenuItem().usp_GetParentMenuByRoleId(roleId, true);
        for (int i = 0; i < menuTop.Rows.Count; i++)
        {
            menu.Append(@" <li>");
            if (menuTop.Rows[i]["HasAccess"].ToString() == "1")
            {
                string Menu_Name = menuTop.Rows[i]["Menu_Name"].ToString();
                string url = menuTop.Rows[i]["Menu_URL"].ToString() == string.Empty ? "#" : menuTop.Rows[i]["Menu_URL"].ToString();
                string iconclass = menuTop.Rows[i]["IconClass"].ToString() == string.Empty ? "fa fa-th-large" : menuTop.Rows[i]["IconClass"].ToString();
                int ParentID = string.IsNullOrEmpty(menuTop.Rows[i]["MenuId"].ToString()) ? 0 : Convert.ToInt16(menuTop.Rows[i]["MenuId"]);
                menu.Append(@" <a href='" + url + "' data-toggle='collapse' data-target='#child_" + i + "'><div class='pull-left'><i class='mr-20 " + iconclass + "'></i><span class='right-nav-text'>" + Menu_Name + "</span></div><div class='clearfix'></div></a>");
                if (ParentID != 0)
                {
                    DataTable menuChild = new BAL_Setup_MenuItem().usp_Get_ChildMenuByRoleId_ParentId(roleId, ParentID, true);
                    if (menuChild != null && menuChild.Rows.Count > 0)
                    {
                        MakeSubMenuHTML(menuChild, i);
                    }
                }
                menu.Append(@"</li>");
            }
        }
    }
    private void MakeSubMenuHTML(DataTable menuChild, int i)
    {
        menu.Append(@"<ul id='child_" + i + "' class='collapse collapse-level-1'>");
        for (int j = 0; j < menuChild.Rows.Count; j++)
        {
            if (menuChild.Rows[j]["HasAccess"].ToString() == "1")
            {
                string url = menuChild.Rows[j]["Menu_URL"].ToString() == string.Empty ? "#" : menuChild.Rows[j]["Menu_URL"].ToString();
                string Menu_Name = menuChild.Rows[j]["Menu_Name"].ToString();
                menu.Append(@"<li><a href='" + url + "'>" + Menu_Name + "</a></li>");
            }
        }
        menu.Append(@"</ul>  ");
    }
}