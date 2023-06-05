using System;
using System.Data;
using System.Web.UI;
using DMS_BAL;
using DMS_Utilities;

public partial class Login : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblYear.Text = Convert.ToString(DateTime.Now.Year);
            //if (UserId > 0 && RoleId > 0)
            //{
            //    string Redirect = GenericConstants.GetDefaultPage;
            //    Response.Redirect(Redirect);
            //}
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        bool IsAuthenticate = false;
        try
        {
            string LoginId = txtUserName.Text.Trim();
            if (LoginId != "")
            {
                string Password = txtPassword.Text.Trim();
                if (Password != "")
                {
                    DataTable dt = new BAL_Login().Get_UserByLoginId(LoginId);
                    if (dt.Rows.Count > 0)
                    {
                        int _UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        if (_UserId > 0)
                        {
                            if (dt.Rows[0]["Password"].ToString() == Password)
                            {
                                if (Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString() == "" ? "0" : dt.Rows[0]["IsActive"].ToString()) == true)
                                {
                                    int _RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"].ToString());
                                    //int _DistributorId = Convert.ToInt32(Convert.ToString(dt.Rows[0]["DistributorId"]) == "" ? "0" : dt.Rows[0]["DistributorId"]);
                                    //int _EducatorId = Convert.ToInt32(Convert.ToString(dt.Rows[0]["DCMId"]) == "" ? "0" : dt.Rows[0]["DCMId"]);
                                    Base baseClass = new Base();
                                    baseClass.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                                    baseClass.FullName = Convert.ToString(dt.Rows[0]["Name"]);
                                    baseClass.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"]);
                                    baseClass.LoginId = Convert.ToString(dt.Rows[0]["LoginId"]);
                                    baseClass.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                                    //baseClass.DistributerId = _DistributorId;
                                    baseClass._EducatorId = _EducatorId;
                                    baseClass.IsSuperAdmin = _RoleId == UserRole.SuperAdmin ? true : false;
                                    //baseClass.IsAdmin = _RoleId == UserRole.Admin ? true : false;
                                    //baseClass.IsSubAdmin = _RoleId == UserRole.Admin ? true : false;
                                    //baseClass.IsSupervisor = _RoleId == UserRole.Supervisor ? true : false;
                                    baseClass.IsAgent = _RoleId == UserRole.Agent ? true : false;
                                    baseClass.IsDistributer = _RoleId == UserRole.Distributer ? true : false;
                                    baseClass.IsCommunicationManager = _RoleId == UserRole.CommunicationManager ? true : false;
                                    baseClass.IsSupplyChain = _RoleId == UserRole.SupplyChain ? true : false;
                                    baseClass.IsDCM = _RoleId == UserRole.DCM ? true : false;
                                    string Redirect = GenericConstants.GetDefaultPage;
                                    IsAuthenticate = true;
                                    InsertUserLoginHistory(_UserId, true);
                                    Response.Redirect(Redirect,false);
                                }
                                else
                                {
                                    lblValidation.Text = "In-Active User";
                                    InsertUserLoginHistory(_UserId, false);
                                }
                            }
                            else
                            {
                                lblValidation.Text = "Invalid Password";
                                InsertUserLoginHistory(_UserId, false);
                            }
                        }
                    }
                    else
                    {
                        lblValidation.Text = "Invalid Login Id";
                    }
                }
                else
                {
                    lblValidation.Text = "Please enter the Password";
                }
            }
            else
            {
                lblValidation.Text = "Please enter the Login Id";
            }
        }
        catch (Exception ex)
        {
            if (IsAuthenticate == false)
            {
                Logger.WriteErrorLog("Login.aspx", "btnLogin_Click", ex.Message);
            }
        }
    }
    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            string LoginId = txtLoginId.Text.Trim();
            if (LoginId != "")
            {
                DataTable dt = new BAL_Login().Get_UserByLoginId(LoginId);
                if (dt.Rows.Count > 0)
                {
                    int _UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    if (_UserId > 0)
                    {
                        string CurrentPassword = dt.Rows[0]["Password"].ToString();
                        string NewPassword = CommonObjects.CreateRandomPassword(10);
                        DataTable dt1 = new BAL_Login().UpdatePassword(_UserId, CurrentPassword.Trim(), NewPassword.Trim(), UserIP);
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            string Message = Convert.ToString(dt1.Rows[0]["Msg"]);
                            if (Message.Contains("Error"))
                            {
                                Error(Message);
                            }
                            else if (Message.Contains("Success"))
                            {
                                string msg = "";
                                msg += "Dear " + dt.Rows[0]["Name"].ToString() + ",<br/><br/>";
                                msg += "Your password is : <b>" + NewPassword.Trim() + "</b>";
                                msg += "<br/><br/>Regards,<br/>Support Team";
                                string EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
                                if (EmailAddress != "")
                                {
                                    Email.SendMail(EmailAddress, "Forgot Password", msg, "", "", "");
                                    ClosePopup();
                                    Success("Password has been sent to email address : " + EmailAddress);
                                }
                                else
                                {
                                    Error("Your password cannot be emailed because your email address is not exists, kindly contact to Administrator.");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Error("Invalid Login Id");
                }
            }
            else
            {
                Error("Please enter the Login Id");
            }
        }
        catch (Exception ex)
        {
            Logger.WriteErrorLog("Login.aspx", "btnForgetPassword_Click", ex.Message);

        }
    }
    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
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
    private void InsertUserLoginHistory(int UserId, bool IsSuccess)
    {
        new BAL_Login().Insert_UserLoginHistory(UserId, IsSuccess, UserIP);
    }

   
}