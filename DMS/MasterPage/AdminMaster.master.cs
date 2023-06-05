using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;
using System.Data;

public partial class MasterPage_AdminMaster : System.Web.UI.MasterPage
{
    Base objbase = new Base();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblYear.Text = Convert.ToString(DateTime.Now.Year);
            lblLoginStatus.Text = new Base().FullName;
            CheckFirstLogin();
        }
    }
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        // remove session before sending to login page.
        Session.Abandon();
        new Base().ExpireCookie();
        Response.Redirect("/Login.aspx", true);
    }
    private void CheckFirstLogin()
    {
        DataTable dt = new BAL_UserPasswordHistory().Get_UserPasswordHistory_ByLoginId(objbase.UserId);
        if (dt != null && dt.Rows.Count > 0)
        {
        }
        else
        {
            string pageName = System.IO.Path.GetFileNameWithoutExtension(Request.Path);
            pageName = pageName.ToLower();
            if (pageName != "changepassword")
            {
                Response.Redirect("/Pages/ChangePassword.aspx");
            }
        }
    }
}
