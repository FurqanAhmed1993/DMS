using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DMS_DAL
{
    public class DAL_Role : DAL
    {
        public DataTable usp_UserRole_Get(int? RoleId, string RoletName, bool? IsActive, int? LoginRoleId, int Operation)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Usp_UserRole_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    cmd.Parameters.Add("@RoleName", SqlDbType.VarChar).Value = RoletName;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@LoginRoleId", SqlDbType.Int).Value = LoginRoleId; 
                    cmd.Parameters.Add("@Operation", SqlDbType.Int).Value = Operation;

                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_UserRole_Get : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable Usp_UserRole_Insert(string RoleName, bool IsActive, int UserId, string UserIp)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Usp_UserRole_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@RoleName", SqlDbType.VarChar).Value = RoleName;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIp;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during Usp_UserRole_Insert : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_UserRole_Update(int RoleID, string RoleName, bool IsActive, int UserId, string UserIp)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Usp_UserRole_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = 3;
                    cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                    cmd.Parameters.Add("@RoleName", SqlDbType.VarChar).Value = RoleName;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIp;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Product_Update : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_UserRole_Delete(int RoleID, int UserId, string UserIp)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Usp_UserRole_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = 4;
                    cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIp;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_UserRole_Delete : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_User_Role(string LoginId,string Password)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_UserLogin_Get_UserByLoginId"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LoginId", SqlDbType.VarChar).Value = LoginId;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = Password;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_UserLogin_Get_UserByLoginId : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}
