using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DMS_BAL
{
    public class BAL_Login : DMS_DAL.DAL_Login
    {
        DataTable dt = new DataTable();
        public DataTable Get_UserByLoginId(string LoginId,string Password=null)
        {
            dt = usp_UserLogin_Get_UserByLoginId(LoginId,Password);
            return dt;
        }
        public DataTable Insert_UserLoginHistory(int UserId, bool IsSuccess, string UserIp)
        {
            dt = usp_UserLoginHistory_Insert(UserId, IsSuccess, UserIp);
            return dt;
        }
        public DataTable UpdatePassword(int UserId, string OldPassword, string NewPassword, string UserIp)
        {
            dt = usp_UserLogin_UpdatePassword(UserId, OldPassword, NewPassword, UserIp);
            return dt;
        }
    }
}
