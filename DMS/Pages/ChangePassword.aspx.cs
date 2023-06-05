using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS_BAL;
using DMS_Utilities;

public partial class Pages_ChangePassword : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        DataTable dt = new BAL_Login().UpdatePassword(UserId, txtCurrentPassword.Text.Trim(), txtNewPassword.Text.Trim(), UserIP);
        if (dt != null && dt.Rows.Count > 0)
        {
            string Message = Convert.ToString(dt.Rows[0]["Msg"]);
            if (Message.Contains("Error"))
            {
                Error(Message);
            }
            else if (Message.Contains("Success"))
            {
                Success(Message);
            }
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

}