using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DMS_BAL
{
    public class BAL_Deh : DMS_DAL.DAL_Deh
    {
        DataTable dt = new DataTable();
        public DataTable usp_Setup_Deh(int OperationType, int? DehId, string DehName, int? TalukaId, int? UserId, string UserIP)
        {
            dt = usp_Setup_Crud_Deh(OperationType,  DehId, DehName ,TalukaId, UserId, UserIP);
            return dt;



           // dt = GetData(cmd);
        }

        public DataTable usp_IsTransection_Exist_Taluka(string OperationType, int Id)
        {
            dt = usp_SetupDeh_IsTransection_Exist(OperationType, Id);
            return dt;
        }
    }
}
