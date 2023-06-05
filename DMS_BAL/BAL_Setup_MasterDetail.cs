using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DMS_BAL
{
    public class BAL_Setup_MasterDetail : DMS_DAL.DAL_Setup_MasterDetail
    {
        DataTable dt = new DataTable();
        public DataTable usp_Get_Setup_MasterDetail(int SetupMasterId, bool? IsActive, int? ParentId, string Name, int? SetupDetailId)
        {
            dt = usp_Setup_MasterDetail_Get(SetupMasterId, IsActive, ParentId, Name, SetupDetailId);
            return dt;
        }
        public DataTable usp_Get_SetupMaster(int SetupMasterId, bool? IsActive)
        {
            dt = usp_SetupMaster_Get(SetupMasterId, IsActive);
            return dt;
        }
        public DataTable usp_Delete_MasterDetail(int SetupDetailId, int UserId, string UserIp)
        {
            dt = usp_SetupMasterDetail_Delete(SetupDetailId, UserId, UserIp);
            return dt;
        }
        public DataTable usp_Insert_MasterDetail(int SetupMasterId, string SetupDetailName, int? ParentId, string Flex1, string Flex2, string Flex3, bool IsActive, int UserId, string UserIp, int? RegionId = null)
        {
            dt = usp_SetupMasterDetail_Insert(SetupMasterId, SetupDetailName, ParentId, Flex1, Flex2, Flex3, IsActive, UserId, UserIp,RegionId);
            return dt;
        }
        public DataTable usp_Update_MasterDetail(int SetupDetailId, int SetupMasterId, string SetupDetailName, int? ParentId, string Flex1, string Flex2, string Flex3, bool IsActive, int UserId, string UserIp,int? RegionId=null)
        {
            dt = usp_SetupMasterDetail_Update(SetupDetailId, SetupMasterId, SetupDetailName, ParentId, Flex1, Flex2, Flex3, IsActive, UserId, UserIp,RegionId);
            return dt;
        }
        public string ValidateControls(int SetupMasterId, int SetupDetailId, string Name, int? ParentId)
        {
            string msg = "";
            if (Name != "")
            {
                dt = usp_Setup_MasterDetail_IsExist(SetupMasterId, SetupDetailId, Name, ParentId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = "Already Exist";
                }
            }
            
            else
            {
                msg = "Please fill the required fields";
            }

            return msg;
        }
        public DataTable usp_Get_City(int SetupMasterId, bool? IsActive, int? ProvinceId, string Name, int? SetupDetailId)
        {
            dt = usp_Setup_MasterDetail_Get_City(SetupMasterId, IsActive, ProvinceId, Name, SetupDetailId);
            return dt;
        }
        public DataTable usp_Get_Area(int SetupMasterId, bool? IsActive, int? ProvinceId, int? CityId, string Name, int? SetupDetailId)
        {
            dt = usp_Setup_MasterDetail_Get_Area(SetupMasterId, IsActive, ProvinceId, CityId, Name, SetupDetailId);
            return dt;
        }
        public DataTable usp_Get_Follow_Up_Product_Remarks(int SetupMasterId, bool? IsActive, string Name, int? SetupDetailId)
        {
            dt = usp_Follow_Up_Product_Remarks_Get(SetupMasterId, IsActive, Name, SetupDetailId);
            return dt;
        }
        public DataTable usp_IsTransection_Exist_SetupMasterDetail(int SetupMasterId, int Id)
        {
            dt = usp_SetupMasterDetail_IsTransection_Exist(SetupMasterId, Id);
            return dt;
        }


    }
}
