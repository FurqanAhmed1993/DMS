using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_BAL
{
    public class  BAL_DeputyCommissioner : DMS_DAL.DAL_DeputyCommissioner
    {
        DataTable dt = new DataTable();
        public DataTable usp_Setup_DeputyCommissioner(int OperationType, int? CommisionerId, string CommisionerName, DateTime? FromDate , DateTime? ToDate, DateTime? DomicileApprovalDate, int? UserId, string UserIP)
        {
            dt = usp_Setup_Crud_DeputyCommisioner(OperationType, CommisionerId, CommisionerName, FromDate, ToDate, DomicileApprovalDate,UserId, UserIP);
            return dt;
        }

    }
}
