using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_BAL
{
    public class BAL_Taluka : DMS_DAL.DAL_Taluka
    {
        DataTable dt = new DataTable();
        public DataTable usp_Setup_Taluka(int OperationType, int? TalukaId, string TalukaName, int? UserId, string UserIP)
        {
            dt = usp_Setup_Crud_Taluka(OperationType, TalukaId, TalukaName, UserId, UserIP);
            return dt;
        }

        public DataTable usp_IsTransection_Exist_Taluka(int OperationType, int Id)
        {
            dt = usp_SetupTaluka_IsTransection_Exist(OperationType, Id);
            return dt;
        }
    }
}
