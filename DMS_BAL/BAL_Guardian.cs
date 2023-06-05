using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DMS_BAL
{
    public class BAL_Guardian : DMS_DAL.DAL_Guardian
    {
        DataTable dt = new DataTable();
        public DataTable usp_tblGuardian(int OperationType , int Id)
        {
            dt = usp_Setup_tblGuardian(OperationType , Id);
            return dt;
        }
    }
}
