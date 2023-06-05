using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DMS_DAL
{
    public class DAL_Setup_MenuItem : DAL
    {
        public DataTable usp_Setup_MenuItem_GetParentMenuByRoleId(int RoleId, bool? Is_Displayed_In_Menu)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MenuItem_GetParentMenuByRoleId"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    cmd.Parameters.Add("@Is_Displayed_In_Menu", SqlDbType.Bit).Value = Is_Displayed_In_Menu;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MenuItem_GetParentMenuByRoleId : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MenuItem_Get_ChildMenuByRoleId_ParentId(int RoleId, int ParentId, bool? Is_Displayed_In_Menu)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MenuItem_Get_ChildMenuByRoleId_ParentId"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    cmd.Parameters.Add("@Is_Displayed_In_Menu", SqlDbType.Bit).Value = Is_Displayed_In_Menu;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MenuItem_Get_ChildMenuByRoleId_ParentId : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_MenuItemFeatureMapping_Get(int? MenuItemFeatureId, int? MenuId, int? FeatureId, int? RoleId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_MenuItemFeatureMapping_Get"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MenuItemFeatureId", SqlDbType.Int).Value = MenuItemFeatureId;
                    cmd.Parameters.Add("@MenuId", SqlDbType.Int).Value = MenuId;
                    cmd.Parameters.Add("@FeatureId", SqlDbType.Int).Value = FeatureId;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_MenuItemFeatureMapping_Get : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MenuItem_CheckMenuAccess(int RoleId, string Url)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MenuItem_CheckMenuAccess"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    cmd.Parameters.Add("@Menu_URL", SqlDbType.VarChar).Value = Url;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MenuItem_CheckMenuAccess : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_RoleAccess_RoleMenuItemFeatureMapping_Insert(int RoleId, int MenuId, string MenuItemFeatureId, bool IsInsert, int UserId, string Userip)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_RoleAccess_RoleMenuItemFeatureMapping_Insert"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
                    cmd.Parameters.Add("@MenuId", SqlDbType.Int).Value = MenuId;
                    cmd.Parameters.Add("@MenuItemFeatureId", SqlDbType.VarChar).Value = MenuItemFeatureId;
                    cmd.Parameters.Add("@IsInsert", SqlDbType.Bit).Value = IsInsert;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = Userip;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_RoleAccess_RoleMenuItemFeatureMapping_Insert : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable Usp_MenuItem_Get(int? MenuId, string Menu_Name, int? ParentId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MenuItem_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Get";
                    cmd.Parameters.Add("@MenuId", SqlDbType.Int).Value = MenuId;
                    cmd.Parameters.Add("@Menu_Name", SqlDbType.VarChar).Value = Menu_Name;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MenuItem_Crud-Get : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MenuItem_Delete(int MenuId, int UserId, string UserIp)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MenuItem_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Delete";
                    cmd.Parameters.Add("@MenuId", SqlDbType.Int).Value = MenuId;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIp;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MenuItem_Crud-Delete : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MenuItem_Insert(string MenuName, string MenuURL, int? ParentId, bool IsDisplayed, int SortOrder, int UserId, string UserIP, bool IsActive, string FeatureId, string Parenticon)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MenuItem_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Insert";
                    cmd.Parameters.Add("@Menu_Name", SqlDbType.VarChar).Value = MenuName;
                    cmd.Parameters.Add("@MenuURL", SqlDbType.VarChar).Value = MenuURL;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    cmd.Parameters.Add("@IsDisplayed", SqlDbType.Bit).Value = IsDisplayed;
                    cmd.Parameters.Add("@SortOrder", SqlDbType.Int).Value = SortOrder;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@FeatureId", SqlDbType.VarChar).Value = FeatureId;
                    cmd.Parameters.Add("@Parenticon", SqlDbType.VarChar).Value = Parenticon;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MenuItem_Crud-Insert : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MenuItem_Update(int MenuId, string MenuName, string MenuURL, int? ParentId, bool IsDisplayed, int SortOrder, int UserId, string UserIP, bool IsActive, string FeatureId, string InsertedFeatureIds, string DeletedFeatureIds, string Parenticon)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MenuItem_Crud"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Update";
                    cmd.Parameters.Add("@MenuId", SqlDbType.Int).Value = MenuId;
                    cmd.Parameters.Add("@Menu_Name", SqlDbType.VarChar).Value = MenuName;
                    cmd.Parameters.Add("@MenuURL", SqlDbType.VarChar).Value = MenuURL;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    cmd.Parameters.Add("@IsDisplayed", SqlDbType.Bit).Value = IsDisplayed;
                    cmd.Parameters.Add("@SortOrder", SqlDbType.Int).Value = SortOrder;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@FeatureId", SqlDbType.VarChar).Value = FeatureId;
                    cmd.Parameters.Add("@InsertedFeatureIds", SqlDbType.VarChar).Value = InsertedFeatureIds;
                    cmd.Parameters.Add("@DeletedFeatureIds", SqlDbType.VarChar).Value = DeletedFeatureIds;
                    cmd.Parameters.Add("@Parenticon", SqlDbType.VarChar).Value = Parenticon;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MenuItem_Crud-Update : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}
