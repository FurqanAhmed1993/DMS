using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DMS_DAL
{
    public class DAL_Setup_MasterDetail : DAL
    {
        public DataTable usp_Setup_MasterDetail_Get(int SetupMasterId, bool? IsActive, int? ParentId, string Name, int? SetupDetailId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MasterDetail_Get"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@SetupDetailId", SqlDbType.Int).Value = SetupDetailId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MasterDetail_Get : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_SetupMaster_Get(int SetupMasterId, bool? IsActive)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_SetupMaster_Get"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_SetupMaster_Get : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_SetupMasterDetail_Delete(int SetupDetailId, int UserId, string UserIp)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_SetupMasterDetail_Delete"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupDetailId", SqlDbType.Int).Value = SetupDetailId;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIp;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_SetupMasterDetail_Delete : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MasterDetail_IsExist(int SetupMasterId, int SetupDetailId, string Name, int? ParentId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MasterDetail_IsExist"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@SetupDetailId", SqlDbType.Int).Value = SetupDetailId;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MasterDetail_IsExist : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_SetupMasterDetail_Insert(int SetupMasterId, string SetupDetailName, int? ParentId, string Flex1, string Flex2, string Flex3, bool IsActive, int UserId, string UserIp, int? RegionId = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_SetupMasterDetail_Insert"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@SetupDetailName", SqlDbType.VarChar).Value = SetupDetailName;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    cmd.Parameters.Add("@Flex1", SqlDbType.VarChar).Value = Flex1;
                    cmd.Parameters.Add("@Flex2", SqlDbType.VarChar).Value = Flex2;
                    cmd.Parameters.Add("@Flex3", SqlDbType.VarChar).Value = Flex3;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIp;
                    cmd.Parameters.Add("@RegionId", SqlDbType.Int).Value = RegionId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_SetupMasterDetail_Insert : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_SetupMasterDetail_Update(int SetupDetailId, int SetupMasterId, string SetupDetailName, int? ParentId, string Flex1, string Flex2, string Flex3, bool IsActive, int UserId, string UserIp, int? RegionId = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_SetupMasterDetail_Update"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupDetailId", SqlDbType.Int).Value = SetupDetailId;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@SetupDetailName", SqlDbType.VarChar).Value = SetupDetailName;
                    cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = ParentId;
                    cmd.Parameters.Add("@Flex1", SqlDbType.VarChar).Value = Flex1;
                    cmd.Parameters.Add("@Flex2", SqlDbType.VarChar).Value = Flex2;
                    cmd.Parameters.Add("@Flex3", SqlDbType.VarChar).Value = Flex3;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIp;
                    cmd.Parameters.Add("@RegionId", SqlDbType.Int).Value = RegionId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_SetupMasterDetail_Update : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MasterDetail_Get_City(int SetupMasterId, bool? IsActive, int? ProvinceId, string Name, int? SetupDetailId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MasterDetail_Get_City"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@ProvinceId", SqlDbType.Int).Value = ProvinceId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@SetupDetailId", SqlDbType.Int).Value = SetupDetailId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MasterDetail_Get_City : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Setup_MasterDetail_Get_Area(int SetupMasterId, bool? IsActive, int? ProvinceId, int? CityId, string Name, int? SetupDetailId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_MasterDetail_Get_Area"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@ProvinceId", SqlDbType.Int).Value = ProvinceId;
                    cmd.Parameters.Add("@CityId", SqlDbType.Int).Value = CityId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@SetupDetailId", SqlDbType.Int).Value = SetupDetailId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_MasterDetail_Get_City : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_Follow_Up_Product_Remarks_Get(int SetupMasterId, bool? IsActive, string Name, int? SetupDetailId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Follow_Up_Product_Remarks_Get"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@SetupDetailId", SqlDbType.Int).Value = SetupDetailId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Follow_Up_Product_Remarks_Get : {0}", ex.Message), ex);
            }
            return dt;
        }
        public DataTable usp_SetupMasterDetail_IsTransection_Exist(int SetupMasterId, int Id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_SetupMasterDetail_IsTransection_Exist"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SetupMasterId", SqlDbType.Int).Value = SetupMasterId;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_SetupMasterDetail_IsTransection_Exist : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}
