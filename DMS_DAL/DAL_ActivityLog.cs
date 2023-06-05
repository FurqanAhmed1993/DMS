using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_DAL
{
    public class DAL_ActivityLog : DAL
    {
        public DataTable usp_ActivityLog(string ActionPerformed , string PageName, int? UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActivityLog_Insert"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action_Performed", SqlDbType.VarChar).Value = ActionPerformed;
                    cmd.Parameters.Add("@PageName", SqlDbType.VarChar).Value = PageName;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_Taluka : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}
