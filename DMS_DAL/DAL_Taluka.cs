using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_DAL
{
    public class DAL_Taluka : DAL
    {
        public DataTable usp_Setup_Crud_Taluka(int OperationType, int? TalukaId, string TalukaName, int? UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_Taluka"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@TalukaId", SqlDbType.Int).Value = TalukaId;
                    cmd.Parameters.Add("@TalukaName", SqlDbType.VarChar).Value = TalukaName;
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

        public DataTable usp_SetupTaluka_IsTransection_Exist(int OperationType, int? Id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_Taluka"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@TalukaId", SqlDbType.Int).Value = Id;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_DeliveryDaysDate : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}
