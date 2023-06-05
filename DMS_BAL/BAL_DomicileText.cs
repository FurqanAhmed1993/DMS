using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DMS_BAL
{
    public class BAL_DomicileText : DMS_DAL.DAL_DomicileText    
    {
        DataTable dt = new DataTable();
        public DataTable usp_Setup_DomicileData(int OperationType, int? Id, int? FormTypeId, string HeaderArea, string MiddleArea, string LowerArea, int? UserId, string UserIP)
        {
            dt = usp_Setup_Crud_DomicileData(OperationType, Id, FormTypeId, HeaderArea, MiddleArea, LowerArea , UserId, UserIP);
            return dt;
        }
    }
}
