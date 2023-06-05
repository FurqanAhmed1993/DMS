using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DMS_DAL
{
    public class DAL_User : DAL
    {
        public DataTable usp_setup_UserCrud(int OperationType, int? UserId, string Username, string PhoneNo, string EmailAddress, string Password, string LoginId,
                int? RoleId, int? LoginUserId, bool IsActive, string UserIP, string TalukaIds, int? CreatedBy, int? ModifiedBy)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_UserCrud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Username;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = PhoneNo;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = EmailAddress;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                    cmd.Parameters.Add("@LoginId", SqlDbType.VarChar).Value = LoginId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    cmd.Parameters.Add("@LoginUserId", SqlDbType.Int).Value = LoginUserId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;
                    cmd.Parameters.Add("@TalukaIds", SqlDbType.VarChar).Value = TalukaIds;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = CreatedBy;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = ModifiedBy;

                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_UserCrud : {0}", ex.Message), ex);
            }
            return dt;
        }

        public DataTable usp_Setup_User_IsExist(int OperationType, int? RoleId, string Username, string LoginId, int? UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_UserCrud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Username;                    
                    cmd.Parameters.Add("@LoginId", SqlDbType.VarChar).Value = LoginId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;

                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_ComplaintMaster_Crud : {0}", ex.Message), ex);
            }
            return dt;
        }        

        public DataTable usp_Setup_User_Taluka(int OperationType, int UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_UserCrud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;

                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_ComplaintMaster_Crud : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}
