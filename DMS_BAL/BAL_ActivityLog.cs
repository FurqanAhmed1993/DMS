using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_BAL
{
    public class BAL_ActivityLog : DMS_DAL.DAL_ActivityLog
    {
        DataTable dt = new DataTable();
        public DataTable usp_SetupActivityLog(string ActionPerformed, string PageName, int? UserId, string UserIP)
        {
            dt = usp_ActivityLog(ActionPerformed, PageName, UserId, UserIP);
            return dt;
        }
    }
}
