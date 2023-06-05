using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DMS_Utilities;

namespace DMS_BAL
{
    public class BAL_User : DMS_DAL.DAL_User
    {
        DataTable dt = new DataTable();
        public DataTable usp_Setup_User(int OperationType, int? UserId, string Username, string PhoneNo, string EmailAddress, string Password, string LoginId,
                int? RoleId, int? LoginUserId, bool IsActive, string UserIP, string TalukaIds, int? CreatedBy, int? ModifiedBy)
        {
            dt = usp_setup_UserCrud(OperationType, UserId, Username, PhoneNo, EmailAddress, Password, LoginId, RoleId, LoginUserId, IsActive, UserIP, TalukaIds, CreatedBy, ModifiedBy);
            return dt;
        }

        public DataTable usp_Setup_UserTaluka(int OperationType, int UserId, string UserIP)
        {
            dt = usp_Setup_User_Taluka(OperationType, UserId, UserIP);
            return dt;
        }

        public string ValidateControls(int OperationType, int? RoleId, string Username, string LoginId, int? UserId, string UserIP)
        {
            string msg = "";
            if (RoleId > 0)
            {
                if (Username != "")
                {
                    if (LoginId != "")
                    {
                        dt = usp_Setup_User_IsExist(OperationType, RoleId, Username, LoginId, UserId, UserIP);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            msg = "Already Exist";
                        }
                    }
                    else
                    {
                        msg = "Please enter Login Id";
                    }
                }
                else
                {
                    msg = "Please enter User Name";
                }
            }
            else
            {
                msg = "Please select Role";
            }


            return msg;
        }       
        public void ExportToExcel(string FileName, DataTable dt)
        {
            try
            {

                dt.Columns.Remove("DCMId");
                dt.Columns.Remove("UserId");
                dt.Columns.Remove("RoleId");
                dt.Columns.Remove("DistributorId");
                dt.Columns.Remove("IsActive");
                dt.Columns.Remove("UserIP");
                CommonObjects.ExportToExcel(FileName, dt);
            }
            catch
            {
            }
        }

    }
}
