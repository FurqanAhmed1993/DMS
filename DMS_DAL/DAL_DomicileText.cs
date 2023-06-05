using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_DAL
{
    public class DAL_DomicileText : DAL
    {
        public DataTable usp_Setup_Crud_DomicileData(int OperationType, int? Id, int? FormTypeId, string HeaderArea, string MiddleArea, string LowerArea, int? UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_DomicileData"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    cmd.Parameters.Add("@FormTypeId", SqlDbType.Int).Value = FormTypeId;
                    cmd.Parameters.Add("@HeaderArea", SqlDbType.VarChar).Value = HeaderArea;
                    cmd.Parameters.Add("@MiddleArea", SqlDbType.VarChar).Value = MiddleArea;
                    cmd.Parameters.Add("@LowerArea", SqlDbType.VarChar).Value = LowerArea;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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
