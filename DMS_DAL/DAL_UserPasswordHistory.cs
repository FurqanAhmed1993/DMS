using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DMS_DAL
{
    public class DAL_UserPasswordHistory : DAL
    {
        public DataTable usp_UserPasswordHistory_GetByUserId(int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_UserPasswordHistory_GetByUserId"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_UserPasswordHistory_GetByUserId : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}
