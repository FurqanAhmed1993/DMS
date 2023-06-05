using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS_DAL;

namespace DMS_BAL
{
    public class BAL_Dashboard : DMS_DAL.DAL_Dashboard
    {
        DataTable dt = new DataTable();
        public DataTable usp_DashboardDatas(int UserId)
        {
            dt = usp_DashboardData(UserId);
            return dt;
        }
    }
}
