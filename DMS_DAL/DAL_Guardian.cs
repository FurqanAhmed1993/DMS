using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_DAL
{
    public  class DAL_Guardian : DAL
    {
        public DataTable usp_Setup_tblGuardian(int OperationType , int Id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_tblGuardian_CRUD"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationTypeId", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_Complaint_Category : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}
