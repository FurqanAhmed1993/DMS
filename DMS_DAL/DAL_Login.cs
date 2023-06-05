using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DMS_DAL
{
    public class DAL_Login : DAL
    {
        public DataTable usp_UserLogin_Get_UserByLoginId(string LoginId,string Password)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_UserLogin_Get_UserByLoginId"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LoginId", SqlDbType.VarChar).Value = LoginId;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_UserLogin_Crud : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_UserLoginHistory_Insert(int UserId, bool IsSuccess, string UserIp)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_UserLoginHistory_Insert"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@IsSuccess", SqlDbType.Bit).Value = IsSuccess;
                    cmd.Parameters.Add("@UserIp", SqlDbType.VarChar).Value = UserIp;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_UserLogin_Insert_UserLoginHistory : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_UserLogin_UpdatePassword(int UserId, string OldPassword, string NewPassword, string UserIp)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_UserLogin_UpdatePassword"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@OldPassword", SqlDbType.VarChar).Value = OldPassword;
                    cmd.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = NewPassword;
                    cmd.Parameters.Add("@UserIp", SqlDbType.VarChar).Value = UserIp;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during Usp_UserLogin_UpdatePassword : {0}", ex.Message), ex);
            }
            return dt;
        }
        
    }
}
