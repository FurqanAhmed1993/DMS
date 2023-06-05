using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DMS_BAL
{
    public class BAL_Setup_MenuItem : DMS_DAL.DAL_Setup_MenuItem
    {
        DataTable dt = new DataTable();
        public DataTable usp_GetParentMenuByRoleId(int RoleId, bool? Is_Displayed_In_Menu)
        {
            dt = usp_Setup_MenuItem_GetParentMenuByRoleId(RoleId, Is_Displayed_In_Menu);
            return dt;
        }
        public DataTable usp_Get_ChildMenuByRoleId_ParentId(int RoleId, int ParentId, bool? Is_Displayed_In_Menu)
        {
            dt = usp_Setup_MenuItem_Get_ChildMenuByRoleId_ParentId(RoleId, ParentId, Is_Displayed_In_Menu);
            return dt;
        }
        public DataTable usp_Get_MenuItemFeatureMapping(int? MenuItemFeatureId, int? MenuId, int? FeatureId, int? RoleId)
        {
            dt = usp_MenuItemFeatureMapping_Get(MenuItemFeatureId, MenuId, FeatureId, RoleId);
            return dt;
        }
        public DataTable usp_CheckMenuAccess(int RoleId, string Url)
        {
            dt = usp_Setup_MenuItem_CheckMenuAccess(RoleId, Url);
            return dt;
        }
        public DataTable Usp_Get_MenuItem(int? MenuId, string Menu_Name, int? ParentId)
        {
            dt = Usp_MenuItem_Get(MenuId, Menu_Name, ParentId);
            return dt;
        }
        public DataTable usp_Insert_RoleAccess_RoleMenuItemFeatureMapping_(int RoleId, int MenuId, string MenuItemFeatureId, bool IsInsert, int UserId, string Userip)
        {
            dt = usp_RoleAccess_RoleMenuItemFeatureMapping_Insert(RoleId, MenuId, MenuItemFeatureId, IsInsert, UserId, Userip);
            return dt;
        }
        public DataTable usp_Delete_Setup_MenuItem(int MenuId, int UserId, string UserIp)
        {
            dt = usp_Setup_MenuItem_Delete(MenuId, UserId, UserIp);
            return dt;
        }
        public DataTable usp_Insert_Setup_MenuItem(string MenuName, string MenuURL, int? ParentId, bool IsDisplayed, int SortOrder, int UserId, string UserIP, bool IsActive, string FeatureId, string Parenticon)
        {
            dt = usp_Setup_MenuItem_Insert(MenuName, MenuURL, ParentId, IsDisplayed, SortOrder, UserId, UserIP, IsActive, FeatureId, Parenticon);
            return dt;
        }
        public DataTable usp_Update_Setup_MenuItem(int MenuId, string MenuName, string MenuURL, int? ParentId, bool IsDisplayed, int SortOrder, int UserId, string UserIP, bool IsActive, string FeatureId, string InsertedFeatureIds, string DeletedFeatureIds, string Parenticon)
        {
            dt = usp_Setup_MenuItem_Update(MenuId, MenuName, MenuURL, ParentId, IsDisplayed, SortOrder, UserId, UserIP, IsActive, FeatureId, InsertedFeatureIds, DeletedFeatureIds, Parenticon);
            return dt;
        }
    }
}
